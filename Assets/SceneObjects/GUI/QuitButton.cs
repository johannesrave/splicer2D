using UnityEditor;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitApp()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
        Application.Quit();
    }
}
