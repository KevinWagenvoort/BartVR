using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNEW : MonoBehaviour
{
    public GameObject AnimatorObject;

    private Animator animator;
    [SerializeField]
    private float npcMovementSpeed = 5f;
    private float lowerBodyLayer = 0f;
    // Use this for initialization
    private NavMeshAgent navMeshAgent;
    void Start()
    {
        animator = AnimatorObject.GetComponent<Animator>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = navMeshAgent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }
}
