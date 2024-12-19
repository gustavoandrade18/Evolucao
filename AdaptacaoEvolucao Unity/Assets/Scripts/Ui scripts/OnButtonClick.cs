using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class OnButtonClick : MonoBehaviour
{
    public GameObject settings;
    [SerializeField] private EventReference click;
    private FMOD.Studio.EventInstance clickAudio;
    // Start is called before the first frame update
    void Start()
    {
        clickAudio = RuntimeManager.CreateInstance(click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        clickAudio.start();
        SceneManager.LoadScene("Fase 1");
    }
    public void opcoes()
    {
        clickAudio.start();
        settings.SetActive(true);
    }
    public void voltar()
    {
        clickAudio.start();
        settings.SetActive(false);
    }
     public void QuitGame()
    {
        clickAudio.start();
        Application.Quit();
        Debug.Log("o jogo foi fechado");
    }
}
