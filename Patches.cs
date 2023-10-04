using HarmonyLib;
using System.Linq;
using UnityEngine;
using WootingAnalogSDKNET;

namespace WootingPlugin
{
    [HarmonyPatch]
    public class Patches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(AxisBinding), nameof(AxisBinding.GetAxis))]
        private static void PatchGetAxis(ref float __result, AxisBinding __instance)
        {
            var keyBinding = KeyAxis.List.FirstOrDefault(k => k.Axis == __instance);
            if (keyBinding != null)
            {
                var valuePlus = Mathf.Max(GetAnalogValue(keyBinding.PlusKey.primary.code), GetAnalogValue(keyBinding.PlusKey.secondary.code));
                var valueMinus = Mathf.Max(GetAnalogValue(keyBinding.MinusKey.primary.code), GetAnalogValue(keyBinding.MinusKey.secondary.code));

                if (valuePlus > valueMinus)
                {
                    if (valuePlus > Mathf.Abs(__result))
                    {
                        __result = Mathf.Min(valuePlus*1.01f, 1f);
                    }
                }
                else
                {
                    if (valueMinus > Mathf.Abs(__result))
                    {
                        __result = Mathf.Max(-valueMinus*1.01f, -1f);
                    }
                }
            }
        }

        private static float GetAnalogValue(KeyCode code)
        {
            var key = ScanCode.TranslateFromKeyCode(code);

            if (key == HIDKeyboardScanCode.None)
            {
                return 0f;
            }

            var (value, result) = WootingAnalogSDK.ReadAnalog((byte)key);

            if (result != WootingAnalogResult.Ok)
            {
                Debug.Log("WootingPlugin: Failed to read key " + key);
            }
            return value;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(KeyBinding), nameof(KeyBinding.GetKey))]
        private static bool PatchGetKey(ref bool __result, KeyBinding __instance)
        {
            if (KeyAxis.List.Exists(k => k.PlusKey == __instance || k.MinusKey == __instance))
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
