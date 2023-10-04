using System.Collections.Generic;
using UnityEngine;

namespace WootingPlugin
{
    internal class KeyAxis
    {
        public static List<KeyAxis> List;

        public static void BuildList()
        {
            Debug.Log("WootingPlugin: Building binding list");

            List = new List<KeyAxis>
            {
                new KeyAxis(GameSettings.AXIS_CAMERA_HDG, GameSettings.CAMERA_ORBIT_RIGHT, GameSettings.CAMERA_ORBIT_LEFT),
                new KeyAxis(GameSettings.AXIS_CAMERA_PITCH, GameSettings.CAMERA_ORBIT_UP, GameSettings.CAMERA_ORBIT_DOWN),
                new KeyAxis(GameSettings.AXIS_PITCH, GameSettings.PITCH_UP, GameSettings.PITCH_DOWN),
                new KeyAxis(GameSettings.AXIS_YAW, GameSettings.YAW_RIGHT, GameSettings.YAW_LEFT),
                new KeyAxis(GameSettings.AXIS_ROLL, GameSettings.ROLL_RIGHT, GameSettings.ROLL_LEFT),
                new KeyAxis(GameSettings.AXIS_TRANSLATE_X, GameSettings.TRANSLATE_RIGHT, GameSettings.TRANSLATE_LEFT),
                new KeyAxis(GameSettings.AXIS_TRANSLATE_Y, GameSettings.TRANSLATE_FWD, GameSettings.TRANSLATE_BACK),
                new KeyAxis(GameSettings.AXIS_TRANSLATE_Z, GameSettings.TRANSLATE_UP, GameSettings.TRANSLATE_DOWN),
                new KeyAxis(GameSettings.AXIS_THROTTLE_INC, GameSettings.THROTTLE_UP, GameSettings.THROTTLE_DOWN),
                new KeyAxis(GameSettings.AXIS_WHEEL_STEER, GameSettings.WHEEL_STEER_LEFT, GameSettings.WHEEL_STEER_RIGHT),
                new KeyAxis(GameSettings.AXIS_WHEEL_THROTTLE, GameSettings.WHEEL_THROTTLE_UP, GameSettings.WHEEL_THROTTLE_DOWN),
            };
        }

        public AxisBinding Axis { get; }
        public KeyBinding PlusKey { get; }
        public KeyBinding MinusKey { get; }
        public KeyAxis(AxisBinding axis, KeyBinding plusKey, KeyBinding minusKey)
        {
            Axis = axis;
            PlusKey = plusKey;
            MinusKey = minusKey;
        }

        public KeyAxis(AxisKeyBinding axisKey)
        {
            Axis = axisKey.axisBinding;
            PlusKey = axisKey.plusKeyBinding;
            MinusKey = axisKey.minusKeyBinding;
        }
    }
}
