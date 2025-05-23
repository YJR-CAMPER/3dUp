using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Header("Launch Settings")]
    public Vector3 launchDirection = Vector3.up;      // 발사 방향
    public float launchForce = 15f;                   // 발사 힘

    [Header("Options")]
    public bool overrideVerticalVelocity = true;      // 기존 y속도 무시할지
    public string targetTag = "Player";               // 태그 필터 (비워두면 무제한)

    private void OnTriggerEnter(Collider other)
    {

        // 태그 필터링 (비워두면 생략)
        if (!string.IsNullOrEmpty(targetTag) && !other.CompareTag(targetTag)) return;

        // Rigidbody 찾기
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        // PlayerController 찾기 (자식 Collider 대응용)
        PlayerController controller = other.GetComponentInParent<PlayerController>();
        if (controller != null)
        {
            // 발사 방향 계산
            Vector3 dir = launchDirection.normalized;

            // y속도 제거 (선택사항)
            if (overrideVerticalVelocity)
            {
                Vector3 vel = rb.velocity;
                vel.y = 0f;
                rb.velocity = vel;
            }

            Debug.Log("[JumpPad] Launching Player!");
            controller.Launch(dir, launchForce);
        }
        else
        {
            // Fallback: Launch 없을 경우 강제 물리 적용
            Vector3 dir = launchDirection.normalized;

            if (overrideVerticalVelocity)
            {
                Vector3 vel = rb.velocity;
                vel.y = 0f;
                rb.velocity = vel;
            }

            rb.AddForce(dir * launchForce, ForceMode.Impulse);
        }
    }
}

