using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChoose : MonoBehaviour
{
    public GameObject DT;
    public string scene;
    public int k;
    public GameObject MsgBox;
    public GameObject MsgBox1;
    // Start is called before the first frame update
    void Start()
    {
        DT = GameObject.Find("DataTrans");//���DataTrans
        scene = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneChoose(string sceneName)
    {
        scene = sceneName;//��ťָ�򳡾�
    }
    public void LevelC(int i)
    {
        k = i;//��ťָ�򳡾����
    }

    public void Go()
    {
        if (scene == "")
        {
            MsgBox1.SetActive(true);
        }
        else
        {
            if (!DT.GetComponent<DataTrans>().isLevelLocked(k)) SceneManager.LoadScene(scene);//���������
            else MsgBox.SetActive(true);
        }
    }
    public void SceneJump(string sceneName)
    {
        SceneManager.LoadScene(sceneName);//������ת
    }
}
