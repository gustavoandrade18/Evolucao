using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FasesUi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnGameOver()
    {
        SceneManager.LoadScene("Selecao de criatura 1");
        Time.timeScale = 1;
    }
}
