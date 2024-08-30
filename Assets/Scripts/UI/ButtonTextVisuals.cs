using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTextVisuals : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color baseColor;
    [SerializeField] private Color hoverColor;

    private TextMeshProUGUI buttonText;

    private void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        baseColor = buttonText.color;
    }

    private void OnDisable()
    {
        buttonText.color = baseColor;
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
