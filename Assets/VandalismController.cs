using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VandalismController : MonoBehaviour
{
    public GameObject Thrower, Window, ThrowingObject, FlyingObject, LandedObject;

    public Mesh BrokenWindowMesh;

    private Animator animator;
    private MeshFilter meshFilter;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = Thrower.GetComponent<Animator>();
        meshFilter = Window.GetComponent<MeshFilter>();
        audioSource = Window.GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartVandalism()
    {
        Invoke("DoingVandalism", 5);
    }

    void DoingVandalism()
    {
        animator.SetTrigger("VandalismTrigger");
        ThrowingObject.SetActive(true);
        Invoke("MidVandalism", 1);
    }

    void MidVandalism()
    {
        //TODO: Add glass breaking sound
        audioSource.Play();

        ThrowingObject.SetActive(false);
        FlyingObject.SetActive(true);
        meshFilter.mesh = BrokenWindowMesh;
        Invoke("DoneVandalism", 0.1f);
    }

    void DoneVandalism()
    {
        FlyingObject.SetActive(false);
        LandedObject.SetActive(true);
        meshFilter.mesh = BrokenWindowMesh;
    }
}
