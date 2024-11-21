using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using FMODUnity;

public class Movimento : MonoBehaviour
{
    //variaveis fase 1
    float tempo2;
    public bool quimiosintesse = false;
    //
    public float speed = 10.0f;
    public float velocidadeRotacao= 1.0f;
    private Vector2 move;
    private float multiplicadorRotacao = 1;
    public float rotacaoMultiplica;
    public float sprintSpeed;
    public float walkSpeed =0.0f;
    float tempoNado=5f;
    //variaveis do void Playerstats (oxigenio)
    public bool sprint =false;
    public float oxigenioMax;
    private float oxigenio;
    public int oxigenioConsumo = 2;
    public int oxigenioRegenera = 1;
    private float tempo;
    public int oxigenioPattack;
    public float visao;
    //variaveis do void Playerstats (vida)
    public float vidaTotal=100.0f;
    public float vida;
    public float vidaAcressimo = 20f;
    //Varieaveis imagem
    public Slider vidaImageInstance;
    public Slider vidaImage;
    public Slider oxigenioImage;
    public Slider fomeImage;
    public Slider expImage;
    public Slider temperatureImage;
    public TMP_Text gameOver;
    //variaveis do void Playerstats (fome)
    public float fome;
    private float fomeTotal = 100.0f;   
    public float fomeConsumo;
    public float fomeConsumoI;
    //variaveis da temperatura
    public float temperatura;
    public float coldResistance;
    public float heatResistance;
    //Boca
    public OnTriggerBoca onTriggerBoca;
    public float dano;
    private GameObject bocaPlayer; 
    //varivaveis de experiencia
    public float exp;
    public float expMax;
    float expTime;
    public float expPassiveGain;
    public string[] fase = new string[3]{"Filhote", "Jovem", "Adulto"};
    public int level = 0;
    public TMP_Text expText;
    public TMP_Text levelText;
    private CapsuleCollider2D capsuleCollider;
    //Interação de reproduzir
    public bool reproduzir;
    public float minDistance = 10f;
    //canvas
    public Canvas canvas;
    //Sons
    [SerializeField] private EventReference quimio;
    private FMOD.Studio.EventInstance quimioAudio;
    [SerializeField] private EventReference nado;
    private FMOD.Studio.EventInstance nadoAudio;
    // animacao
    [SerializeField] private Animator animator;
    //efeitos
    public GameObject fumacaPrefab;
    private GameObject fumaca;


    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        quimioAudio = RuntimeManager.CreateInstance(quimio);
        nadoAudio = RuntimeManager.CreateInstance(nado);

        BarraManager(ref vidaImage);
        BarraManager(ref oxigenioImage);
        BarraManager(ref fomeImage);
        BarraManager(ref expImage);
        BarraManager(ref temperatureImage);

        gameOver = Instantiate(gameOver);
        gameOver.transform.SetParent(canvas.transform, false);
        levelText = Instantiate(levelText);
        levelText.transform.SetParent(canvas.transform, false);
        expText = expImage.transform.Find("Exp").GetComponent<TMP_Text>();
        //
        walkSpeed = speed;
        sprintSpeed = speed * 2;
        oxigenio = oxigenioMax;
        vida = vidaTotal;
        fome = fomeTotal;
        fomeConsumo = fomeConsumoI;
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {  
        if(reproduzir == true)
        {
            RotateTowardsAlly();
        }
        tempo += Time.deltaTime;
        PlayerStats();
        Move();
        Temperature();
        if (tempo >= 1)
        {
            tempo = 0;
        }
        Exp();
        if(quimiosintesse == true)
        {
            tempo2 += Time.deltaTime;
            if(tempo2 >= 2f)
            {
                fumaca = Instantiate(fumacaPrefab, transform.position, Quaternion.identity);
                Destroy(fumaca, 0.5f);

                quimioAudio.start();
                fome += 15;
                exp += 15;
                tempo2 = -1;
            }
        }
    }

    //Status da criatura
    void PlayerStats ()
    {
        //oxigenio
        if (oxigenio < oxigenioConsumo)
        {
            speed = walkSpeed;
            sprint = false;
        }
        if(sprint == true && tempo >=1)
        {
            oxigenio = oxigenio - oxigenioConsumo;
        }
        else if (sprint == false && tempo >=1 && oxigenio <= oxigenioMax-1)
        {
            oxigenio = oxigenio + oxigenioRegenera;
        }
        if(oxigenio > oxigenioMax)
        {
            oxigenio = oxigenioMax;
        }
        if(oxigenioImage != null)
        {
           oxigenioImage.value = oxigenio/oxigenioMax;
        }

        //vida
        if(vidaImage != null)
        {
            vidaImage.value = vida/100f;
        }
        if(vida <=0)
        {
            Time.timeScale = 0;
            if(gameOver != null)
            {
                gameOver.text = "Fim de Jogo";
                gameOver.gameObject.SetActive(true);
            }
        }
         if (Input.GetKeyUp(KeyCode.Minus))
         {
            vida = vida-1;
         }
        //fome
         if(fomeImage != null)
        {
            fomeImage.value = fome/fomeTotal;
        }
        if (fome > fomeTotal)
        {
            fome = fomeTotal;
        }
        if(fome <= 0 && tempo >= 1)
        {
            vida -= 1;
            fome =0;
        }
        else if (fome > fomeTotal/2 && vida < vidaTotal && tempo >= 1) 
        {   
            if(temperatura > 0 && temperatura < 35)
            {
                vida += 1;
            }
        }
          
          
          if (tempo >= 1 && fome >0)
        {
            fome = fome - fomeConsumo;
        }
    }
    void Temperature()
    {
        //maior que 25 fome aumentada
        //maior que 35 começa a perder vida
        //menor que 10 fome aumentada
        //menos que 0 coemça a perder vida
        if(temperatureImage != null)
        {
            temperatureImage.value = temperatura/35;
        }
        if (temperatura >= 25)
        {
            fomeConsumo = fomeConsumoI * 2;
        }
        if (temperatura >= 35 && tempo >=1)
        {
            vida -= 1;
        }
         if (temperatura <= 10)
        {
            fomeConsumo = fomeConsumoI * 2;
        }
        if (temperatura <= 0 && tempo >=1)
        {
            vida -= 1;
        }
    }

