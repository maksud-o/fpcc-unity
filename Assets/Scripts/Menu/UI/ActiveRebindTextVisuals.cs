using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ActiveRebindTextVisuals : MonoBehaviour
{
    [SerializeField] private float blinkingFrequency;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(TextBlinking());
    }

    private IEnumerator TextBlinking()
    {
        while (true)
        {
            text.enabled = !text.enabled;
            yield return new WaitForSeconds(blinkingFrequency);
        }
    }
}
