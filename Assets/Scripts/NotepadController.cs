using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;

public class NotepadController : MonoBehaviour
{
    public string filename;

    string NOTEPAD_FILE_PATH;
    System.Diagnostics.Process notepad;
    
    void Start()
    {
        NOTEPAD_FILE_PATH = $"{Application.dataPath}/TextFiles/{filename}.txt";
    }


    public void Speak(string text)
    {
        ShowNotepadOuput(text.Replace("[NEW_LINE]","\n"));
    }


    void OnApplicationQuit()
    {
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


    public void CloseNotepad()
    {
        Debug.Log("closing!");
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
        Debug.Log("opening!");
        if(notepad != null) {
            CloseNotepad();
        }

        File.WriteAllText(NOTEPAD_FILE_PATH, text);
        notepad = System.Diagnostics.Process.Start("notepad.exe", NOTEPAD_FILE_PATH);
        // StartCoroutine("KeepTryingToMoveWindow");
        // // try {
        // //     if(NotepadController.MoveWindow(notepad.MainWindowHandle, 0, 0, 0, 500, true)) {
        // //         Debug.Log("success!");
        // //     }     
        // //     else {
        // //         Debug.LogError($"unable to move notepad window {filename}: {Marshal.GetLastWin32Error()}...");
        // //     }
        // // }
        // // catch(System.Exception ex) {
        // //     Debug.LogError($"error moving notepad window: {ex.Message}");
        // // }
        
    }

    IEnumerator KeepTryingToMoveWindow()
    {
        Debug.Log("trying to move!");
        while(true) {
            Debug.Log("trying to move!");
            if(NotepadController.MoveWindow(notepad.MainWindowHandle, 0, 0, 0, 500, true)) {
                Debug.Log("success!");
                break;
            }     
            else {
                Debug.LogError($"unable to move notepad window {filename}: {Marshal.GetLastWin32Error()}...");
            }
            yield return null;
        }
        
    }
    
    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool MoveWindow(System.IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
}
