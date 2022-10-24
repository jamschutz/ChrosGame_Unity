using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitForPlayerToStop : MonoBehaviour
{
    public UnityEvent eventOnSuccess;
    public bool onlyOnce = true;

    PlayerController playerController;
    bool isWaitingForPlayer;

    void Start()
    {
        isWaitingForPlayer = false;
    }


    void Update()
    {
        if(!isWaitingForPlayer) return;

        if(playerController.GetMoveState() != PlayerController.MoveState.None) return;

        eventOnSuccess.Invoke();
        if(onlyOnce) Destroy(this);
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") {
            Debug.Log($"got collision: {other.name}");
            playerController = other.GetComponent<PlayerController>();
            isWaitingForPlayer = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player") {
            isWaitingForPlayer = false;
        }
    }
}
