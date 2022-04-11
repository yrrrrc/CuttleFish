using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameContinue()
    {
        GameObject.Find("GameMainControl").GetComponent<GameMainControl>().isPause = false;//È¡ÏûÔÝÍ£×´Ì¬
        Time.timeScale = 1f;
    }
}
