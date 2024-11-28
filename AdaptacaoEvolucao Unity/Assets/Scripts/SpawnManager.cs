using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject objetoPrefab; // O prefab que será spawnado
    public GameObject gelo;
    public float intervaloSpawn = 5.0f; // Intervalo entre spawns
    public float raioSpawn = 10.0f; // Raio dentro do qual os objetos serão spawnados
    public Transform pontoCentro; // Ponto central ao redor do qual os objetos serão spawnados

    private float tempoDesdeUltimoSpawn;
    public int foodOnScreen;
    public int foodLimit;
    public int geloLimit;
    public int geloOnScreen;
    public float geloCooldown;
    public float geloDesdeUltimoSpawn;
    public bool foodDestroyed;

    void Start()
    {
        tempoDesdeUltimoSpawn = 0f; // Inicializa o tempo desde o último spawn
    }

    void Update()
    {
        if(pontoCentro == null)
        {
            pontoCentro = GameObject.FindGameObjectWithTag("Player").transform;
        } 
        // Incrementa o tempo desde o último spawn
        tempoDesdeUltimoSpawn += Time.deltaTime;
        geloDesdeUltimoSpawn += Time.deltaTime;

        // Verifica se é o momento de spawnar um novo objeto
        if(foodLimit > foodOnScreen )
        {
            if (tempoDesdeUltimoSpawn >= intervaloSpawn)
            {
                SpawnObjeto(objetoPrefab);
                tempoDesdeUltimoSpawn = 0f; // Reseta o tempo desde o último spawn
            }
            if (foodDestroyed)
            {
                SpawnObjeto(objetoPrefab);
                foodDestroyed = false;
            }
        }
        else 
        {
            tempoDesdeUltimoSpawn =0f;
        }
        
        if(geloLimit > geloOnScreen)
        {
            if (geloDesdeUltimoSpawn >= geloCooldown)
            {
                SpawnObjeto(gelo);
                geloDesdeUltimoSpawn = 0f; // Reseta o tempo desde o último spawn
            }
        }
        else 
        {
            geloDesdeUltimoSpawn =0f;
        }
    }

    void SpawnObjeto(GameObject prefab)
    {
        // Gera uma posição aleatória dentro de um quadrado com o lado igual ao dobro do raio
        Vector2 randomOffset = Random.insideUnitCircle * raioSpawn;

        // Calcula a posição final de spawn
        Vector2 spawnPosition = pontoCentro ? (Vector2)pontoCentro.position + randomOffset : (Vector2)transform.position + randomOffset;

        // Instancia o prefab no ponto de spawn
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
