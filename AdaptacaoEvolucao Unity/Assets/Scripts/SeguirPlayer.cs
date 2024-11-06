using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirPlayer : MonoBehaviour
{
    public GameObject player; // Referência ao Transform do player
    public Vector3 offset;    // Distância fixa entre a câmera e o player

    void Start()
    {
        // Inicializa o offset com a distância atual entre a câmera e o player
        offset = new Vector3(0, 0, -10);
    }

    void LateUpdate()
    { 
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        } 
       Vector3 newPosition = player.transform.position + offset;
       transform.position = newPosition;
    }
}
