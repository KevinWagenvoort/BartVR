using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOpenScript : MonoBehaviour
{
    public GameObject ChatAppPanel;
    public GameObject MinimapPanel;
    public GameObject TeleportTutorial;
    public GameObject MapPath;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor)
        {
            Navigation();
        }
    }

    void Navigation()
    {
        //PC
        if (Input.GetKeyUp(KeyCode.Return))
        {
            OpenMap();
        }
    }

    public void OpenMap()
    {
        ChatAppPanel.SetActive(false);
        MinimapPanel.SetActive(true);
        TeleportTutorial.SetActive(true);
        MapPath.SetActive(true);
        gameObject.SetActive(false);
    }
}
