using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSync : MonoBehaviour
{
    [Serializable]
    public struct NpcImageOption
    {
        public GameObject npc;
        public GameObject minimapImage;
    }

    // Public viarables
    public GameObject map;
    public GameObject plane;
    public List<NpcImageOption> NpcToDisplayOnMinimap;

    // Private viarables 
    private Vector2 mapSize;
    private Vector2 planeSize;
    private float xScale;
    private float yScale;
    //public float offsetx = 1.7f;
    //public float offsety = 2f;

    // Public methods
    public Vector2 GetSuspectMinimapLocation()
    {
        // Last item in array is suspect
        return NpcToDisplayOnMinimap[NpcToDisplayOnMinimap.Count - 1].minimapImage.GetComponent<RectTransform>().transform.localPosition;
    }

    // Private methods
    private void Start()
    {
        UpdateMapSizeAndScale();
    }

    void Update()
    {
        foreach (NpcImageOption npc in NpcToDisplayOnMinimap)
            ScaleNpcOnMap(npc);
    }

    private void UpdateMapSizeAndScale()
    {
        mapSize = map.GetComponent<RectTransform>().sizeDelta;
        planeSize = plane.GetComponent<RectTransform>().sizeDelta;
        //xScale = (planeSize.x / mapSize.x) + offsetx;
        //yScale = (planeSize.y / mapSize.y) + offsety;
    }

    private void ScaleNpcOnMap(NpcImageOption NpcOption)
    {
        NpcOption.minimapImage.GetComponent<RectTransform>().transform.localPosition = new Vector2(-1 * (NpcOption.npc.transform.position.x),
                                                                                 -1 * (NpcOption.npc.transform.position.z));
        if (NpcOption.npc.tag == "CameraRig")
        {
            try
            {
                Transform eye = NpcOption.npc.transform.Find("Camera (eye)");
                NpcOption.minimapImage.GetComponent<RectTransform>().transform.rotation = Quaternion.Euler(0, 0, eye.eulerAngles.y - 45);
            }
            catch
            {

            }
        }
    }
}
