using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    [Header("Features toggles")]
    [SerializeField] private bool sprintEnabled = true;
    [SerializeField] private bool crouchEnabled = true;
    [SerializeField] private bool jumpEnabled = true;
    [SerializeField] private bool slopeSlidingEnabled = true;

    [Header("Horizontal Parameters")]
    [SerializeField] private float crouchSpeed = 3f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    public static bool isMoving { get; private set; } = false;
    public static bool isSprinting { get; private set; } = false;

    [Header("Vertical Parameters")]
    [SerializeField] private float jumpHeight = 0.3f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float midairMovementModifier = 0.33f;
    [SerializeField] private float jumpBufferTime = 0.05f;
    private float lastJumpAttemptTime;
    private bool isTryingToJump = false;
    public static bool isGrounded { get; private set; }

    [Header("Crouching Parameters")]
    [SerializeField] private float crouchingHeight = 1f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private float crouchTransitionTime = 0.25f; //in seconds
    private float standingHeight;
    private Vector3 standingCenter;
    public static bool inTransition { get; private set; } = false;
    public static bool isCrouching { get; private set; } = false;

    [Header("Slope Sliding Parameters")]
    [SerializeField] private float slopeSpeed = 6f;

    private CharacterController controller;
    private Vector3 playerVelocity = Vector3.zero;
    private float playerYVelocity = 0;

    //CONSTRAINTS
    private bool canMove = true;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        standingHeight = controller.height;
        standingCenter = controller.center;
    }

    private void Start()
    {
        if(jumpEnabled)
        {
            PlayerInputSingleton.Instance.OnFoot.Jump.performed += _ => ProcessJump();
        }
        if(crouchEnabled)
        {
            PlayerInputSingleton.Instance.OnFoot.Crouch.performed += _ => ProcessCrouch();
        }
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;

        ApplyHorizontalMovement(PlayerInputSingleton.Instance.OnFoot.Movement.ReadValue<Vector2>());
        ApplyVerticalMovement();
        ApplySlopeSliding();
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void ApplyHorizontalMovement(Vector2 input)
    {
        if (canMove)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            if (moveDirection.x != 0 || moveDirection.z != 0)
            {
                isMoving = true;
            }
            else
            {
                playerVelocity = Vector3.zero;
                isMoving = false;
                return;
            }

            isSprinting = false;
            Vector3 motion;
            if (isCrouching)
            {
                motion = transform.TransformDirection(moveDirection) * crouchSpeed;
            }
            else if (sprintEnabled && PlayerInputSingleton.Instance.OnFoot.Sprint.inProgress && moveDirection.z > 0)
            {
                motion = transform.TransformDirection(moveDirection) * sprintSpeed;
                isSprinting = true;
            }
            else
            {
                motion = transform.TransformDirection(moveDirection) * walkSpeed;
            }

            if (!isGrounded)
            {
                motion *= midairMovementModifier;
            }
            playerVelocity = motion;
        }
    }

    private void ProcessJump()
    {
        isTryingToJump = true;
        lastJumpAttemptTime = Time.time;
    }

    private void ApplyJump()
    {
        bool wasTryingToJump = Time.time - lastJumpAttemptTime <= jumpBufferTime;
        bool isOrWasTryingToJump = wasTryingToJump || isTryingToJump;
        bool canJump = isGrounded && canMove;
        if (isOrWasTryingToJump && canJump)
        {
            playerYVelocity = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
        isTryingToJump = false;
    }

    private void ApplyVerticalMovement()
    {
        ApplyJump();
        playerYVelocity += gravity * Time.deltaTime;
        if (isGrounded && playerYVelocity < -2f)
        {
            playerYVelocity = -2f;
        }
        playerVelocity.y = playerYVelocity;
    }

    private void ApplySlopeSliding()
    {
        if (slopeSlidingEnabled && isGrounded && Physics.Raycast(gameObject.transform.position, Vector3.down, out RaycastHit hit, 2f) && Vector3.Angle(hit.normal, Vector3.up) >= controller.slopeLimit)
        {
            var normal = hit.normal;
            playerVelocity += new Vector3(normal.x, -normal.y, normal.z) * slopeSpeed;
        }
    }

    private void ProcessCrouch()
    {
        if (isGrounded)
        {
            StartCoroutine(CrouchTransition());
        }
    }

    private IEnumerator CrouchTransition()
    {
        if (isCrouching && Physics.Raycast(transform.position + new Vector3(0, controller.height / 2, 0), Vector3.up, 1f))
        {
            yield break;
        }

        inTransition = true;
        float timeElapsed = 0f;
        float targetHeight = isCrouching ? standingHeight : crouchingHeight;
        float currentHeight = controller.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = controller.center;

        while (timeElapsed < crouchTransitionTime)
        {
            controller.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / crouchTransitionTime);
            controller.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / crouchTransitionTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        controller.height = targetHeight;
        controller.center = targetCenter;

        isCrouching = !isCrouching;
        inTransition = false;
    }
}
