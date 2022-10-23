using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NotepadController : MonoBehaviour
{
    public string filename;

    string NOTEPAD_FILE_PATH;
    System.Diagnostics.Process notepad;
    
    void Start()
    {
        NOTEPAD_FILE_PATH = $"{Application.dataPath}/TextFiles/{filename}.txt";
        ShowNotepadOuput("TEST");
    }


    public void Speak(string text)
    {
        ShowNotepadOuput(text);
    }


    void OnApplicationQuit()
    {
        // Close process by sending a close message to its main window.
        notepad.CloseMainWindow();
        // Free resources associated with process.
        notepad.Close();
    }


    void CloseNotepad()
    {
        if(notepad == null) return;

        try {
            // Close process by sending a close message to its main window.
            notepad.CloseMainWindow();
            // Free resources associated with process.
            notepad.Close();
        }
        catch {
            // do nothing
        }
    }


    void ShowNotepadOuput(string text)
    {
        if(notepad != null) {
            CloseNotepad();
        }

        File.WriteAllText(NOTEPAD_FILE_PATH, text);
        notepad = System.Diagnostics.Process.Start("notepad.exe", NOTEPAD_FILE_PATH);
    }
}
