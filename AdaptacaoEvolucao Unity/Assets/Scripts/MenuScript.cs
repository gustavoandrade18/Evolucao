using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using FMODUnity;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;
    GameObject menuManipulavel;
    public Canvas canvas;
    [SerializeField] EvolucaoEntreCenas salva;
    //
    [SerializeField] private EventReference click;
    private FMOD.Studio.EventInstance clickAudio;
    Movimento movimento;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        clickAudio = RuntimeManager.CreateInstance(click);
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        menuManipulavel = Instantiate(menu);
        menuManipulavel.transform.SetParent(canvas.transform, false);

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
        if(movimento.menuOn == true)
        {
            clickAudio.start();
            menuManipulavel.transform.SetAsLastSibling();
            menuManipulavel.SetActive(!menuManipulavel.activeSelf);
            if (menuManipulavel.activeSelf)
            {
            Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            movimento.menuOn = false;
        }
    }
    public void Selecao()
    {
        clickAudio.start();
        salva.fase -= 1;
        SceneManager.LoadScene("Selecao de criatura 1");
        Time.timeScale = 1;
    }
    public void Menu()
    {
        clickAudio.start();
        salva.fase = 0;
        SceneManager.LoadScene("Menu principal");
        Time.timeScale = 1;
    }

}
