using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SpeciesAlly : MonoBehaviour
{
    public Canvas canvas;
    public Vector2 targetPosition;
    public float targetRange=30.0f;
    public float velocidadeRotacao=5.0f;
    public float speed = 10f;
    Movimento movimento;
    public GameObject player;
    float distanciaMinima=10f;
    float tempo;
    public TMP_Text textoPrefab;
    TMP_Text allyText;
    FollowGameObject followGameObject;
    NpcSpawn npcSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
        // Canvas
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        allyText = Instantiate(textoPrefab);
        allyText.transform.SetParent(canvas.transform, false);
        followGameObject = allyText.GetComponent<FollowGameObject>();
        followGameObject.inimigoObject =  this.gameObject;
        allyText.text = "Aliado";

        targetPosition = Random.insideUnitCircle.normalized;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            movimento = player.GetComponent<Movimento>();
        }
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < distanciaMinima && movimento.reproduzir == true && movimento.level >= 1)
        {
            tempo += Time.deltaTime;

            Vector2 playerPosition = player.transform.position;
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            Vector2 directionToTarget = playerPosition - position;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            angle -= 90;

            // Rotaciona o objeto para o ângulo calculado no eixo Z
            float rotationZ = Mathf.LerpAngle(transform.eulerAngles.z, angle, velocidadeRotacao * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, rotationZ);

            if(tempo >= 5f)
            {
                SceneManager.LoadScene("Selecao de criatura 1");
            }
        }
        else
        {
            tempo=0;
            Wander();
        }
    }

    void Wander()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 directionToTarget = targetPosition - position;

        // Calcula o ângulo desejado em relação ao alvo
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        angle -= 90;

        // Rotaciona o objeto para o ângulo calculado no eixo Z
        float rotationZ = Mathf.LerpAngle(transform.eulerAngles.z, angle, velocidadeRotacao * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        // Move o objeto na direção do alvo
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Verifica se o objeto alcançou o alvo
        if (Vector3.Distance(transform.position, targetPosition) < 1.5f)
        {
            targetPosition = new Vector3(Random.Range(-targetRange, targetRange) + player.transform.position.x ,Random.Range(-targetRange, targetRange)+ player.transform.position.y ,0.0f);
        } 
    }
}
