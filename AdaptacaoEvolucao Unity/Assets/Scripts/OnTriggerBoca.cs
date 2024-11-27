using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

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
    public float cooldown;
    public bool fugidinha;
    public float fugidinhaCooldown;
    public GameObject player;
    float start=0f;
    public bool quebraGelo = false;

    /*void SetPlayer(GameObject player) {
        this.player = player; 
    }*/

    
    // Start is called before the first frame update
    void Start()
    {
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
        cooldown += Time.deltaTime;
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
            tempo += Time.deltaTime;
            attack=false;
        }
        if(attack==true && tempo>=0.1)
        {
            attack=false;
            tempo=0;
            boxCollider.size = boxColliderI;
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
                    movimento.exp += 5;
                }
                Destroy(other.gameObject);
            }
        } 
        if (other.CompareTag("Planta") && carnivoro == false)
        {
            if(attack == true)
            {
                if(transform.parent.CompareTag("Player"))
                {
                    movimento.fome +=  alimento;
                    movimento.exp += 5;
                }
                Destroy(other.gameObject);
                spawnManager.foodOnScreen -= 1;
                attack = false;
            }
        } 

        if (other.CompareTag("Player") && transform.parent.CompareTag("Inimigo") && cooldown > 2.0f)
        {
            cooldown =0;
            fugidinha = true;
            movimento.vida -= dano;
            movimento.speed = movimento.sprintSpeed * 1.2f;
        } 

    }
}
