using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody[] limpsRigidbodies;
    private Collider[] limpsColliders;
    public GameObject hips;
    public GameObject thisguy;
    public BoxCollider bigCollider;
    //public Animator thisguyAnimator;
    private void Awake()
    {
        GetRagdollBits();
        RagdollModeOff();
        
    }

    void GetRagdollBits()
    {
        limpsRigidbodies = thisguy.GetComponentsInChildren<Rigidbody>();
        limpsColliders = thisguy.GetComponentsInChildren<Collider>();
    }

    void RagdollModeOff()
    {
        foreach (var rigid in limpsRigidbodies)
        {
            rigid.isKinematic = true;
        }

        foreach (var col in limpsColliders)
        {
            col.enabled = false;
        }

        //thisguyAnimator.enabled = true;
        bigCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    void RagdollModeOn()
    {
        foreach (var rigid in limpsRigidbodies)
        {
            rigid.isKinematic = false;
        }

        foreach (var col in limpsColliders)
        {
            col.enabled = true;
        }

        //thisguyAnimator.enabled = false;
        bigCollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "BluntObj")
        {
            RagdollModeOn();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
