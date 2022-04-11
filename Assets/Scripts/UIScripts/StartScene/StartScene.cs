using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SceneJump(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void BtnQuit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//仅在编辑器中使用
        #endif

        Application.Quit();

    }
}
