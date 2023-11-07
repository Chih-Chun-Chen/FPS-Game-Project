using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function to quit the application
    void Exit()
    {
        #if UNITY_EDITOR
                // If the game is running in the Unity Editor, stop playing the scene
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                // If the game is not running in the Unity Editor, quit the application
                Application.Quit();
        #endif
    }
}
