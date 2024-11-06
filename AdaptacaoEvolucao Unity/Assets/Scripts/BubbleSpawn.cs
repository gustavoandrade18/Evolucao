using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawn : MonoBehaviour
{
 public GameObject bubblePrefab; // Prefab da bolha
    public float spawnRadiusX = 5f; // Raio máximo para spawn no eixo X
    public float spawnRadiusY = 3f; // Raio máximo para spawn no eixo Y
    public float maxDistanceX = 7f; // Distância máxima para destruir bolhas no eixo X
    public float maxDistanceY = 5f; // Distância máxima para destruir bolhas no eixo Y
    public float spawnInterval = 1f; // Intervalo entre os spawns
    public Transform player; // Referência ao jogador
    public LayerMask obstacleLayer; // Camada de obstáculos

    public int maxBubbles = 10; // Limite de bolhas ativas
    private int currentBubbleCount = 0; // Contador de bolhas ativas
    public float forwardSpawnFactor = 6f; // Fator para aumentar o spawn na direção do jogador

    private void Start()
    {
        InvokeRepeating(nameof(SpawnBubble), 0f, spawnInterval);
    }

    private void Update()
    {
        DestroyFarBubbles();
    }

    private void SpawnBubble()
    {
        // Verifica se o limite de bolhas foi alcançado
        if (currentBubbleCount >= maxBubbles)
        {
            return; // Retorna se o limite foi alcançado
        }

        Vector2 spawnPosition;

        // Tenta encontrar uma posição válida
        for (int attempts = 0; attempts < 10; attempts++)
        {
            // Calcula a direção que o jogador está olhando
            Vector2 forwardDirection = player.up; // Considerando que o jogador olha para cima

            float randomX = Random.Range(-spawnRadiusX, spawnRadiusX);
            float randomY = Random.Range(-spawnRadiusY, spawnRadiusY);

            // Aumenta a área de spawn na direção do olhar
            spawnPosition = new Vector2(
                player.position.x + randomX + forwardDirection.x * forwardSpawnFactor,
                player.position.y + randomY + forwardDirection.y * forwardSpawnFactor
            );

            // Verifica se a posição está livre
            if (!Physics2D.OverlapCircle(spawnPosition, 0.5f, obstacleLayer))
            {
                GameObject bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
                float randomSize = Random.Range(1f, 1.5f);
                bubble.transform.localScale = new Vector3(randomSize, randomSize, 1f);
                currentBubbleCount++; // Incrementa o contador de bolhas ativas // Registra a destruição
                break; // Sai do loop se uma posição válida for encontrada
            }
        }
    }

    private void DestroyFarBubbles()
    {
        foreach (GameObject bubble in GameObject.FindGameObjectsWithTag("Bubble"))
        {
            // Verifica se a bolha está muito longe do jogador e a destrói
            if (Mathf.Abs(bubble.transform.position.x - player.position.x) > maxDistanceX ||
                Mathf.Abs(bubble.transform.position.y - player.position.y) > maxDistanceY)
            {
                Destroy(bubble);
                currentBubbleCount--;
            }
        }
    }
}
