using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Vibrations : MonoBehaviour
{
    public SteamVR_Action_Vibration hapticAction;
    private static SteamVR_Action_Vibration staticHapticAction;

    private void Start()
    {
        staticHapticAction = hapticAction;
    }

    public static void Pulse(float duration, float frequency = 50, float amplitude = 0.2f, SteamVR_Input_Sources source = SteamVR_Input_Sources.LeftHand)
    {
        try
        {
            staticHapticAction.Execute(0, duration, frequency, amplitude, source);
        } catch
        {
            Debug.LogError("No VR headset");
        }
    }
}
