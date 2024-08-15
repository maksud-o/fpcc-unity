using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSingleton : MonoBehaviour
{
    public PlayerInput.OnFootActions OnFoot;

    public static PlayerInputSingleton Instance { get; private set; }

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
