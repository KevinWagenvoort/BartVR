using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapSync : MonoBehaviour
{
    public GameObject Arrow;
    public Transform VRCamera;

    // Update is called once per frame
    void Update()
    {
        Arrow.GetComponent<RectTransform>().transform.localRotation = Quaternion.Euler(90, 0, -VRCamera.localEulerAngles.y);
    }
}
