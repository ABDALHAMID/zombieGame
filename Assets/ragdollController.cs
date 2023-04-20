using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdollController : MonoBehaviour
{
    public Rigidbody mainRigidbody;
    public Animator animator;
    Rigidbody[] ragdollRigidbodys;
    void Start()
    {
        getRagdollBits();
        RagdolModeOff();
    }

    void Update()
    {
        
    }
    public void OnHit(Rigidbody rb)
    {
        rb.isKinematic = false;
        rb.AddForce(-transform.up * 2);
    }
    public void OnDie()
    {
        Invoke(nameof(RagdolModeOn), 1f);
    }
    public void RagdolModeOn()
    {
        animator.enabled = false;
        foreach (Rigidbody rb in ragdollRigidbodys)
        {
            rb.isKinematic = false;
        }
    }
    public void RagdolModeOff()
    {
        foreach (Rigidbody rb in ragdollRigidbodys)
        {
            rb.isKinematic = true;
        }
        animator.enabled = true;
    }
    void getRagdollBits()
    {
        ragdollRigidbodys = mainRigidbody.GetComponentsInChildren<Rigidbody>();
    }
}
