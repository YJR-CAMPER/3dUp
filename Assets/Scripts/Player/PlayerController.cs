using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpPower = 7f;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook = -60f;
    public float maxXLook = 70f;
    public float lookSensitivity = 2f;

    private Rigidbody rb;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float camCurXRot;
    private bool isRunning;
    private bool groundedLastFrame = true;
    private bool isLaunched = false;
    private bool isDead = false;

    [SerializeField] private Animator anim;

    [HideInInspector]
    public bool canLook = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetRun(bool run)
    {
        isRunning = run;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        lookInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started && groundedLastFrame)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetTrigger("IsJump");
            groundedLastFrame = false;
        }
    }

    public void SetDead()
    {
        isDead = true;
    }

    private void FixedUpdate()
    {
        if (isLaunched)
        {
            if (rb.velocity.y <= 0.1f)
            {
                isLaunched = false;
                Debug.Log("Launch ended, control returned");
            }
            return;
        }

        Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        float speed = isRunning ? runSpeed : moveSpeed;
        moveDir *= speed;
        moveDir.y = rb.velocity.y;

        rb.velocity = moveDir;

        Vector3 localDir = transform.InverseTransformDirection(moveDir.normalized);
        anim.SetFloat("MoveX", localDir.x);
        anim.SetFloat("MoveY", localDir.z);
        anim.SetFloat("Speed", isRunning ? 1f : 0.5f);

        if (IsGrounded() && rb.velocity.y <= 0.1f)
        {
            groundedLastFrame = true;
            anim.SetBool("IsJump", false);
        }
    }

    private void LateUpdate()
    {
        if (!canLook) return;

        camCurXRot += lookInput.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, lookInput.x * lookSensitivity, 0);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.01f, Vector3.down, 0.1f, groundLayerMask);
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    //  점프패드 대응용 외부 발사 함수
    public void Launch(Vector3 direction, float force)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        anim.SetTrigger("IsJump");
        isLaunched = true;
        groundedLastFrame = false;
    }
}
