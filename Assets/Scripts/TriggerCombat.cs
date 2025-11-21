using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{

   [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;
    AudioSource source;
    Collider2D soundTrigger;

    void Awake() {
        source = GetComponent<AudioSource>();
        soundTrigger = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        onTriggerEnter.Invoke();
        source.Play();
      
    }
           void OnTriggerExit2D(Collider2D other)
    {
       
        onTriggerEnter.Invoke();
    }
    
    
}
