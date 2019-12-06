using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class OnCollision : MonoBehaviour {
    public GameObject Teleporting;

	void OnTriggerEnter(Collider collision) {
		if(collision.gameObject.tag != "Hands" && collision.gameObject.tag != "POI" && collision.gameObject.tag != "Suspect") {
			SteamVR_Fade.Start(Color.black, 0f);
            Teleporting.SetActive(false);
		}
        Debug.Log(collision.name);
    }

	void OnTriggerExit(Collider collision) {
        Teleporting.SetActive(true);
		SteamVR_Fade.Start(Color.clear,0f);
    }
}
