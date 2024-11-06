using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public EnemySpawn enemySpawn;
    public EnemyStats[] enemyStatsArray;
    public Movimento movimento;
    public GameObject textPrefab;
    //
    public TMP_Text[] vidaText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        if(enemySpawn.enemySpawned==true)
        {
            GameObject textObject = new GameObject("Vida Inimigo");
            vidaText[] = textObject.AddComponent<TextMeshProUGUI>();
            vidaText.transform.SetParent(transform);

            enemySpawn.enemySpawned = false;
        }
    }*/
}
