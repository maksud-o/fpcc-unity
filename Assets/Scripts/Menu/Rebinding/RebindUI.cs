using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RebindUI : MonoBehaviour
{
    [SerializeField] private InputActionReference inputAction;
    [SerializeField] private string bindingMask;
    [SerializeField] private bool excludeMouse = true;

    [Header("UI Display Parameters")]
    [SerializeField] private InputBinding.DisplayStringOptions bindingDisplayOption;
    [SerializeField] private TextMeshProUGUI actionLabel;
    [SerializeField] private Button rebindButton;
    [SerializeField] private TextMeshProUGUI bindingText;
    [Header("DEBUG - DO NOT CHANGE")]
    [SerializeField] private InputBinding[] inputBindings;


    private string actionName;

    private void OnValidate()
    {
        GetBindingInfo();
        UpdateUI();
    }

    private void GetBindingInfo()
    {
        if (inputAction != null)
        {
            actionName = inputAction.action.name;
        }

        inputBindings = inputAction.action.bindings.ToArray();
    }

    private void UpdateUI()
    {
        if (actionLabel != null)
        {
            actionLabel.text = actionName;
        }

        if (bindingText != null)
        {
            if (Application.isPlaying)
            {
                //grab data from Input Manager
            }
            else
            {
                
                bindingText.text = inputAction.action.GetBindingDisplayString(InputBinding.MaskByGroup(bindingMask), bindingDisplayOption);
            }
        }
    }


}
