using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmController : MonoBehaviour
{
    public GameObject bgmPrefab;
    public GameObject bgmInstance = null;
    // Start is called before the first frame update
    void Start()
    {
        bgmInstance = GameObject.FindGameObjectWithTag("sound");
        if (bgmInstance == null)
        {
            bgmInstance = (GameObject)Instantiate(bgmPrefab);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
