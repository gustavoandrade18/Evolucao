using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public Vector2 thresholdPosition = new Vector2(0, 0); // Posição de referência
    public float thresholdDistanceX = 95.0f; // Distância a partir da qual consideramos "passou" no eixo X
    public float thresholdDistanceY = 50.0f; // Distância a partir da qual consideramos "passou" no eixo Y
    public float randomOffsetRangeX = 20.0f; // Intervalo para o número aleatório no eixo X
    public float randomOffsetRangeY = 20.0f; // Intervalo para o número aleatório no eixo Y

    void Update()
    {
        // Verifica todos os objetos com a tag "Inimigo"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Inimigo");
        foreach (GameObject enemy in enemies)
        {
            // Calcula a distância do inimigo em relação à posição de referência
            float distanceX = Mathf.Abs(enemy.transform.position.x - thresholdPosition.x);
            float distanceY = Mathf.Abs(enemy.transform.position.y - thresholdPosition.y);

            // Verifica se a distância X ultrapassa o limite
            if (distanceX > thresholdDistanceX)
            {
                // Gera um número aleatório para o deslocamento em X
                float randomOffsetX = Random.Range(-randomOffsetRangeX, randomOffsetRangeX);
                // Reseta a posição do inimigo com deslocamento aleatório no eixo X
                enemy.transform.position = new Vector3(thresholdPosition.x + randomOffsetX, enemy.transform.position.y, enemy.transform.position.z);
            }

            // Verifica se a distância Y ultrapassa o limite
            if (distanceY > thresholdDistanceY)
            {
                // Gera um número aleatório para o deslocamento em Y
                float randomOffsetY = Random.Range(-randomOffsetRangeY, randomOffsetRangeY);
                // Reseta a posição do inimigo com deslocamento aleatório no eixo Y
                enemy.transform.position = new Vector3(enemy.transform.position.x, thresholdPosition.y + randomOffsetY, enemy.transform.position.z);
            }
        }

        // Verifica todos os objetos com a tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            // Calcula a distância do jogador em relação à posição de referência
            float distanceX = Mathf.Abs(player.transform.position.x - thresholdPosition.x);
            float distanceY = Mathf.Abs(player.transform.position.y - thresholdPosition.y);

            // Verifica se a distância X ultrapassa o limite
            if (distanceX > thresholdDistanceX)
            {
                // Gera um número aleatório para o deslocamento em X
                float randomOffsetX = Random.Range(-randomOffsetRangeX, randomOffsetRangeX);
                // Reseta a posição do jogador com deslocamento aleatório no eixo X
                player.transform.position = new Vector3(thresholdPosition.x + randomOffsetX, player.transform.position.y, player.transform.position.z);
            }

            // Verifica se a distância Y ultrapassa o limite
            if (distanceY > thresholdDistanceY)
            {
                // Gera um número aleatório para o deslocamento em Y
                float randomOffsetY = Random.Range(-randomOffsetRangeY, randomOffsetRangeY);
                // Reseta a posição do jogador com deslocamento aleatório no eixo Y
                player.transform.position = new Vector3(player.transform.position.x, thresholdPosition.y + randomOffsetY, player.transform.position.z);
            }
        }
         GameObject[] ally = GameObject.FindGameObjectsWithTag("Aliado");
        foreach (GameObject aliado in ally)
        {
            // Calcula a distância do jogador em relação à posição de referência
            float distanceX = Mathf.Abs(aliado.transform.position.x - thresholdPosition.x);
            float distanceY = Mathf.Abs(aliado.transform.position.y - thresholdPosition.y);

            // Verifica se a distância X ultrapassa o limite
            if (distanceX > thresholdDistanceX)
            {
                // Gera um número aleatório para o deslocamento em X
                float randomOffsetX = Random.Range(-randomOffsetRangeX, randomOffsetRangeX);
                // Reseta a posição do jogador com deslocamento aleatório no eixo X
                aliado.transform.position = new Vector3(thresholdPosition.x + randomOffsetX, aliado.transform.position.y, aliado.transform.position.z);
            }

            // Verifica se a distância Y ultrapassa o limite
            if (distanceY > thresholdDistanceY)
            {
                // Gera um número aleatório para o deslocamento em Y
                float randomOffsetY = Random.Range(-randomOffsetRangeY, randomOffsetRangeY);
                // Reseta a posição do jogador com deslocamento aleatório no eixo Y
                aliado.transform.position = new Vector3(aliado.transform.position.x, thresholdPosition.y + randomOffsetY, aliado.transform.position.z);
            }
        }
    }
}