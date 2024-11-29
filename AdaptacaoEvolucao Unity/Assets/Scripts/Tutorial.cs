using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using FMODUnity;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TMP_Text tutorialText;
    public int textoQuantidade;
    float tempo;
    //
    [SerializeField] EvolucaoEntreCenas salva;
    //
    [SerializeField] private EventReference click;
    private FMOD.Studio.EventInstance clickAudio;
    // Start is called before the first frame update
    void Start()
    {
        if(salva.fase == 0)
        {
            salva.fase = 1;
        }
        clickAudio = RuntimeManager.CreateInstance(click);
    }

    // Update is called once per frame
    void Update()
    {
        tempo += Time.deltaTime;
        if(salva.fase == 1)
        {
            TutorialFase1();
        }
        else if (salva.fase == 2)
        {
            TutorialFase2();
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
            tutorialText.text = "Cada criatura tem diferentes caracteristicas que são obtidas aleatoriamente";
        }
        else if(textoQuantidade == 2)
        {
            tutorialText.text = "(clique em cima de uma das criaturas para ver suas caracteristicas)";
        }
        else if(textoQuantidade == 3)
        {
            tutorialText.text = "Nenhuma criatura é superior a outra, cada uma tem caracteristicas unicas que a diferem das outras";
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
        if(textoQuantidade == 0)
        {
            tutorialPanel.SetActive(true);
            tutorialText.text = "Bem vindo a fase 2!";
        }
        else if(textoQuantidade == 1)
        {
            tutorialText.text = "Agora que sua criatura evoluiu, ela esta mais complexa, como você pode ver novas huds foram adicionados a sua tela";
        }
        else if(textoQuantidade == 2)
        {
            tutorialText.text = "A barra de azul é a barra de oxigenio, para correr e morder você gasta oxigenio \n (Aperte shit para começar e parar de correr) \n (Aperte espaço para morder)";
        }
        else if(textoQuantidade == 3)
        {
            tutorialText.text = "Administre bem seu oxigenio, pois você precisara dele para fugir e perseguir outras criaturas";
        }
        else if(textoQuantidade == 4)
        {
            tutorialText.text = "A barra com azul e vermelho mesclado é a barra de temperatura, se ela ficar baixa ou alta, você perderá energia mais rapidamente";
        }
        else if(textoQuantidade == 5)
        {
            tutorialText.text = "Caso a temperatura chege no minimo ou no maximo, você recebera dano até morrer ou ajustar sua temperatura";
        }
        else if(textoQuantidade == 6)
        {
            tutorialPanel.SetActive(false);
        }
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
}
