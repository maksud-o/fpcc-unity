using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class OptionsButton : MonoBehaviour
{
    [SerializeField] private GameObject optionsPopup;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => optionsPopup.SetActive(true));
    }
}
