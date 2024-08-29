using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class MenuButtonVisuals : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color baseColor;
    [SerializeField] private Color hoverColor;

    private Button button;
    private TextMeshProUGUI buttonText;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        baseColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = baseColor;
    }
}
