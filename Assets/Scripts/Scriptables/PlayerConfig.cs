using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Config", menuName = "Player Config")]
public class PlayerConfig : ScriptableObject
{
    [Header("MOVEMENT")]

    [Header("Features Toggles")]
    [SerializeField] private bool sprintEnabled = true;
    [SerializeField] private bool crouchEnabled = true;
    [SerializeField] private bool jumpEnabled = true;
    [SerializeField] private bool slopeSlidingEnabled = true;
    public bool SprintEnabled { get => sprintEnabled; }
    public bool CrouchEnabled { get => crouchEnabled; }
    public bool JumpEnabled { get => jumpEnabled; }
    public bool SlopeSlidingEnabled { get => slopeSlidingEnabled; }

    [Header("Horizontal Movement Parameters")]
    [SerializeField] private float crouchSpeed = 3f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    public float CrouchSpeed { get => crouchSpeed; }
    public float WalkSpeed { get => walkSpeed; }
    public float SprintSpeed { get => sprintSpeed; }

    [Header("Vertical Movement Parameters")]
    [SerializeField] private float jumpHeight = 0.3f;
    [SerializeField] private float downforce = -9.8f;
    [SerializeField] private float midairMovementModifier = 0.33f;
    [SerializeField] private float jumpBufferTime = 0.05f;
    public float JumpHeight { get => jumpHeight; }
    public float Downforce { get => downforce; }
    public float MidairMovementModifier { get => midairMovementModifier; }
    public float JumpBufferTime { get => jumpBufferTime; }

    [Header("Crouching Parameters")]
    [SerializeField] private float crouchingHeight = 1f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private float crouchTransitionTime = 0.25f;
    public float CrouchingHeight { get => crouchingHeight; }
    public Vector3 CrouchingCenter { get => crouchingCenter; }
    public float CrouchTransitionTime { get => crouchTransitionTime; }

    [Header("Slope Sliding Parameters")]
    [SerializeField] private float slopeSpeed = 6f;
    public float SlopeSpeed { get => slopeSpeed; }

    [Header("CAMERA")]

    [Header("Features Toggles")]
    [SerializeField] private bool headBobEnabled = true;
    [SerializeField] private bool zoomEnabled = true;
    public bool HeadBobEnabled { get => headBobEnabled; }
    public bool ZoomEnabled { get => zoomEnabled; }

    [Header("Look Parameters")]
    [SerializeField] private float xSensitivity = 30f;

    [SerializeField] private float ySensitivity = 30f;
    [SerializeField, Range(1f, 180f)] private float upperLookLimit = 80f;
    [SerializeField, Range(1f, 180f)] private float lowerLookLimit = 80f;
    public float XSensitivity { get => xSensitivity; }
    public float YSensitivity { get => ySensitivity; }
    public float UpperLookLimit { get => upperLookLimit; }
    public float LowerLookLimit { get => lowerLookLimit; }

    [Header("Head Bob Parameters")]
    [SerializeField] private float walkHeadbobFreq = 12f;
    [SerializeField] private float walkHeadbobAmplitude = 0.5f;
    [SerializeField] private float sprintHeadbobFreq = 16f;
    [SerializeField] private float sprintHeadbobAmplitude = 1f;
    [SerializeField] private float crouchHeadbobFreq = 8f;
    [SerializeField] private float crouchHeadbobAmplitude = 0.1f;
    [SerializeField][Tooltip("Time to reset camera to default position")] private float resetTime = 0.75f;
    public float WalkHeadbobFreq { get => walkHeadbobFreq; }
    public float WalkHeadbobAmplitude { get => walkHeadbobAmplitude; }
    public float SprintHeadbobFreq { get => sprintHeadbobFreq; }
    public float SprintHeadbobAmplitude { get => sprintHeadbobAmplitude; }
    public float CrouchHeadbobFreq { get => crouchHeadbobFreq; }
    public float CrouchHeadbobAmplitude { get => crouchHeadbobAmplitude; }
    public float ResetTime { get => resetTime; }

    [Header("Zoom Parameters")]
    [SerializeField] private float zoomTransitionTime = 0.2f;

    [SerializeField][Range(1, 120)] private float zoomFOV = 30f;
    [SerializeField][Range(1, 120)] private float defaultFOV = 90f;
    public float ZoomTransitionTime { get => zoomTransitionTime; }
    public float ZoomFOV { get => zoomFOV; }
    public float DefaultFOV { get => defaultFOV; set => defaultFOV = value; }

    [Header("AUDIO")]

    [Header("Features Toggles")]
    [SerializeField] private bool footstepsEnabled = true;
    public bool FootstepsEnabled { get => footstepsEnabled; }

    [Header("Footstep Audio Parameters")]
    [SerializeField] private float crouchFootstepInterval = 1f;
    [SerializeField] private float walkFootstepInterval = 0.5f;
    [SerializeField] private float sprintFootstepInterval = 0.25f;
    public float CrouchFootstepInterval { get => crouchFootstepInterval; }
    public float WalkFootstepInterval { get => walkFootstepInterval; }
    public float SprintFootstepInterval { get => sprintFootstepInterval; }

    [Header("INTERACTION")]

    [SerializeField] private float rayLength = 3f;
    [SerializeField] private LayerMask interactableMask;
    public float RayLength { get => rayLength; }
    public LayerMask InteractableMask { get => interactableMask; }

    [Header("UI")]

    [SerializeField] private bool crosshairEnabled = true;
}
