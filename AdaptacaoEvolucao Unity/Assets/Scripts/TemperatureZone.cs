using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TemperatureZone : MonoBehaviour
{
    public Movimento movimento;
    public bool entrou;
    public float temperaturaLocal;
    private float tempo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (entrou==true)
        {
            tempo += Time.deltaTime;
            //maior que 25 fome aumentada
            //maior que 35 começa a perder vida
            //menor que 10 fome aumentada
            //menos que 0 coemça a perder vida
            if(temperaturaLocal-movimento.heatResistance > movimento.temperatura && tempo >= 2)
            {
                movimento.temperatura += 2;
            }
            if(temperaturaLocal + movimento.coldResistance < movimento.temperatura && tempo >= 2)
            {
                movimento.temperatura -= 2;
            }
            if (tempo >= 2)
            {
                tempo = 0;
            }
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            entrou = true;
            // Obtém o GameObject do "Player"
            movimento = other.GetComponent<Movimento>();
        }
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            entrou = false;
            tempo=0;
        }
    }
}
