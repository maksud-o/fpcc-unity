using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }
}
