using System.Collections;
using System.Threading;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Feature Toggles")]
    [SerializeField] private bool headBobEnabled = true;
    [SerializeField] private bool zoomEnabled = true;

    [Header("Look Parameters")]
    [SerializeField] private float xSensitivity = 30f;
    [SerializeField] private float ySensitivity = 30f;
    [SerializeField, Range(1f, 180f)] private float upperLookLimit = 80f;
    [SerializeField, Range(1f, 180f)] private float lowerLookLimit = 80f;

    [Header("Head Bob Parameters")]
    [SerializeField] private float walkHeadbobFreq = 14f;
    [SerializeField] private float walkHeadbobAmplitude = 0.05f;
    [SerializeField] private float sprintHeadbobFreq = 18f;
    [SerializeField] private float sprintHeadbobAmplitude = 0.1f;
    [SerializeField] private float crouchHeadbobFreq = 8f;
    [SerializeField] private float crouchHeadbobAmplitude = 0.025f;
    [SerializeField][Tooltip("Time to reset camera to default position")] private float ResetTime = 0.75f;
    private Vector3 cameraStartPos;
    private bool inReset = false;

    [Header("Zoom Pwarameters")]
    [SerializeField] private float zoomTransitionTime = 0.2f;
    [SerializeField][Range(1, 120)] private float zoomFOV = 30f;
    [SerializeField][Range(1, 120)] private float defaultFOV = 90f;
    private Coroutine zoomCoroutine;

    private Camera playerCamera;
    private float rotation;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        cameraStartPos = playerCamera.transform.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        playerCamera.fieldOfView = defaultFOV;

        if(zoomEnabled)
        {
            PlayerInputSingleton.Instance.OnFoot.ZoomADS.started += _ => ZoomIn();
            PlayerInputSingleton.Instance.OnFoot.ZoomADS.canceled += _ => ZoomOut();
        }
    }

    private void Update()
    {
        ProcessLook(PlayerInputSingleton.Instance.OnFoot.Look.ReadValue<Vector2>());
        if (headBobEnabled)
        {
            ProcessHeadBob();
        }
    }

    private void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        rotation -= mouseY * Time.deltaTime * ySensitivity;
        rotation = Mathf.Clamp(rotation, -upperLookLimit, lowerLookLimit); //clamp y rotation
        playerCamera.transform.localRotation = Quaternion.Euler(rotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX * Time.deltaTime * xSensitivity); //rotates both camera and player
    }

    private IEnumerator ResetCameraPosition()
    {
        inReset = true;
        float timeElapsed = 0;
        while(!PlayerMovementController.isMoving && timeElapsed < ResetTime)
        {
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, cameraStartPos, timeElapsed / ResetTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        playerCamera.transform.localPosition = cameraStartPos;
        inReset = false;
    }

    private void ProcessHeadBob()
    {
        if (!PlayerMovementController.isMoving || !PlayerMovementController.isGrounded)
        {
            if(!inReset && playerCamera.transform.localPosition != cameraStartPos)
            {
                StartCoroutine(ResetCameraPosition());
            }
            return;
        }

        float frequency;
        float amplitude;
        if (PlayerMovementController.isCrouching)
        {
            frequency = crouchHeadbobFreq;
            amplitude = crouchHeadbobAmplitude;
        }
        else if (PlayerMovementController.isSprinting)
        {
            frequency = sprintHeadbobFreq;
            amplitude = sprintHeadbobAmplitude;
        }
        else
        {
            frequency = walkHeadbobFreq;
            amplitude = walkHeadbobAmplitude;
        }

        var bobbing = Vector3.zero;
        bobbing.y = Mathf.Sin(Time.time * frequency) * amplitude;
        bobbing.x = Mathf.Cos(Time.time * frequency / 2) * amplitude / 2;
        playerCamera.transform.localPosition += bobbing * Time.deltaTime;
    }

    private void ZoomIn()
    {
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
            zoomCoroutine = null;
        }
        zoomCoroutine = StartCoroutine(ZoomTransition(true));
    }

    private void ZoomOut()
    {
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
            zoomCoroutine = null;
        }
        zoomCoroutine = StartCoroutine(ZoomTransition(false));
    }

    private IEnumerator ZoomTransition(bool isZoomIn)
    {
        float targetFOV;
        float transitionTime;
        if(isZoomIn)
        {
            targetFOV = zoomFOV;
            transitionTime = zoomTransitionTime * (playerCamera.fieldOfView / defaultFOV);
        }
        else
        {
            targetFOV = defaultFOV;
            transitionTime = zoomTransitionTime * (playerCamera.fieldOfView / zoomFOV);
        }

        float elapsedTime = 0;
        while(elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, elapsedTime / transitionTime);
            yield return null;
        }
        playerCamera.fieldOfView = targetFOV;
        zoomCoroutine = null;
    }
}
