using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TerminalInputManager : MonoBehaviour
{

    private string INPUT_FILE_PATH;
    private System.DateTime lastUpdateTime;

    void Start()
    {
        // run input manager
        System.Diagnostics.Process.Start($"{Application.dataPath}/CPP_Applications/Windows/inputManager.exe");
        INPUT_FILE_PATH = $"{Application.dataPath}/CPP_Applications/Windows/_communications/input.txt";
        lastUpdateTime = System.DateTime.Now;
    }


    void Update()
    {
        if(GotNewInput()) {
            var input = System.IO.File.ReadAllText(INPUT_FILE_PATH);
            Debug.Log(input);
        }
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
