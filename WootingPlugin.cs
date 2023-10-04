using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using WootingAnalogSDKNET;

namespace WootingPlugin
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class WootingPlugin : MonoBehaviour
    {
        public void Start()
        {
            KeyAxis.BuildList();

            GameEvents.OnGameSettingsWritten.Add(delegate() { KeyAxis.BuildList(); });

            try
            {
                var (numDev, result) = WootingAnalogSDK.Initialise();

                if (result == WootingAnalogResult.Ok)
                {
                    Debug.Log("WootingPlugin: WootingAnalogSDK initialized successfully");
                }
                else
                {
                    Debug.Log($"WootingPlugin: Failed to initialize: {result}");
                    return;
                }

                if (numDev < 1)
                {
                    Debug.Log("WootingPlugin: No keyboard detected");
                    return;
                }
            }
            catch (Exception e)
            {
                Debug.Log("WootingPlugin: WootingAnalogSDK failed to initialize due to an internal error: " + e.ToString());
                return;
            }

            Debug.Log("WootingPlugin: Harmonizing");
            var harmony = new Harmony("Garbius.WootingPlugin");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
