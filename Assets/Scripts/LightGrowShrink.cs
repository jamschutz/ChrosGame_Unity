using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGrowShrink : MonoBehaviour
{
    public float minRadius = 10;
    public float maxRadius = 14.5f;
    public float flickerTime = 2;
    public AnimationCurve flickerCurve;


    private Light light;
    [SerializeField]
    private float timer;
    private bool goingUp;

    private void Start()
    {
        light = GetComponent<Light>();
        timer = 0;
        goingUp = true;
    }


    private void Update()
    {
        light.range = Mathf.Lerp(minRadius, maxRadius, flickerCurve.Evaluate(timer / flickerTime));

        if(goingUp) {
            timer += Time.deltaTime;
            if(timer >= flickerTime) goingUp = false;
        }
        else {
            timer -= Time.deltaTime;
            if(timer <= 0) goingUp = true;
        }
    }
}
