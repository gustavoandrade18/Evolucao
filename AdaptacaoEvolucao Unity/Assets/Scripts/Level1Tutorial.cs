using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using FMODUnity;
public class Level1Tutorial : MonoBehaviour
{
    public float tempo=0f;
    public GameObject menuPanel;
    GameObject player;
    Movimento movimento;
    public TMP_Text tutorialText; 
    public int textoQuantidade;
    float reproducao;
    //objeto que salva valores
    [SerializeField] EvolucaoEntreCenas salva;

    //audio
    [SerializeField] private EventReference click;
    private FMOD.Studio.EventInstance clickAudio;
    //
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        clickAudio = RuntimeManager.CreateInstance(click);
        player = GameObject.FindGameObjectWithTag("Player");
        movimento = player.GetComponent<Movimento>();
    }

    // Update is called once per frame
    void Update()
    {
        Tutorial();

        if(movimento.reproduzir == true && movimento.level >= 1 || reproducao >=2.2f) 
        {
            if(reproducao == 0f)
            {
                animator.SetBool("Reproduzir", true);
            }
            
            reproducao += Time.deltaTime;
            if(reproducao >= 3f)
            {
                SceneManager.LoadScene("Selecao de criatura 1");
                salva.fase = 1;
            }
        }
        else if(movimento.reproduzir == false && reproducao <= 2.2f)
        {
            reproducao = 0f;
            animator.SetBool("Reproduzir", false);
        }
    }
     public void OnTutorial (InputValue value)
    {
       
            if(tempo >= 2.5f && textoQuantidade != 7) 
            {
                tempo = 1.5f;
                textoQuantidade ++;
                clickAudio.start();
            }
    }
    public void Tutorial()
    {
        if(textoQuantidade != 7)
        {
            tempo += Time.unscaledDeltaTime;
        }
        if(tempo >= 1.5f)
        {

            if(textoQuantidade==0)
            {
                Time.timeScale =0;
                menuPanel.SetActive(true);
                tutorialText.text = "Bem vindo a Adaptação à Evolução, este é o tutorial que vai te ensinar o básico sobre o jogo.";
            }
            if(textoQuantidade==1)
            {
                Time.timeScale =1;
                tutorialText.text = "Para se locomover, use WASD, ou o joystick direcional caso esteja em um dispositivo móvel.";
            }
            if(textoQuantidade == 2)
            {
                Time.timeScale =0;
                tutorialText.text = "Sua criatura ainda é muito simples, então a forma no qual ela se alimenta é simples, quimiossíntese é o nome desse processo de alimentação.";
            }
            if(textoQuantidade == 3)
            {
                tutorialText.text = "A quimiossíntese é o processo pelo qual organismos utilizam substâncias químicas inorgânicas para produzir energia, sem a necessidade de luz solar.";
            }
            if(textoQuantidade == 4)
            {
                tutorialText.text = "(segure espaço ou o botão de ataque para realizar a quimiossíntese)";
            }
            if(textoQuantidade == 5)
            {
                tutorialText.text = "Com o passar do tempo, e toda vez que você se alimenta, sua criatura ganha experiência";
            }
            if(textoQuantidade == 6)
            {
                tutorialText.text = "Quando experiencia o suficiente for obtida, sua criatura passará para o próximo estágio da vida";
            }
            if(textoQuantidade == 7)
            {
               menuPanel.SetActive(false);
               Time.timeScale =1;
            }
            if(textoQuantidade == 8)
            {
                Time.timeScale =0;
               menuPanel.SetActive(true);
               tutorialText.text = "Sua criatura cresceu, parabéns!";
            }
            if(textoQuantidade == 9)
            {
               tutorialText.text = "Agora que sua criatura cresceu, ela pode se reproduzir, dando origem a novas criaturas que com o passar de várias gerações, terão novas características";
            }
            if(textoQuantidade == 10)
            {
               tutorialText.text = "Sua criatura se reproduz de forma assexuada, então por enquanto ela não precisa de outra criatura para se reproduzir, porém nas fases seguintes isso vai ser diferente";
            }
            if(textoQuantidade == 11)
            {
                tutorialText.text = "Segure E ou o botão de reprodução para se reproduzir";
            }
            if(textoQuantidade == 12)
            {
                menuPanel.SetActive(false);
                Time.timeScale =1;
            }

            if(movimento.level>=1 && textoQuantidade <=7)
            {
                textoQuantidade = 8;
            }
            
        }
    }
}

