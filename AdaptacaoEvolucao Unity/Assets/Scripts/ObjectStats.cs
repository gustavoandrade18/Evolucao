using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ObjectStats : MonoBehaviour
{
    // Start is called before the first frame update
    private Movimento movimento;
    private EnemyStats enemyStats;
    public float speed;
    public int oxigenioMax;
    public float tempo;
    public float visao;
    Camera mainCamera;
    public float oxigenioInimigo;
    public float enemyDetection;
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
            if (transform.parent.tag == "Player" || transform.parent.tag == "Criatura0" || transform.parent.tag == "Criatura1" || transform.parent.tag == "Criatura2" )
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
            if (CompareTag("Guelra") && parentScript != null)
            {
                SetOxigen(parentScript);
                Destroy(this); // Destrói o script após modificar o oxigenio
            }
            if (CompareTag("Olho") && parentScript != null)
            {

                SetVision(parentScript);
                Destroy(this);
                
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
            enemyScript.partesStatus = true;
        }
    }
    void SetOxigen(MonoBehaviour myScript)
    {
        if (myScript is Movimento)
        {
            Movimento movScript = (Movimento)myScript;
            movScript.oxigenioMax = oxigenioMax;

        }
        else if (myScript is EnemyStats)
        {
            EnemyStats enemyScript = (EnemyStats)myScript;
            enemyScript.oxigenioMax = oxigenioInimigo;
            enemyScript.partesStatus = true;
        }
    }
    void SetVision(MonoBehaviour myScript)
    {
        if (myScript is Movimento)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName != "Selecao de criatura 1")
            {
                mainCamera = Camera.main;
                mainCamera.orthographicSize = visao;
            }
            Movimento movScript = (Movimento)myScript;
            movScript.visao = visao;
        }
        else if (myScript is EnemyStats)
        {
            EnemyStats enemyScript = (EnemyStats)myScript;
            enemyScript.detectionRange = enemyDetection;
            enemyScript.partesStatus = true;
        }
    }
}


