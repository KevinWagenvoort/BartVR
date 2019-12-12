using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class OnCollision : MonoBehaviour {
    public GameObject Teleporting;

	void OnTriggerEnter(Collider collision) {
		if(collision.gameObject.tag != "Hands" && collision.gameObject.tag != "POI" && collision.gameObject.tag != "Suspect" && collision.gameObject.tag != "Officer" && collision.gameObject.tag != "VRUIButton") {
			SteamVR_Fade.Start(Color.black, 0f);
            Teleporting.SetActive(false);
		}
    }

	void OnTriggerExit(Collider collision) {
        Teleporting.SetActive(true);
		SteamVR_Fade.Start(Color.clear,0f);
    }
}
