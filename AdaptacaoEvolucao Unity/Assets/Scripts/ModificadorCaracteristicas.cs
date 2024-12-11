using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificadorCaracteristicas : MonoBehaviour
{
// Start is called before the first frame update
public GameObject[] prefabsRabo; // Array de prefabs do rabo
public GameObject[] prefabsBoca; // Array de prefabs da boca
public GameObject[] prefabsOlhos; // Array de prefabs dos olhos
public GameObject[] prefabsGuelras; // Array de prefabs das guelras

public string childRaboNome = "Rabo"; // Nome do objeto filho que será substituído para o rabo
public string childBocaNome = "Boca"; // Nome do objeto filho que será substituído para a boca
public string childOlhosNome = "Olhos"; // Nome do objeto filho que será substituído para os olhos
public string childGuelrasNome = "Guelras"; // Nome do objeto filho que será substituído para as guelras

private GameObject[] criatura = new GameObject[3];
// Seleciona um dos GameObjects criatura
public int criaturaNumber = 0; 
// Salva o numero que foi sorteado para a criatura
public int[] bocaNumero = new int[3];
public int[] raboNumero = new int[3];
public int[] olhoNumero = new int[3];
public int[] guelrasNumero = new int[3];
//spawna o corpo 
public GameObject[] prefabsCorpo; // Array para armazenar os prefabs com nome "Corpo"
public int[] corpoNumero = new int [3];

public string[] tags; // Array para armazenar as tags correspondentes

public Vector3[] posicoes; // Array para armazenar as posições correspondentes
int i = 0;

    void Start()
    {
        while(criaturaNumber <= 2)
        {
            CriaCorpo();
        // Seleciona um prefab aleatório para o rabo
        int randomIndex = Random.Range(0, prefabsRabo.Length);
        raboNumero[criaturaNumber] = randomIndex;
        GameObject prefabRabo = prefabsRabo[randomIndex];
        // Seleciona um prefab aleatório para a boca
        randomIndex = Random.Range(0, prefabsBoca.Length);
        bocaNumero[criaturaNumber] = randomIndex;
        GameObject prefabBoca = prefabsBoca[randomIndex];

        // Seleciona um prefab aleatório para os olhos
        randomIndex = Random.Range(0, prefabsOlhos.Length);
        olhoNumero[criaturaNumber] = randomIndex;
        GameObject prefabOlhos = prefabsOlhos[randomIndex];

        // Seleciona um prefab aleatório para as guelras
        randomIndex = Random.Range(0, prefabsGuelras.Length);
        guelrasNumero[criaturaNumber] = randomIndex;
        GameObject prefabGuelras = prefabsGuelras[randomIndex];

        // Encontra o GameObject com a tag "Criatura"
        criatura[criaturaNumber] = GameObject.FindGameObjectWithTag("Criatura" + criaturaNumber);

        if (criatura[criaturaNumber] != null)
        {
            
            // Encontra o Transform dos objetos filhos
            Transform childRabo = criatura[criaturaNumber].transform.Find(childRaboNome);
            Transform childBoca = criatura[criaturaNumber].transform.Find(childBocaNome);
            Transform childOlhos = criatura[criaturaNumber].transform.Find(childOlhosNome);
            Transform childGuelras = criatura[criaturaNumber].transform.Find(childGuelrasNome);

            // Substitui o rabo
            ReplaceChild(childRabo, prefabRabo);
        
            // Substitui a boca
            ReplaceChild(childBoca, prefabBoca);
        
            // Substitui os olhos
            ReplaceChild(childOlhos, prefabOlhos);
        
            // Substitui as guelras
            ReplaceChild(childGuelras, prefabGuelras);

        }
        Movimento movimento = criatura[criaturaNumber].GetComponent<Movimento>();
        movimento.fomeConsumoI += (float)olhoNumero[criaturaNumber] / 4 + (float)raboNumero[criaturaNumber] / 4 + (float)bocaNumero[criaturaNumber] / 4;
        criaturaNumber++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
            // Define o novo prefab como filho do GameObject player
            newChild.transform.SetParent(criatura[criaturaNumber].transform);
        }
    }
    void CriaCorpo()
    {
            int indiceAleatorio = Random.Range(0, prefabsCorpo.Length);
            corpoNumero[i] = indiceAleatorio;
            // Verifica se o nome do prefab começa com "Corpo"
            if (prefabsCorpo[indiceAleatorio] != null)
            {
                // Instancia o prefab, define a posição e a tag
                GameObject instancia = Instantiate(prefabsCorpo[indiceAleatorio], posicoes[i], Quaternion.identity);
                instancia.tag = tags[i];
            }
            i++;
    }
}