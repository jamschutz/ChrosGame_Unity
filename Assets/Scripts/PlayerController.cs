using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum MoveState { Forward, Backward, Left, Right, None }
    public enum TurnState { Left, Right, None }

    [Header("Movement")]
    public float moveSpeed;
    public float turnSpeed;

    [Header("Game Objects")]
    public GameObject idle;
    public GameObject forward;
    public GameObject backward;


    private MoveState moveState;
    private TurnState turnState;

    private void Start()
    {
        SetMoveState(MoveState.None);
        SetTurnState(TurnState.None);
    }


    private void Update()
    {
        // handle move....
        void Move(Vector3 direction) {
            transform.Translate(direction * Time.deltaTime, Space.World);
        }
        switch(moveState) {
            case MoveState.Forward:
                Move(transform.forward);
                break;
            case MoveState.Left:
                Move(-transform.right);
                break;
            case MoveState.Right:
                Move(transform.right);
                break;
            case MoveState.Backward:
                Move(-transform.forward);
                break;
            case MoveState.None:
                // do nothing...
                break;
            default:
                Debug.LogError($"unknown move state!!! {moveState.ToString()}");
                break;
        }


        // handle turn...
        void Rotate(float angle) {
            transform.Rotate(new Vector3(0, angle * Time.deltaTime, 0), Space.World);
        }
        switch(turnState) {
            case TurnState.Left:
                Rotate(-turnSpeed);
                break;
            case TurnState.Right:
                Rotate(turnSpeed);
                break;
            case TurnState.None:
                // do nothing...
                break;
            default:
                Debug.LogError($"unknown turn state!!! {turnState.ToString()}");
                break;
        }
    }



    public void SetMoveState(MoveState move)
    {
        // turn off all objects
        idle.SetActive(false);
        forward.SetActive(false);
        backward.SetActive(false);

        moveState = move;
        switch(moveState) {
            case MoveState.Forward:
            case MoveState.Left:
            case MoveState.Right:
                forward.SetActive(true);
                break;
            case MoveState.Backward:
                backward.SetActive(true);
                break;
            case MoveState.None:
                idle.SetActive(true);
                break;
            default:
                Debug.LogError($"unknown move state!!! {move.ToString()}");
                break;
        }
    }



    public void SetTurnState(TurnState turn)
    {
        turnState = turn;
        switch(turnState) {
            case TurnState.Left:
                break;
            case TurnState.Right:
                break;
            case TurnState.None:
                break;
            default:
                Debug.LogError($"unknown turn state!!! {turn.ToString()}");
                break;
        }
    }
}
