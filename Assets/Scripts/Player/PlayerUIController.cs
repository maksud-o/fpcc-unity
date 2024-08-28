using TMPro;
using UnityEngine;
using UnityEngine.UI;

// TODO: Editor Scripts
public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;

    [SerializeField] private Canvas playerUI;
    [SerializeField] private TextMeshProUGUI InteractablePromptText;
    //[SerializeField] private GameObject crosshair;

    private void Start()
    {
        //if(crosshairEnabled)
        //{
        //    Instantiate(crosshair, playerUI.transform);
        //}

        InteractablePromptText.text = string.Empty;
    }

    public void UpdateText(string promptMessage)
    {
        InteractablePromptText.text = promptMessage;
    }
}
