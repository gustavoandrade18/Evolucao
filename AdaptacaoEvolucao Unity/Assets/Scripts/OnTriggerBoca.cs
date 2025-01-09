using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class OnTriggerBoca : MonoBehaviour
{

    public Movimento movimento;
    private float alimento =25f;
    public SpawnManager spawnManager;
    public bool attack;
    public float sizeIncrease =2f;
    public BoxCollider2D boxCollider;
    public UnityEngine.Vector2 boxColliderI;
    float tempo=0f;
    public bool carnivoro = true;
    public EnemyStats enemyStats;
    public float dano = 0;
    public bool fugidinha;
    public float fugidinhaCooldown;
    public GameObject player;
    float start=0f;
    public bool quebraGelo = false;
    public bool recebeMais= false;
    private bool alrAttacked = false;
    [SerializeField] private EventReference mordida;
    private FMOD.Studio.EventInstance mordidaAudio;
    [SerializeField] private EventReference mordidaInimigo;
    private FMOD.Studio.EventInstance mordidaInimigoAudio;
    [SerializeField] private EventReference heart;
    private FMOD.Studio.EventInstance heartAudio;
    //
    [SerializeField] private Animator mordeu;
    /*void SetPlayer(GameObject player) {
        this.player = player; 
    }*/

    
    // Start is called before the first frame update
    void Start()
    {
        mordidaAudio = RuntimeManager.CreateInstance(mordida);
        heartAudio = RuntimeManager.CreateInstance(heart);
        if(recebeMais == true)
        {
            alimento = 35f;
        }
        if (transform.parent.CompareTag("Inimigo"))
        {
            // Obtém o script EnemyStats do objeto pai
            enemyStats = transform.parent.GetComponent<EnemyStats>();
        }
        if(enemyStats != null)
        {
            enemyStats.carnivoro = carnivoro;
        }

      if (boxCollider != null)
        {
            // Armazena o tamanho do BoxCollider2D
            boxColliderI = boxCollider.size;
        }
        spawnManager = FindObjectOfType<SpawnManager>();
    }
    void LateStart()
    {
        if(start> 0.2f)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if(player != null)
            {
                movimento = player.GetComponent<Movimento>();
                boxCollider = GetComponent<BoxCollider2D>();
                boxColliderI = boxCollider.size;
            }
        }
        start += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(start<0.5f)
        {
            LateStart();
        }
        if (fugidinha==true)
        {
            fugidinhaCooldown += Time.deltaTime;
        }
        if(movimento != null)
        {
            if(fugidinhaCooldown >= 1.0f && movimento.sprint == true)
            {
                movimento.speed = movimento.sprintSpeed;
                fugidinha =false;
                fugidinhaCooldown = 0;
            }
            else if(fugidinhaCooldown >= 1.0f && movimento.sprint == false)
            {
                movimento.speed = movimento.walkSpeed;
                fugidinha =false;
                fugidinhaCooldown = 0;
            }
        }

        if(attack==true)
        {
            if(tempo == 0 && movimento.sprint == true)
            {
                movimento.speed = movimento.sprintSpeed * 1.4f;
            }
            if(tempo == 0 && movimento.sprint == false)
            {
                movimento.speed = movimento.walkSpeed * 1.4f;
            }
            tempo += Time.deltaTime;
        }
        if(attack==true && tempo>=0.2f)
        {
            attack=false;
            tempo=0;
            boxCollider.size = boxColliderI;
            //
            if(movimento.sprint == true)
            {
                movimento.speed = movimento.sprintSpeed;
            }
            if(movimento.sprint == false)
            {
                movimento.speed = movimento.walkSpeed;
            }
        }
        if(alrAttacked && spawnManager.cooldown > 1f)
        {
            boxCollider.size = boxColliderI;
            alrAttacked = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Carne") && carnivoro == true)
        {
            if(attack == true)
            {
                if(transform.parent.CompareTag("Player"))
                {
                    movimento.fome +=  alimento;
                    movimento.vida += movimento.vidaTotal/25f;
                    movimento.exp += 5;
                    Destroy(other.gameObject);
                }
            }
        } 
        if (other.CompareTag("Planta") && carnivoro == false)
        {
            if(attack == true)
            {
                if(transform.parent.CompareTag("Player"))
                {
                    movimento.fome +=  alimento;
                    movimento.vida += movimento.vidaTotal/25f;
                    movimento.exp += 5;
                }
                Destroy(other.gameObject);
                spawnManager.foodOnScreen -= 1;
                attack = true;
            }
        } 

        if (other.CompareTag("Player") && transform.parent.CompareTag("Inimigo") && spawnManager.cooldown > 2.0f)
        {
            mordidaInimigoAudio = RuntimeManager.CreateInstance(mordidaInimigo);
            mordeu.SetTrigger("Mordeu");
            mordidaInimigoAudio.start();
            heartAudio.start();
            boxCollider.size -= new Vector2(1f, 1f);
            spawnManager.cooldown = 0;
            fugidinha = true;
            movimento.vida -= dano;
            movimento.speed = movimento.sprintSpeed * 1.2f;
            alrAttacked = true;
        } 

    }
 
}
