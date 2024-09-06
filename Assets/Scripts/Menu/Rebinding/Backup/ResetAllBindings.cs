using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ResetAllBindings : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ResetBindings);
    }

    private void ResetBindings()
    {
        foreach (var actionMap in inputActions.actionMaps)
        {
            actionMap.RemoveAllBindingOverrides();
        }
    }

}
