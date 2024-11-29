using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeraCaracteristicas : MonoBehaviour
{
    [SerializeField] EvolucaoEntreCenas salva;


    public string childRaboNome = "Rabo"; // Nome do objeto filho que será substituído para o rabo
    public string childBocaNome = "Boca"; // Nome do objeto filho que será substituído para a boca
    public string childOlhosNome = "Olhos"; // Nome do objeto filho que será substituído para os olhos
    public string childGuelrasNome = "Guelras"; // Nome do objeto filho que será substituído para as guelras

    public GameObject [] prefabsModelo;
    public GameObject[] prefabsRabo; // Array de prefabs do rabo
    public GameObject[] prefabsBoca; // Array de prefabs da boca
    public GameObject[] prefabsOlhos; // Array de prefabs dos olhos
    public GameObject[] prefabsGuelras; // Array de prefabs das guelras
    public GameObject[] prefabsModeloAlly;
    //
    GameObject prefabCorpo;
    GameObject prefabCorpoAlly;
    GameObject prefabRabo;
    GameObject prefabGuelras;
    GameObject prefabBoca;
    GameObject prefabOlhos;

    private GameObject player;
    Movimento movimento;
    NpcSpawn npcSpawn;
    GameObject ally;
    SpeciesAlly speciesAlly;
    public int allyQuantity;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Pega o script de spawn
        npcSpawn = GameObject.Find("SpawnManager").GetComponent<NpcSpawn>();
        //
    
        prefabCorpo = prefabsModelo[salva.corpo];
        prefabCorpoAlly = prefabsModeloAlly[salva.corpo];
        // Pega a carcteristica ja selocinada pelo player
        prefabRabo = prefabsRabo[salva.rabo];
        // Seleciona um prefab aleatório para o guelras
        prefabGuelras = prefabsGuelras[salva.guelras];
        // Seleciona um prefab aleatório para o boca
        prefabBoca = prefabsBoca[salva.boca];
        prefabBoca.tag = "BocaPlayer";
        // Seleciona um prefab aleatório para o olhos
        prefabOlhos = prefabsOlhos[salva.olhos];
        GameObject playerPrefab = Instantiate(prefabCorpo, new Vector3(0, 0, 0), Quaternion.identity);
        playerPrefab.tag = "Player";
        movimento = playerPrefab.GetComponent<Movimento>();
        movimento.enabled = true;
        
        player = GameObject.FindGameObjectWithTag("Player");


        if (player != null)
        {
            // Encontra o Transform dos objetos filhos
            Transform childRabo = player.transform.Find(childRaboNome);
            Transform childBoca = player.transform.Find(childBocaNome);
            Transform childOlhos = player.transform.Find(childOlhosNome);
            Transform childGuelras = player.transform.Find(childGuelrasNome);

            // Substitui o rabo
            ReplaceChild(childRabo, prefabRabo, player);
        
            // Substitui a boca
            ReplaceChild(childBoca, prefabBoca, player);
        
            // Substitui os olhos
            ReplaceChild(childOlhos, prefabOlhos, player);
        
            // Substitui as guelras
            ReplaceChild(childGuelras, prefabGuelras, player);

            movimento.fomeConsumoI += (float)salva.olhos / 2 + (float)salva.rabo / 2 + (float)salva.boca / 2;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(npcSpawn.allySpawned)
        {
            GameObject aliado = Instantiate(prefabCorpoAlly, GetRandomSpawnPosition(), Quaternion.identity);
            speciesAlly = aliado.GetComponent<SpeciesAlly>();
            aliado.tag = "Aliado";
            speciesAlly.enabled = true;
            Transform childRabo = aliado.transform.Find(childRaboNome);
            Transform childBoca = aliado.transform.Find(childBocaNome);
            Transform childOlhos = aliado.transform.Find(childOlhosNome);
            Transform childGuelras = aliado.transform.Find(childGuelrasNome);
            // Substitui o rabo
            ReplaceChild(childRabo, prefabRabo, aliado);
        
            // Substitui a boca
            ReplaceChild(childBoca, prefabBoca, aliado);
        
            // Substitui os olhos
            ReplaceChild(childOlhos, prefabOlhos,aliado);
        
            // Substitui as guelras
            ReplaceChild(childGuelras, prefabGuelras, aliado);
            
            if(allyQuantity >=2)
            {
                npcSpawn.allySpawned = false;
            }
            allyQuantity++;
        }
    }

    void ReplaceChild(Transform child, GameObject prefab, GameObject criatura)
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
            // Define o novo prefab como filho do GameObject player
            newChild.transform.SetParent(criatura.transform);
        }
    }
     Vector3 GetRandomSpawnPosition()
    {
        // Gera uma direção aleatória dentro de um círculo em um plano 2D
        Vector2 randomDirection = Random.insideUnitCircle * 150f;
        return new Vector3(randomDirection.x, randomDirection.y,0) + player.transform.position; // Use Y como 0 se necessário
    }
}
