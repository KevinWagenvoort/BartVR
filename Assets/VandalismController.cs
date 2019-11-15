using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VandalismController : MonoBehaviour
{
    public GameObject Thrower, Window, ThrowingObject;

    public Mesh BrokenWindowMesh;

    private Animator animator;
    private MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        animator = Thrower.GetComponent<Animator>();
        meshFilter = Window.GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartVandalism()
    {
        Debug.Log("StartVandalism");
        Invoke("DoingVandalism", 5);
    }

    void DoingVandalism()
    {
        animator.SetTrigger("VandalismTrigger");
        ThrowingObject.SetActive(true);
        Invoke("DoneVandalism", 1);
    }

    void DoneVandalism()
    {
        ThrowingObject.SetActive(false);
        meshFilter.mesh = BrokenWindowMesh;
    }
}
