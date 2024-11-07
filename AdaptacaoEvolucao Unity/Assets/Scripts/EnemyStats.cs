using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class EnemyStats : MonoBehaviour
{
    public Canvas canvas;
    public float vidaTotal=100.0f;
    public float vida;
    private int porcentagem;
    public TMP_Text vidaText;
    public OnTriggerBoca onTriggerBoca;
    public GameObject objetoPrefab;
    NpcSpawn npcSpawn;
    TMP_Text vidaPrefab;
    FollowGameObject followGameObject;
    OutOfBounds outOfBounds;
    //spawn da comida variavel
    float raioSpawn = 3.0f;
    public Transform pontoCentro;
    public int foodAmount = 3;
    //seguir ou fugir do player
    public bool carnivoro;
    private float invencibilidade;
    public GameObject player;
    public Vector2 playerDistance;
    public float maxDistance;
    public float speed= 10.0f;
    public float detectionRange = 1f;
    public float velocidadeRotacao=5.0f;
    private Vector2 move;
    Vector2 dectionVector;
    public float oxigenioMax;
    public float oxigenio;
    public float walkSpeed;
    public float sprintSpeed;
    float tempo = 0.0f;
    //anda aleatoriamente
    public Vector2 targetPosition;
    private Vector2 posicaoInicial;
    float targetRange=50.0f;
    public bool advancedBeheavior = true;
    // Start is called before the first frame update
    void Start()
    {   
        //Pega o script de spawn de criaturas 
        
        
        // Configura o texto canvas do inimigo
        /*canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        vidaPrefab = canvas.transform.Find("Vida").GetComponent<TMP_Text>();
        vidaText = Instantiate(vidaPrefab);
        vidaText.transform.SetParent(canvas.transform, false);
        followGameObject = vidaText.GetComponent<FollowGameObject>();
        followGameObject.inimigoObject =  this.gameObject;*/

        //
        player = GameObject.FindGameObjectWithTag("Player");
        vida = vidaTotal;
        EnemyStat();
        dectionVector = new Vector2(detectionRange,detectionRange);
        targetPosition = Random.insideUnitCircle.normalized;
        walkSpeed = speed;
        sprintSpeed = speed * 2;
        oxigenio = oxigenioMax;
    }

    // Update is called once per frame
    void Update()
    {   
        invencibilidade += Time.deltaTime;
        tempo += Time.deltaTime;
        EnemyStat();
        playerDistance = player.transform.position - transform.position;
        //foge ou persegue o jogador a depender da sua alimentação
        if(playerDistance.y * playerDistance.y <= dectionVector.y * dectionVector.y && playerDistance.x * playerDistance.x <= dectionVector.x * dectionVector.x && advancedBeheavior)
        {
            FollowPlayer();
            if(tempo>=1 && oxigenio >=1)
            {
                oxigenio -= 1;
                tempo=0;
            }
        }
       else if (Mathf.Abs(playerDistance.x) >= maxDistance && Mathf.Abs(playerDistance.y) >= maxDistance/0.56f)
       {
        npcSpawn = GameObject.Find("SpawnManager").GetComponent<NpcSpawn>();
        if(carnivoro == true)
        {
            npcSpawn.inimigoCarnivoro--;
        }
        else if(carnivoro == false)
        {
            npcSpawn.inimigoHerbivoro--;
        }
        Destroy(gameObject);
       }
        else
        {
            if(tempo>=1 && oxigenio < oxigenioMax)
            {
                oxigenio += 1;
                tempo=0;
            }
            Wander();
        }
    }

    //recebe o dano do jogador
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject bocaPlayer = GameObject.FindGameObjectWithTag("BocaPlayer");
        onTriggerBoca = bocaPlayer.GetComponent<OnTriggerBoca>();

        if (other.CompareTag("BocaPlayer"))
        {
            if(onTriggerBoca.attack == true && invencibilidade>=2)
            {
                vida -= onTriggerBoca.dano;
                invencibilidade=0;
            }
        }
    }
    //cuida dos status da criatura
    void EnemyStat()
    {
        porcentagem = Mathf.FloorToInt(vida/(vidaTotal/100));
        if (vidaText != null)
        {
            vidaText.text= "Vida:" + porcentagem + "%";
        }
        if(vida<=0)
        {
            npcSpawn = GameObject.Find("SpawnManager").GetComponent<NpcSpawn>();
            if(carnivoro == true)
            {
                npcSpawn.inimigoCarnivoro--;
            }
            else if(carnivoro == false)
            {
                npcSpawn.inimigoHerbivoro--;
            }
            while (foodAmount > 0 && onTriggerBoca.attack==false)
            {
                Vector2 randomOffset = Random.insideUnitCircle * raioSpawn;
                Vector2 spawnPosition = pontoCentro ? (Vector2)pontoCentro.position + randomOffset : (Vector2)transform.position + randomOffset;
                Instantiate(objetoPrefab, spawnPosition, Quaternion.identity);
                foodAmount--;
                if (foodAmount > 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    //se carnivoro, persegue o jogador
    void FollowPlayer()
    {
        Vector2 direcao = new Vector2(move.x, move.y);
        move = player.transform.position - transform.position;

        if (direcao.magnitude > 0.1f) // Verifica se há uma direção válida
        {
            if(carnivoro==false)
            {
                direcao.x = direcao.x * -1;
                direcao.y = direcao.y * -1;
            }
            // Calcula o ângulo de rotação baseado na direção
            float anguloAlvo = Mathf.Atan2(-direcao.x, direcao.y) * Mathf.Rad2Deg;
            // Interpola a rotação atual para a rotação alvo
            float anguloAtual = transform.eulerAngles.z;
            float anguloSuave = Mathf.LerpAngle(anguloAtual, anguloAlvo, Time.deltaTime * velocidadeRotacao);
            Vector2 direction = new Vector2(0,1);

            if(move.x != 0 || move.y != 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, anguloSuave);
                transform.Translate( direction * speed * Time.deltaTime);
            }
        }
        //se o inimigo tiver oxigenio, ele se movimentara mais rapido
        if (oxigenio>=1)
        {
            speed = sprintSpeed; 
        }
        else
        {
            speed = walkSpeed;
        }
    }
    void Wander()
    {
        speed = walkSpeed;
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
            targetPosition = new Vector3(Random.Range(-targetRange, targetRange),Random.Range(-targetRange, targetRange),0.0f);
        } 
        OutOfBoundsWander();
    }
    void OutOfBoundsWander()
    {
        outOfBounds = GameObject.Find("SpawnManager").GetComponent<OutOfBounds>();
        if(Mathf.Abs(targetPosition.x) > outOfBounds.thresholdDistanceX)
        {
            targetPosition.x = Random.Range(-targetRange, targetRange);
        }
        if(Mathf.Abs(targetPosition.y) > outOfBounds.thresholdDistanceY)
        {
            targetPosition.y = Random.Range(-targetRange, targetRange);
        }
    }
}
