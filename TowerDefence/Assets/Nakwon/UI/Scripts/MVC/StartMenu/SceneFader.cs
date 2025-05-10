using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage; // FadePanel의 Image

    private void Awake()
    {
        // 시작 시 완전 투명으로 초기화
        SetAlpha(0f);
    }

    public void SetAlpha(float alpha)
    {
        var color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }

    public IEnumerator FadeIn(float duration = 1f)
    {
        float time = 0f;
        while (time < duration)
        {
            SetAlpha(1f - time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        SetAlpha(0f);
    }

    public IEnumerator FadeOut(float duration = 1f)
    {
        float time = 0f;
        while (time < duration)
        {
            SetAlpha(time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        SetAlpha(1f);
    }
}
