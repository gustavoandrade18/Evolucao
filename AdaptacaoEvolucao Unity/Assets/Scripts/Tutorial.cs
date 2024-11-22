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
    }
    public void TutorialFase1()
    {
        if(textoQuantidade == 0)
        {
            tutorialPanel.SetActive(true);
            tutorialText.text = "Esta é a seleção de criatura, aqui você vai poder selecionar qual criatura você vai controlar";
        }
        if(textoQuantidade == 1)
        {
            tutorialText.text = "Cada criatura tem diferentes caracteristicas que são obtidas aleatoriamente";
        }
        if(textoQuantidade == 2)
        {
            tutorialText.text = "(clique em cima de uma das criaturas para ver suas caracteristicas)";
        }
        if(textoQuantidade == 3)
        {
            tutorialText.text = "Nenhuma criatura é superior a outra, cada uma tem caracteristicas unicas que a diferem das outras";
        }
        if(textoQuantidade == 4)
        {
            tutorialText.text = "Contudo, para conseguir sobreviver ela precisa estar adaptada ao ambiente, então escolha com cuidado";
        }
        if(textoQuantidade == 5)
        {
            tutorialPanel.SetActive(false);
        }
    }

    public void OnAttackPress (InputValue value)
    {
       
            if(tempo >= 2.5f && textoQuantidade != 5) 
            {
                tempo = 1.5f;
                textoQuantidade ++;
                clickAudio.start();
            }
    }
}
