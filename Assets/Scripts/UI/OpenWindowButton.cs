using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class OpenWindowButton : MonoBehaviour
{
    [SerializeField] private GameObject windowToClose;
    [SerializeField] private GameObject windowToOpen;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenWindow);
    }

    private void OpenWindow()
    {
        windowToClose.SetActive(false);
        windowToOpen.SetActive(true);
    }
}
