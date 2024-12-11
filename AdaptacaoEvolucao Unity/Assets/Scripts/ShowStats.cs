using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowStats : MonoBehaviour
{
    public TMP_Text[] caracteristicas;
    public Movimento movimento;
    public int criatura;
    public GameObject criaturaObject;
    private bool alreadyExecuted = false;
    public GameObject bocaPlayer;
    OnTriggerBoca bocaScript;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (alreadyExecuted == false)
        {
            criaturaObject = GameObject.FindGameObjectWithTag("Criatura" + criatura);
            movimento = criaturaObject.GetComponent<Movimento>();

            foreach (Transform filho in criaturaObject.transform)
            {
                if (filho.CompareTag("BocaPlayer"))
                {
                    bocaPlayer = filho.gameObject;
                    break;
                }
            }
            bocaScript = bocaPlayer.GetComponent<OnTriggerBoca>();
            alreadyExecuted = true;
        }
            
        float calor = (movimento.heatResistance - 36f);
        caracteristicas[0].text = "Visão:\n" + movimento.visao;
        caracteristicas[1].text = "Dano:\n" + bocaScript.dano;
        caracteristicas[2].text = "Velocidade:\n" + movimento.speed;
        caracteristicas[3].text = "Tolerancia frio:\n" + movimento.coldResistance;
        caracteristicas[4].text = "Tolerancia calor:\n" + calor;
        caracteristicas[5].text = "Gasto energetico:\n" + movimento.fomeConsumoI;
    }

}
