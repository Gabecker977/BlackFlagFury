using System.Collections;
using UnityEngine;
using TMPro;

public class FadeOutText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    public float fadeDuration = 1.0f; // Duração da transição de fade. Ajuste como necessário.

    void Start()
    {
        StartCoroutine(FadeTextToZeroAlpha(fadeDuration, textMeshPro));
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
