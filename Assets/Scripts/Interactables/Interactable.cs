using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string PromptMessage { get => promptMessage; }

    [SerializeField] private string promptMessage;

    private const int interactableLayerNum = 6;

    private void Awake()
    {
        gameObject.layer = interactableLayerNum;
    }

    public void Interact()
    {
        BaseInteract();
    }

    protected abstract void BaseInteract();
}
