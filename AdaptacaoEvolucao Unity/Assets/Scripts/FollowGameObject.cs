using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    private Transform inimigo;
    public GameObject inimigoObject;  // Referência ao GameObject inimigo
    public float offsetX = 100f;
    public float offsetY = 200f;
    private float tempo;  // Tempo para verificar a ausência do inimigo
    private Camera mainCamera;  // Referência à câmera principal

    void Start()
    {
        if (inimigoObject != null)
        {
            inimigo = inimigoObject.transform;  // Obtém o Transform do inimigo
        }

        // Tenta obter a câmera principal (tag "MainCamera")
        mainCamera = Camera.main;

        // Se a câmera principal não for encontrada, exibe um aviso
        if (mainCamera == null)
        {
            Debug.LogError("Câmera principal não encontrada. Certifique-se de que a câmera tenha a tag 'MainCamera'.");
        }
    }
    void Update()
    {
        // Se não encontrar o inimigo, começa a contagem do tempo
        if (inimigoObject == null)
        {
            tempo += Time.deltaTime;

            // Após 30 segundos sem encontrar o inimigo, destrói o objeto
            if (tempo >= 30f)
            {
                Destroy(gameObject);
            }
            else
            {
                // Caso não tenha encontrado, tenta buscar o jogador
                inimigoObject = GameObject.FindWithTag("Player");

                // Se encontrar o jogador, atualiza o inimigo
                if (inimigoObject != null)
                {
                    inimigo = inimigoObject.transform;
                }
            }
        }
    }
    void LateUpdate()
    {
        // Se o inimigo foi encontrado
        if (inimigoObject != null && mainCamera != null)
        {
            RectTransform rt = GetComponent<RectTransform>();  // Obtém o RectTransform do objeto atual
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(inimigo.position);  // Converte a posição mundial para a posição da tela

            // Aplica o deslocamento
            screenPoint.x += offsetX;
            screenPoint.y += offsetY;

            // Atualiza a posição do RectTransform
            rt.position = screenPoint;
        }
    }
}