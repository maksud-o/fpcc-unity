using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonSpriteVisuals : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite onHoverSprite;

    private Image image;
    private Sprite baseSprite;

    private void Awake()
    {
        image = GetComponent<Image>();
        baseSprite = image.sprite;
    }

    private void OnDisable()
    {
        image.sprite = baseSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = onHoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = baseSprite;
    }
}
