using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    private Transform inimigo;
    public GameObject inimigoObject;
    public float offsetX = 100f;
    public float offsetY = 200f;
    void Start()
    {
        if (inimigoObject != null)
        {
            inimigo = inimigoObject.transform;
        }
    }

    void Update()
    {
    
    }
    void LateUpdate()
    {
         if (inimigoObject == null)
            {
                Destroy (gameObject);
            }
            if(inimigoObject != null)
            {
                RectTransform rt = GetComponent<RectTransform>();
                Vector3 screenPoint = Camera.main.WorldToScreenPoint(inimigo.position);

                screenPoint.x += offsetX;
                screenPoint.y += offsetY;

                rt.position = screenPoint;
            }
    }
}
