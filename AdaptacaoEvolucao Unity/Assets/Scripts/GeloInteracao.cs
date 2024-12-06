using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class GeloInteracao : MonoBehaviour
{
    public OnTriggerBoca onTriggerBoca;
    public GameObject objetoPrefab; 
    private float vida = 10;
    public Transform pontoCentro;
    GameObject[] coldZones;
    //
    [SerializeField] private EventReference rachando;
    private FMOD.Studio.EventInstance rachandoAudio;
    [SerializeField] private EventReference quebrando;
    private FMOD.Studio.EventInstance quebrandoAudio;
    // Start is called before the first frame update
    void Start()
    {
        rachandoAudio = RuntimeManager.CreateInstance(rachando);
        quebrandoAudio = RuntimeManager.CreateInstance(quebrando);

        coldZones = GameObject.FindGameObjectsWithTag("ColdZone");
    }

    // Update is called once per frame
    void Update()
    {
        if(vida<=0)
        {
            if(objetoPrefab != null)
            {
                Vector2 pontoSpawn = pontoCentro.position;
                quebrandoAudio.start();
                Instantiate(objetoPrefab, pontoSpawn, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject bocaPlayer = GameObject.FindGameObjectWithTag("BocaPlayer");
        onTriggerBoca = bocaPlayer.GetComponent<OnTriggerBoca>();

        if (other.CompareTag("BocaPlayer"))
        {
            if(onTriggerBoca.attack == true && onTriggerBoca.quebraGelo)
            {
                rachandoAudio.start();
                vida -= onTriggerBoca.dano;
            }
        }
        
        if (other.CompareTag("HotZone") || other.CompareTag("NeutralZone"))
        {
            transform.position = coldZones[Random.Range(0, coldZones.Length)].transform.position + new Vector3(Random.Range(-10, +10), (Random.Range(-10, +10)),0);
        }
    }
}
