using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [Header("Feature Toggles")]
    [SerializeField] private bool crosshairEnabled = true;
    [Header("Parameters")]
    [SerializeField] private Canvas playerUI;
    [SerializeField] private TextMeshProUGUI InteractablePromptText;
    [SerializeField] private GameObject crosshair;

    private void Start()
    {
        if(crosshairEnabled)
        {
            Instantiate(crosshair, playerUI.transform);
        }
    }

    public void UpdateText(string promptMessage)
    {
        InteractablePromptText.text = promptMessage;
    }
}
