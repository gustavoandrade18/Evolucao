using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using FMODUnity;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TMP_Text tutorialText;
    public int textoQuantidade;
    float tempo=0f;
    GameObject player;
    Movimento movimento;
    //
    [SerializeField] EvolucaoEntreCenas salva;
    //
    [SerializeField] private EventReference click;
    private FMOD.Studio.EventInstance clickAudio;
    //
    bool oneTimeScale;
    // Start is called before the first frame update
    void Start()
    {
        if(salva.fase == 0)
        {
            salva.fase = 1;
        }
        clickAudio = RuntimeManager.CreateInstance(click);
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Fase 5")
        {
            salva.fase = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                movimento = player.GetComponent<Movimento>();
            }
        }
        string currentSceneName = SceneManager.GetActiveScene().name;
        tempo += Time.unscaledDeltaTime;
        if(salva.fase == 1)
        {
            TutorialFase1();
        }
        else if (salva.fase == 2 && currentSceneName != "Selecao de criatura 1")
        {
            TutorialFase2();
        }
        else if (salva.fase == 5)
        {
            FimDeJogo();
        }
    }
    public void TutorialFase1()
    {
        if(textoQuantidade == 0)
        {
            tutorialPanel.SetActive(true);
            tutorialText.text = "Esta é a seleção de criatura, aqui você vai poder selecionar qual criatura você vai controlar";
        }
        else if(textoQuantidade == 1)
        {
            tutorialText.text = "Cada criatura tem diferentes características que são obtidas aleatoriamente";
        }
        else if(textoQuantidade == 2)
        {
            tutorialText.text = "(clique em cima de uma das criaturas para ver suas características)";
        }
        else if(textoQuantidade == 3)
        {
            tutorialText.text = "Nenhuma criatura é superior a outra, cada uma tem características únicas que a diferem das outras";
        }
        else if(textoQuantidade == 4)
        {
            tutorialText.text = "Contudo, para conseguir sobreviver ela precisa estar adaptada ao ambiente, então escolha com cuidado";
        }
        else if(textoQuantidade == 5)
        {
            tutorialPanel.SetActive(false);
        }
    }

    public void TutorialFase2()
    {
        if(movimento.level>=1 && textoQuantidade < 7)
        {
            textoQuantidade = 7;
        }
        if(textoQuantidade == 0 && tempo >= 5f)
        {
            tutorialPanel.SetActive(true);
            tutorialText.text = "Bem vindo a fase 2!";
            Time.timeScale=0;
        }
        else if(textoQuantidade == 1)
        {
            tutorialText.text = "Agora que sua criatura evoluiu, ela está mais complexa, agora outros huds ganharam importância na sua tela";
        }
        else if(textoQuantidade == 2)
        {
            tutorialText.text = "A barra de azul é a barra de oxigênio, para correr e morder você gasta oxigênio \n (Aperte shift para começar e parar de correr) \n (Aperte espaço para morder)";
        }
        else if(textoQuantidade == 3)
        {
            tutorialText.text = "Administre bem seu oxigênio, pois você precisará dele para fugir e perseguir outras criaturas";
        }
        else if(textoQuantidade == 4)
        {
            tutorialText.text = "A barra com azul e vermelho mesclado é a barra de temperatura, se ela ficar baixa ou alta, você perderá energia mais rapidamente";
        }
        else if(textoQuantidade == 5)
        {
            tutorialText.text = "Caso a temperatura chegue no mínimo ou no máximo, você receberá dano até morrer ou ajustar sua temperatura";
        }
        else if(textoQuantidade == 6)
        {
            tutorialPanel.SetActive(false);
            if (oneTimeScale == false)
            {
                Time.timeScale=1;
                oneTimeScale = true;
            }
        }
        else if(textoQuantidade == 7)
        {
            tutorialPanel.SetActive(true);
            Time.timeScale=0;
            oneTimeScale = false;
            tutorialText.text = "Agora que você cresceu, para progredir você precisa se reproduzir";
        }
        else if(textoQuantidade == 8)
        {
            tutorialText.text = "Para se reproduzir, chegue perto de um aliado e segure E";
        }
        else if(textoQuantidade == 9)
        {
           tutorialPanel.SetActive(false);
           if (oneTimeScale == false)
            {
                Time.timeScale=1;
                oneTimeScale = true;
            }
        }
    }
    public void FimDeJogo()
    {
        if(textoQuantidade == 0)
        {
            tutorialText.text = "Você terminou todas as fases de Adaptação à Evolução, Parabens!!!";
        }
        else if(textoQuantidade == 1)
        {
            tutorialText.text = "Agora você pode optar por recomeçar o jogo ou rejogar de forma aleatoria as fases anteriores";
        }
        else if(textoQuantidade == 2)
        {
            tutorialPanel.SetActive(false);
        }
    }

    public void Sair()
    {
        SceneManager.LoadScene("Menu principal");
    }

    public void OnTutorial(InputValue value)
    {
       
            if(tempo >= 2f && tutorialPanel.activeSelf == true) 
            {
                tempo = 1.5f;
                textoQuantidade ++;
                clickAudio.start();
            }
    }
    public void FaseAleatoria()
    {
        int randomNumber = Random.Range(2, 4);
        Debug.Log(randomNumber);
        SceneManager.LoadScene("Fase " + randomNumber);
    }
}
