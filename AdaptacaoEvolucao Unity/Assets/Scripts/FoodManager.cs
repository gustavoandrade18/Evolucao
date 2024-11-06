using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    private float tempo;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();

        spawnManager.foodOnScreen += 1;
    }

    // Update is called once per frame
    void Update()
    {
        tempo+= Time.deltaTime;

        if(tempo> 50f)
        {
            Destroy(gameObject);
            spawnManager.foodOnScreen -= 1;
        }
    }
}
