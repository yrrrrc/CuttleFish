using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHP : MonoBehaviour
{
    public float towerHP;//塔的血量
    public AudioClip deathAudio;
    public int destroyAward1;
    public int destroyAward2;
    public int destroyAward3;

    public float nowHP;
    private bool level1=false, level2=false;//level1标志塔血量状态已经在1/3以下，level2标志塔血量在2/3以下

    private void Start()
    {
        nowHP = towerHP;
        destroyAward1 = 100;
        destroyAward2 = 200;
        destroyAward3 = 300;
    }

    // Update is called once per frame
    void Update()
    {
        if(nowHP <= 0)
        {
            AudioSource.PlayClipAtPoint(deathAudio, transform.position);
            GameObject.Find("GameMainControl").SendMessage("changeMoney", destroyAward3);
            Destroy(this.gameObject);
        } 
        if(nowHP <= towerHP * 2/3 && !level2)//判断塔血量在2/3以下，并给钱
        {
            GameObject.Find("GameMainControl").SendMessage("changeMoney",destroyAward1);
            level2 = true;
        }
        if(nowHP <= towerHP * 1/3 && !level1)//判断塔在1/3以下，并给钱
        {
            GameObject.Find("GameMainControl").SendMessage("changeMoney", destroyAward2);
            level1 = true;
        }
    }

    void changeHP(float HPchange)
    {
        nowHP += HPchange;
        if (nowHP > towerHP) nowHP = towerHP;
    }
}
