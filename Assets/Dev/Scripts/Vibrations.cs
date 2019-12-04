using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrations : MonoBehaviour
{

    private void Start()
    {
    }

    public static IEnumerator LongVibration(float seconds, ushort strength)
    {
        for (float i = 0; i < seconds; i += Time.deltaTime)
        {
            try
            {
            }
            catch
            {

            }
            yield return null; //every single frame for the duration of "length" you will vibrate at "strength" amount
        }
    }
}
