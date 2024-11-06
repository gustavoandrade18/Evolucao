using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Level1Tutorial : MonoBehaviour
{
    public float tempo=0f;
    public GameObject menuPanel;
    GameObject player;
    Movimento movimento;
    public TMP_Text tutorialText; 
    public int textoQuantidade;
    float reproducao;
    //variaveis para recuperar valores do movimento
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movimento = player.GetComponent<Movimento>();
    }

    // Update is called once per frame
    void Update()
    {
        Tutorial();

        if(movimento.reproduzir && movimento.level >= 1) 
        {
            
            reproducao += Time.deltaTime;
            if(reproducao >= 3f)
            {
                SceneManager.LoadScene("Selecao de criatura 1");
            }
        }
        else if(movimento.reproduzir == false)
        {
            reproducao = 0f;
        }
    }
     public void OnAttackPress (InputValue value)
    {
       
            if(tempo >= 2.5f && textoQuantidade != 7) 
            {
                tempo = 1.5f;
                textoQuantidade ++;
            }
    }
    public void Tutorial()
    {
        tempo += Time.deltaTime;
        if(tempo >= 1.5f)
        {

            if(textoQuantidade==0)
            {
                menuPanel.SetActive(true);
                tutorialText.text = "Bem vindo a Adaptação à Evolução, eu sou o tutorial e vou te ensinar o basico sobre o jogo.";
                movimento.enabled = false;
            }
            if(textoQuantidade==1)
            {
                tutorialText.text = "Para se locomover, use WASD, ou o joystick direcional caso esteja em um dispositivo movel.";
                movimento.enabled = true;
            }
            if(textoQuantidade == 2)
            {
                tutorialText.text = "Sua criatura ainda é muito simples, então a forma no qual você se alimenta é simples, quimiossintese é o nome desse processo de alimentação.";
            }
            if(textoQuantidade == 3)
            {
                tutorialText.text = "A quimiossíntese é o processo pelo qual organismos utilizam substâncias químicas inorgânicas para produzir energia e compostos orgânicos, sem a necessidade de luz solar.";
            }
            if(textoQuantidade == 4)
            {
                tutorialText.text = "segure espaço ou o botão de ataque para realizar a quimiosintesse";
            }
            if(textoQuantidade == 5)
            {
                tutorialText.text = "Com o passar do tempo, e toda vez que você se alimenta, sua cratura ganha experiencia";
            }
            if(textoQuantidade == 6)
            {
                tutorialText.text = "Quando experiencia o suficiente for obtida, sua criatura passará para o proximo estagio da vida";
            }
            if(textoQuantidade == 7)
            {
               menuPanel.SetActive(false);
            }
            if(textoQuantidade == 8)
            {
               menuPanel.SetActive(true);
               tutorialText.text = "Sua criatura cresceu, parabens!";
            }
            if(textoQuantidade == 9)
            {
               tutorialText.text = "Agora que sua criatura cresceu, ela pode se reproduzir, dando origem a novas gerações";
            }
            if(textoQuantidade == 10)
            {
               tutorialText.text = "Sua criatura se reproduz de forma assexuada, então por enquanto ela não precisa de outra criatura para se reproduzir";
            }
            if(textoQuantidade == 11)
            {
               tutorialText.text = "Segure = ou o botão de reprodução para se reproduzir";
            }
            if(movimento.level>=1 && textoQuantidade <=7)
            {
                textoQuantidade = 8;
            }
            
        }
    }
}

