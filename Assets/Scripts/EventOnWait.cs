using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnWait : MonoBehaviour
{
    public UnityEvent event_to_trigger;
    public float wait_time;

    float counter;

    void Start()
    {
        counter = 0f;
    }


    void Update()
    {
        counter += Time.deltaTime;
        if(counter > wait_time)
            event_to_trigger.Invoke();
    }


    public void OnSetWaitTime(float new_wait_time)
    {
        wait_time = Mathf.Max(0, wait_time - new_wait_time);
    }
}
