using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public PlayerInput.OnFootActions OnFoot;

    public static PlayerInputManager Instance { get; private set; }

    private PlayerInput playerInput;

    private void Awake()
    {
        Instance = this;
        playerInput = new PlayerInput();
        OnFoot = playerInput.OnFoot;
    }

    private void OnEnable()
    {
        OnFoot.Enable();
    }

    private void OnDisable()
    {
        OnFoot.Disable();
    }
}
