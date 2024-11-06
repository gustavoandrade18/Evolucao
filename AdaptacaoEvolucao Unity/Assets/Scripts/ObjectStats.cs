using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStats : MonoBehaviour
{
    // Start is called before the first frame update
    private Movimento movimento;
    private EnemyStats enemyStats;
    public float speed;
    public float tempo;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(tempo <= 1f)
        {
            tempo += Time.deltaTime;
        }
        if(tempo >= 0.1f && tempo<=0.5f)
        {
        MonoBehaviour parentScript = null;
        if (transform.parent.tag == "Player")
        {
            parentScript = transform.parent.GetComponent<Movimento>();
        }
        else if (transform.parent.tag == "Inimigo")
        {
            parentScript = transform.parent.GetComponent<EnemyStats>();
        }

        // Verifica se este objeto tem a tag "Rabo" e se encontrou um script
        if (CompareTag("Rabo") && parentScript != null)
        {
            SetSpeed(parentScript);
            Destroy(this); // Destrói o script após modificar a velocidade
        }
            
        }
    }

    void SetSpeed(MonoBehaviour myScript)
    {
        if (myScript is Movimento)
        {
            Movimento movScript = (Movimento)myScript;
            movScript.speed = speed;
            movScript.walkSpeed = speed;
            movScript.sprintSpeed = speed * 2;
        }
        else if (myScript is EnemyStats)
        {
            EnemyStats enemyScript = (EnemyStats)myScript;
            enemyScript.speed = speed;
            enemyScript.walkSpeed = speed;
            enemyScript.sprintSpeed = speed * 2;
        }
}
}


