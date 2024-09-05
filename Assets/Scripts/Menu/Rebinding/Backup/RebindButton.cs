using UnityEngine;
using UnityEngine.InputSystem.Samples.RebindUI;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class RebindButton : MonoBehaviour
{
    [SerializeField] private RebindActionUI rebindAction;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(rebindAction.StartInteractiveRebind);
    }
}
