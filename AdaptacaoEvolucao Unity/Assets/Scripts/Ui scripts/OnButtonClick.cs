using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnButtonClick : MonoBehaviour
{
    public GameObject settings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        SceneManager.LoadScene("Fase 1");
    }
    public void opcoes()
    {
       settings.SetActive(true);
    }
    public void voltar()
    {
       settings.SetActive(false);
    }
     public void QuitGame()
    {
        Application.Quit();
        Debug.Log("o jogo foi fechado");
    }
}
