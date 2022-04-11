using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unlock : MonoBehaviour
{
    public GameObject DT;
    public GameObject GMC;
    // Start is called before the first frame update
    void Start()
    {
        DT = GameObject.Find("DataTrans");
        GMC = GameObject.Find("GameMainControl");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void unlock(int i)
    {
        DT.SendMessage("Unlock", i);//��DataTrans���ͽ�����Ϣ
        SceneManager.LoadScene("LevelChoose");//�ص�LevelChoose����
    }

}
