using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scenejump(string sceneName)
    {
        Time.timeScale = 1f;//动画暂停状态取消
        GameObject.Find("GameMainControl").GetComponent<GameMainControl>().isPause = false;
        SceneManager.LoadScene(sceneName);//场景跳转
    }
}
