using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerUIController), typeof(PlayerInput))]
public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference interact;

    private Interactable interactable;
    private bool onInteractable = false;
    private Camera playerCamera;
    private PlayerUIController playerUI;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        playerUI = GetComponent<PlayerUIController>();
    }

    private void Start()
    {
        interact.action.performed += _ => Interact();
    }

    private void Update()
    {
        ProcessInteractionRaycast();
    }
    private void Interact()
    {
        if (onInteractable)
        {
            interactable.Interact();
        }
    }

    private void ProcessInteractionRaycast()
    {
        var ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, config.RayLength, config.InteractableMask) && hitInfo.collider.TryGetComponent(out Interactable _interactable))
        {
            interactable = _interactable;
            onInteractable = true;
            playerUI.UpdateText(interactable.PromptMessage);
        }
        else
        {
            onInteractable = false;
            playerUI.UpdateText(string.Empty);
        }
    }
}
