using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(PlayerUI))]
public class PlayerInteractController : MonoBehaviour
{
    [Header("Interaction Parameters")]
    [SerializeField] private float rayLength = 3f;
    [SerializeField] private LayerMask interactableMask;
    private Interactable interactable;
    private bool onInteractable = false;

    private Camera playerCamera;
    private PlayerUI playerUI;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        playerUI = GetComponent<PlayerUI>();
    }

    private void Start()
    {
        PlayerInputManager.Instance.OnFoot.Interact.performed += _ => Interact();
    }

    private void Update()
    {
        ProcessInteractionRaycast();
    }

    private void Interact()
    {
        if(onInteractable)
        {
            interactable.Interact();
        }
    }

    private void ProcessInteractionRaycast()
    {
        var ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayLength, interactableMask) && hitInfo.collider.TryGetComponent(out Interactable _interactable))
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
