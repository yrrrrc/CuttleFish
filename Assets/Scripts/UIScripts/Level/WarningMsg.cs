using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMsg : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GMC;
    void Start()
    {
        GMC = GameObject.Find("GameMainControl");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cancel()
    {
        GMC.GetComponent<GameMainControl>().isPause = false;
        Time.timeScale = 1f;
    }
}
