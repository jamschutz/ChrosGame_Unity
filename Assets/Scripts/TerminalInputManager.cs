using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TerminalInputManager : MonoBehaviour
{
    public PlayerController playerController;

    private string INPUT_FILE_PATH;
    private System.DateTime lastUpdateTime;

    System.Diagnostics.Process terminalProcess;

    void Start()
    {
        // run input manager
        terminalProcess = System.Diagnostics.Process.Start($"{Application.dataPath}/CPP_Applications/Windows/inputManager.exe");
        INPUT_FILE_PATH = $"{Application.dataPath}/CPP_Applications/Windows/_communications/input.txt";
        lastUpdateTime = System.DateTime.Now;
    }


    void Update()
    {
        if(GotNewInput()) {
            var input = System.IO.File.ReadAllText(INPUT_FILE_PATH);
            
            if(input.ToLower().Trim() == "go forward") {
                Debug.Log("going forward...");
                playerController.SetMoveState(PlayerController.MoveState.Forward);
            }
            else if(input.ToLower().Trim() == "go back") {
                Debug.Log("going backward...");
                playerController.SetMoveState(PlayerController.MoveState.Backward);
            }
            else if(input.ToLower().Trim() == "go left") {
                Debug.Log("going left...");
                playerController.SetMoveState(PlayerController.MoveState.Left);
            }
            else if(input.ToLower().Trim() == "go right") {
                Debug.Log("going right...");
                playerController.SetMoveState(PlayerController.MoveState.Right);
            }
            else if(input.ToLower().Trim() == "turn right") {
                Debug.Log("turning right...");
                playerController.SetTurnState(PlayerController.TurnState.Right);
            }
            else if(input.ToLower().Trim() == "turn left") {
                Debug.Log("turning left...");
                playerController.SetTurnState(PlayerController.TurnState.Left);
            }
            else if(input.ToLower().Trim() == "stop") {
                Debug.Log("stopping...");
                playerController.SetMoveState(PlayerController.MoveState.None);
                playerController.SetTurnState(PlayerController.TurnState.None);
            }
        }
    }


    void OnApplicationQuit()
    {
        // Close process by sending a close message to its main window.
        terminalProcess.CloseMainWindow();
        // Free resources associated with process.
        terminalProcess.Close();
    }


    bool GotNewInput()
    {
        if(File.GetLastWriteTime(INPUT_FILE_PATH) > lastUpdateTime) {
            lastUpdateTime = System.DateTime.Now;
            return true;
        }
        return false;
    }
}
