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
    public LayerMask groundedLayer;

    [Header("Game Objects")]
    public GameObject idle;
    public GameObject forward;
    public GameObject backward;


    // ----- private vars ----------- //
    private CharacterController controller;
    private MoveState moveState;
    private TurnState turnState;

    // physics helpers
    private const float MAX_IS_GROUNDED_DISTANCE = 100f;
    private float verticalVelocity;
    private bool isGrounded;
    private float heightOffset;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        verticalVelocity = 0;

        SetMoveState(MoveState.None);
        SetTurnState(TurnState.None);

        RaycastHit ground;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out ground, MAX_IS_GROUNDED_DISTANCE, groundedLayer)) {
            heightOffset = transform.position.y - ground.point.y;
        }
        else {
            Debug.Log("no ground :(");
        }
    }


    private void Update()
    {
        // handle move....
        void Move(Vector3 direction) {
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
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

        ApplyGravity();
    }



    public void SetMoveState(MoveState move)
    {
        // turn off all objects
        idle.SetActive(false);
        forward.SetActive(false);
        backward.SetActive(false);

        void RotateObjects(float angle) {
            idle.transform.localEulerAngles = new Vector3(0, angle, 0);
            forward.transform.localEulerAngles = new Vector3(0, angle, 0);
        }

        moveState = move;
        switch(moveState) {
            case MoveState.Forward:
                RotateObjects(0);
                forward.SetActive(true);
                break;
            case MoveState.Left:
                RotateObjects(-90);
                forward.SetActive(true);
                break;
            case MoveState.Right:
                RotateObjects(90);
                forward.SetActive(true);
                break;
            case MoveState.Backward:
                RotateObjects(0);
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



    private void ApplyGravity()
    {
        RaycastHit ground;
        if(Physics.Raycast(transform.position + Vector3.up, Vector3.down, out ground, MAX_IS_GROUNDED_DISTANCE, groundedLayer)) {
            transform.position = new Vector3(transform.position.x, ground.point.y + heightOffset, transform.position.z);
        }
    }
}
