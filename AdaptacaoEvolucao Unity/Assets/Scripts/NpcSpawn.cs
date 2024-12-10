using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawn : MonoBehaviour
{
    public int inimigoCarnivoro=0;
    public int inimigoHerbivoro=0;
    public int aliado=0;
    public float tempoSpawnCarnivoro;
    public float tempoSpawnHerbivoro;

    //Gera as caracteristicas dos inimigos
    public GameObject[] prefabsInimigos;
    public GameObject[] prefabsRabo; // Array de prefabs do rabo
    public GameObject[] prefabsBocaCarnivora; // Array de prefabs da boca
    public GameObject[] prefabsBocaHerbivora;
    public GameObject[] prefabsOlhos; // Array de prefabs dos olhos
    public GameObject[] prefabsGuelras; // Array de prefabs das guelras
    bool carnivoro = true;

    public string childRaboNome = "Rabo"; // Nome do objeto filho que será substituído para o rabo
    public string childBocaNome = "Boca"; // Nome do objeto filho que será substituído para a boca
    public string childOlhosNome = "Olhos"; // Nome do objeto filho que será substituído para os olhos
    public string childGuelrasNome = "Guelras"; // Nome do objeto filho que será substituído para as guelras

    public GameObject player;
    public float cooldownSpawn =10f;

    public bool enemySpawned=false;
    public bool allySpawned;
    public bool gerarCaracteristicas = true;

    GameObject prefabBoca;
    //
    EnemyStats enemyStats;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        } 

        if(inimigoCarnivoro < 2)
        {
            tempoSpawnCarnivoro += Time.deltaTime;
            if(tempoSpawnCarnivoro>cooldownSpawn)
            {
                carnivoro = true;
                SpawnInimigo();
                inimigoCarnivoro++;
                tempoSpawnCarnivoro=0f;
            }
        }
        if(inimigoHerbivoro < 2)
        {
            tempoSpawnHerbivoro += Time.deltaTime;
            if(tempoSpawnHerbivoro>cooldownSpawn/2f)
            {
                carnivoro = false;
                SpawnInimigo(); 
                inimigoHerbivoro++;
                tempoSpawnHerbivoro=0f;
            }
        }

    }

    public void SpawnInimigo()
    {
        // Escolhe um prefab aleatório de inimigo
        int randomIndex = Random.Range(0, prefabsInimigos.Length);
        GameObject novoInimigo = Instantiate(prefabsInimigos[randomIndex], GetRandomSpawnPosition(), Quaternion.identity);
        enemyStats = novoInimigo.GetComponent<EnemyStats>();
        enemyStats.enabled = true;

        // Modifica as características do novo inimigo
        if(gerarCaracteristicas == true)
        {
            ModificarCaracteristicas(novoInimigo);
        }
    }

    void ModificarCaracteristicas(GameObject inimigo)
    {
        // Seleciona um prefab aleatório para a boca
        
        if(carnivoro == true)
        {
            int bocaIndex = Random.Range(0, prefabsBocaCarnivora.Length);
            prefabBoca = prefabsBocaCarnivora[bocaIndex];
        }
        else if(carnivoro == false)
        {
            int bocaIndex = Random.Range(0, prefabsBocaHerbivora.Length);
            prefabBoca = prefabsBocaHerbivora[bocaIndex];
        }

        // Seleciona um prefab aleatório para os olhos
        int olhoIndex = Random.Range(0, prefabsOlhos.Length);
        GameObject prefabOlhos = prefabsOlhos[olhoIndex];

        // Seleciona um prefab aleatório para as guelras
        int guelrasIndex = Random.Range(0, prefabsGuelras.Length);
        GameObject prefabGuelras = prefabsGuelras[guelrasIndex];

        // Seleciona um prefab aleatório para o rabo
        int raboIndex = Random.Range(0, prefabsRabo.Length);
        GameObject prefabRabo = prefabsRabo[raboIndex];

        // Encontra o Transform dos objetos filhos
        Transform childBoca = inimigo.transform.Find(childBocaNome);
        Transform childOlhos = inimigo.transform.Find(childOlhosNome);
        Transform childGuelras = inimigo.transform.Find(childGuelrasNome);
        Transform childRabo = inimigo.transform.Find(childRaboNome);

        // Substitui as características
        ReplaceChild(childBoca, prefabBoca);
        ReplaceChild(childOlhos, prefabOlhos);
        ReplaceChild(childGuelras, prefabGuelras);
        ReplaceChild(childRabo, prefabRabo);
    }

    void ReplaceChild(Transform child, GameObject prefab)
    {
        if (child != null)
        {
            // Salva a posição e a rotação do filho
            Vector3 position = child.position;
            Quaternion rotation = child.rotation;

            // Destrói o objeto filho atual
            Destroy(child.gameObject);

            // Instancia o novo prefab
            GameObject newChild = Instantiate(prefab, position, rotation);
            // Define o novo prefab como filho do objeto inimigo
            newChild.transform.SetParent(child.parent);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Gera uma direção aleatória dentro de um círculo em um plano 2D
        Vector2 randomDirection = Random.insideUnitCircle * 80f;
        return new Vector3(randomDirection.x, randomDirection.y,0) + player.transform.position; // Use Y como 0 se necessário
    }
}
