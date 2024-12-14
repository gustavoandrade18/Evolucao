using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;
    public Canvas canvas;
    [SerializeField] EvolucaoEntreCenas salva;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        menu = Instantiate(menu);
        menu.transform.SetParent(canvas.transform, false);

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Fechar()
    {
        Time.timeScale =1;
        menu.SetActive(false);
    }
    public void Selecao()
    {
        salva.fase -= 1;
        SceneManager.LoadScene("Selecao de criatura 1");
        Time.timeScale = 1;
    }
    public void Menu()
    {
        salva.fase = 0;
        SceneManager.LoadScene("Menu principal");
        Time.timeScale = 1;
    }

    public void OnMenu(InputValue value)
    {
        menu.transform.SetAsLastSibling();
        menu.SetActive(!menu.activeSelf);
        if (menu.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
