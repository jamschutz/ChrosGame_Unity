using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnTrigger : MonoBehaviour
{
    public string targetTag = "Player";
    public bool onlyTriggerOnce = true;
    public UnityEvent eventToTrigger;
    public UnityEvent eventOnExit;


    void OnTriggerEnter(Collider other)
    {
        // if not who we're looking for, bounce out
        if(other.tag != targetTag) return;

        eventToTrigger.Invoke();

        if(onlyTriggerOnce)
            Destroy(this.gameObject);
    }


    void OnTriggerExit(Collider other)
    {
        // if not who we're looking for, bounce out
        if(other.tag != targetTag) return;

        eventOnExit.Invoke();

    }
}
