using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapSync : MonoBehaviour
{
    public GameObject Arrow;
    public GameObject CameraRig;
    private Transform Eye;

    // Start is called before the first frame update
    void Start()
    {
        Eye = CameraRig.transform.Find("Camera (eye)");
        if (Eye == null)
        {
            Eye = CameraRig.transform.Find("Camera (head)").transform.Find("Camera (eye)");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Arrow.GetComponent<RectTransform>().transform.localRotation = Quaternion.Euler(90, 0, Eye.localEulerAngles.y -45);
        Debug.Log(Eye.eulerAngles.y);
    }
}
