using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class SelecionarButton : MonoBehaviour
{
    [SerializeField] EvolucaoEntreCenas salva;
    public ModificadorCaracteristicas caracteristicas;

    [SerializeField] private EventReference click;
    private FMOD.Studio.EventInstance clickAudio;
    // Start is called before the first frame update
   public void Selecionar1()
   {
      clickAudio = RuntimeManager.CreateInstance(click);
      clickAudio.start();
      salva.rabo = caracteristicas.raboNumero[0];
      salva.boca = caracteristicas.bocaNumero[0];
      salva.olhos = caracteristicas.olhoNumero[0];
      salva.guelras = caracteristicas.guelrasNumero[0];
      salva.corpo = caracteristicas.corpoNumero[0];
      SceneManager.LoadScene("Teste de Mecanicas");

   }
    public void Selecionar2()
   {
      clickAudio = RuntimeManager.CreateInstance(click);
      clickAudio.start();
      salva.rabo = caracteristicas.raboNumero[1];
      salva.boca = caracteristicas.bocaNumero[1];
      salva.olhos = caracteristicas.olhoNumero[1];
      salva.guelras = caracteristicas.guelrasNumero[1];
      salva.corpo = caracteristicas.corpoNumero[1];
      SceneManager.LoadScene("Teste de Mecanicas");
   }
    public void Selecionar3()
   {
      clickAudio = RuntimeManager.CreateInstance(click);
      clickAudio.start();
      salva.rabo = caracteristicas.raboNumero[2];
      salva.boca = caracteristicas.bocaNumero[2];
      salva.olhos = caracteristicas.olhoNumero[2];
      salva.guelras = caracteristicas.guelrasNumero[2];
      salva.corpo = caracteristicas.corpoNumero[2];
      SceneManager.LoadScene("Teste de Mecanicas");
   }
}