    //Movimentacao da criatura
    void Move()
    {
        tempoNado += Time.deltaTime;
         // Obtém a direção para onde o personagem está se movendo ou olhando
        Vector2 direcao = new Vector2(move.x, move.y);
        
        if (direcao.magnitude > 0.1f) // Verifica se há uma direção válida
        {
            // Calcula o ângulo de rotação baseado na direção
            float anguloAlvo = Mathf.Atan2(-direcao.x, direcao.y) * Mathf.Rad2Deg;

            // Interpola a rotação atual para a rotação alvo
            float anguloAtual = transform.eulerAngles.z;
            float anguloSuave = Mathf.LerpAngle(anguloAtual, anguloAlvo, Time.deltaTime * velocidadeRotacao * multiplicadorRotacao);
            Vector2 direction = new Vector2(0,1);

            // Aplica a rotação
            if(move.x != 0 || move.y != 0)
            {
                if(tempoNado > 5f/(speed/walkSpeed))
                {
                    nadoAudio.start();
                    tempoNado = 0f;
                }
                transform.rotation = Quaternion.Euler(0, 0, anguloSuave);
                transform.Translate( direction * speed * Time.deltaTime);
            }
        }
    }
    void Exp()
    {
        expTime += Time.deltaTime;

        if(exp >= expMax && level < 3)
        {
            level++;
            exp=0;
            transform.localScale += new Vector3(0.1f,0.1f,0);
            capsuleCollider.size += new Vector2(0.1f,0.1f);
            if(onTriggerBoca != null)
            {
                onTriggerBoca.sizeIncrease += 0.1f;
            }
            
            vidaTotal += vidaAcressimo;
        }
        if (expTime >= 1 && exp <180 && level < 2) 
        {
            expTime = 0;
            exp += expPassiveGain;
        }
        if(levelText != null)
        {
            levelText.text = "Estagio: " + fase[level];
        }
        if(expText != null)
        {
            expText.text = "Exp:" + exp + "/" + expMax;
        }
        if(expImage != null)
        {
            expImage.value = exp/expMax;
        }

    }


    void RotateTowardsAlly()
    {
        // Encontrar o objeto mais próximo com a tag "Aliado"
        GameObject closestAlly = FindClosestAlly();

        if (closestAlly != null)
        {
            // Direção do objeto atual até o objeto mais próximo
            Vector2 direction = closestAlly.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;

            // Rotacionar suavemente o objeto até o ângulo do aliado mais próximo
            float rotationZ = Mathf.LerpAngle(transform.eulerAngles.z, angle, velocidadeRotacao * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        }
    }
    GameObject FindClosestAlly()
    {
        GameObject[] aliados = GameObject.FindGameObjectsWithTag("Aliado");
        GameObject closest = null;
        minDistance = 10f;

        foreach (GameObject aliado in aliados)
        {
            float distance = Vector3.Distance(transform.position, aliado.transform.position);
            if(distance < 10f)
            {
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = aliado;
                }
            }
        }

        return closest;
    }

    public void BarraManager(ref Slider imagem)
    {
        imagem = Instantiate(imagem);
        imagem.transform.SetParent(canvas.transform, false);
    }


    //player input
    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();   
    }
    public void OnSprint(InputValue value)
    {
        if(sprint == true)
        {
            oxigenio -= oxigenioConsumo;
            speed = walkSpeed;
            sprint = false;
        }
        else
        {
            speed = sprintSpeed;
            sprint = true;
        }
    }


    public void OnHold (InputValue value)
    {
        multiplicadorRotacao = rotacaoMultiplica;
    }
    public void OnRelease (InputValue value)
    {
        multiplicadorRotacao = 1;
    }
    public void OnAttackPress (InputValue value)
    {
        bocaPlayer = GameObject.FindGameObjectWithTag("BocaPlayer");
        if(bocaPlayer != null)
        {
            onTriggerBoca = bocaPlayer.GetComponent<OnTriggerBoca>();
        }
        
        if(oxigenio >= oxigenioPattack && onTriggerBoca != null)
        {
            onTriggerBoca.attack = true;
            onTriggerBoca.boxCollider.size += new Vector2(onTriggerBoca.sizeIncrease, onTriggerBoca.sizeIncrease);
            oxigenio -= oxigenioPattack;
        }
        if(onTriggerBoca == null)
        {
            quimiosintesse = true;
            animator.SetBool("Quimio", true);
        }
    }
    public void OnAttackRelease (InputValue value)
    {
        quimiosintesse = false;
        if(animator != null)
        {
            animator.SetBool("Quimio", false);
        }
        tempo2 = 0;
    }
    public void OnReproduzirPress (InputValue value)
    {
        reproduzir = true;
    }
    public void OnReproduzirRelease (InputValue value)
    {
       reproduzir = false;
    }
}