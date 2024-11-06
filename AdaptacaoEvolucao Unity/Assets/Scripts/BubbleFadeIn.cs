using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleFadeIn : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void BubbleDestroyed();
    public event BubbleDestroyed OnBubbleDestroyed;

    private SpriteRenderer spriteRenderer;
    public float fadeDuration = 1f; // Duração do efeito fade-in

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        Color color = spriteRenderer.color;
        color.a = 0; // Começa completamente transparente
        spriteRenderer.color = color;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0, 0.7f, elapsedTime / fadeDuration); // Interpolação da transparência
            spriteRenderer.color = color;
            elapsedTime += Time.deltaTime;
            yield return null; // Espera o próximo frame
        }

        color.a = 0.7f; // Garante que a opacidade final seja 1
        spriteRenderer.color = color;
    }

    private void OnDestroy()
    {
        // Chama o evento quando a bolha é destruída
        OnBubbleDestroyed?.Invoke();
    }
}

