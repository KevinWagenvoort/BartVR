using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrations : MonoBehaviour
{
    public static SteamVR_TrackedObject trackedObject;
    public static SteamVR_Controller.Device controller;

    private void Start()
    {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
        controller = SteamVR_Controller.Input((int)trackedObject.index);
    }

    public static IEnumerator LongVibration(float seconds, ushort strength)
    {
        for (float i = 0; i < seconds; i += Time.deltaTime)
        {
            try
            {
                controller.TriggerHapticPulse(strength);
            }
            catch
            {

            }
            yield return null; //every single frame for the duration of "length" you will vibrate at "strength" amount
        }
    }
}
