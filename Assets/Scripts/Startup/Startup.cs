using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour
{
    [SerializeField] private string firstSceneName;

    private void Start()
    {
        SceneManager.LoadScene(firstSceneName, LoadSceneMode.Single);
    }
}
