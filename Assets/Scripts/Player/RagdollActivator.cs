using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollActivator : MonoBehaviour
{
    [SerializeField] private Rigidbody[] ragdollBodies;
    [SerializeField] private Collider[] ragdollColliders;
    [SerializeField] private Animator animator;
    [SerializeField] private HealthSystem health;
    [SerializeField] private MonoBehaviour[] disableOnDeath;
    [SerializeField] private Collider mainCollider;
    [SerializeField] private Rigidbody mainRigidbody;

    private void Start()
    {
        health.OnDeath += ActivateRagdoll;
        SetRagdollState(false); // Ã³À½¿£ ²¨µÒ
    }

    private void SetRagdollState(bool isActive)
    {
        foreach (var rb in ragdollBodies)
        {
            rb.isKinematic = !isActive;
        }

        foreach (var col in ragdollColliders)
        {
            if (col != null)
            {
                col.enabled = true; // Á×À» ¶§ ´Ù½Ã ÄÑÁÜ
                col.isTrigger = false;
            }
        }
    }

    private void ActivateRagdoll()
    {
        SetRagdollState(true);

        if (animator != null)
            animator.enabled = false;

        foreach (var comp in disableOnDeath)
        {
            if (comp != null)
                comp.enabled = false;

            if (comp is PlayerController controller)
                controller.SetDead();
        }

        // ±âÁ¸ ¸ÞÀÎ Rigidbody ¹«·ÂÈ­
        if (mainRigidbody != null)
        {
            mainRigidbody.isKinematic = true;
            mainRigidbody.detectCollisions = false;
        }

        if (mainCollider != null)
            mainCollider.enabled = false;
    }

}
