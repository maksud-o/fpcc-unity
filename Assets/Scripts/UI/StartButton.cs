using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class StartButton : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => SceneManager.LoadScene(sceneName, LoadSceneMode.Single));
    }
}