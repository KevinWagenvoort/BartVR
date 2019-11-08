using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour {
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag != "Hands" && collision.gameObject.tag != "POI" && collision.gameObject.tag != "Suspect" && collision.gameObject.tag != "ScenarioTrigger") {
			SteamVR_Fade.Start(Color.black, 0f);
		}

        if(collision.gameObject.tag == "ScenarioTrigger")
        {
            Debug.Log("Scenario triggered");
        }
	}

	void OnCollisionExit(Collision collision) {
		SteamVR_Fade.Start(Color.clear,0f);
	}
}
