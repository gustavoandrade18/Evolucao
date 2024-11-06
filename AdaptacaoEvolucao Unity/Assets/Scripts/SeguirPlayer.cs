using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirPlayer : MonoBehaviour
{
    public Transform player;  // Referência ao Transform do player
    public Vector3 offset;    // Distância fixa entre a câmera e o player

    void Start()
    {
        // Inicializa o offset com a distância atual entre a câmera e o player
        offset = new Vector3(0, 0, -10);
    }

    void LateUpdate()
    {
        // Atualiza a posição da câmera, mantendo o offset
        transform.position = player.position + offset;

        // Opcional: manter a rotação da câmera fixa (por exemplo, a rotação inicial)
        transform.rotation = Quaternion.identity; // Mantém a rotação zerada
    }
}
