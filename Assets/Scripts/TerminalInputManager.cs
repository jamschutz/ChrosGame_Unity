using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TerminalInputManager : MonoBehaviour
{

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
            }
            else if(input.ToLower().Trim() == "go back") {
                Debug.Log("going backward...");
            }
            else if(input.ToLower().Trim() == "go left") {
                Debug.Log("going left...");
            }
            else if(input.ToLower().Trim() == "go right") {
                Debug.Log("going right...");
            }
            else if(input.ToLower().Trim() == "turn right") {
                Debug.Log("turning right...");
            }
            else if(input.ToLower().Trim() == "turn left") {
                Debug.Log("turning left...");
            }
            else if(input.ToLower().Trim() == "stop") {
                Debug.Log("stopping...");
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
