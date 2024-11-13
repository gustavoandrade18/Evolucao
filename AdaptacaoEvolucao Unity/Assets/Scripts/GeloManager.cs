using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeloManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    private float tempo;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();

        spawnManager.geloOnScreen += 1;
    }

    // Update is called once per frame
    void Update()
    {
        tempo+= Time.deltaTime;

        if(tempo> 50f)
        {
            spawnManager.geloOnScreen -= 1;
            Destroy(gameObject);
        }
    }
}
