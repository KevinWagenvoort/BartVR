using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR;

public class MinimapRouting : MonoBehaviour
{
    public Transform target;
    public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
    public LineRenderer lineRenderer;
    private NavMeshPath path;

    void Start()
    {
        path = new NavMeshPath();
        UpdatePath();
    }

    void Update()
    {
        // Update the way to the goal every second.
        if (teleportAction.GetStateUp(SteamVR_Input_Sources.RightHand) || teleportAction.GetStateUp(SteamVR_Input_Sources.LeftHand))
        {
            Invoke("UpdatePath", 0.2f);
        }
    }

    void UpdatePath()
    {
        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
        lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPositions(path.corners);
    }
}
