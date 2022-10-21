using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotepadController : MonoBehaviour
{

    string NOTEPAD_FILE_PATH;
    
    void Start()
    {
        NOTEPAD_FILE_PATH = $"{Application.dataPath}/CPP_Applications/narration.txt";
        System.Diagnostics.Process.Start("notepad.exe", NOTEPAD_FILE_PATH);
    }
}
