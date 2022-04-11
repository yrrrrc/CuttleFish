using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTrans : MonoBehaviour
{
    private bool[] LevelLock = new bool[3] { false, true, true };
    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock(int i)
    {
        LevelLock[i] = false;
    }

    public bool isLevelLocked(int i)
    {
        return LevelLock[i];
    }
}
