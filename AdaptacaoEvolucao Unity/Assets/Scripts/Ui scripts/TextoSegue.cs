using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoSegue : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform jogador; // Referência ao jogador
    public float distanciaAcima = 2f; // Distância do texto acima do jogador
    public float distanciaLado = 2f;

    private RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jogador != null)
        {
            // Calcula a posição na tela onde o jogador deve estar
            Vector3 screenPos = Camera.main.WorldToScreenPoint(jogador.position);

            // Define a posição do texto, considerando a distância acima
            screenPos.y += distanciaAcima;
            screenPos.x += distanciaLado;

            // Define a posição do texto na tela, considerando a âncora do RectTransform
            rectTransform.anchoredPosition = screenPos;
        }
    }
}
