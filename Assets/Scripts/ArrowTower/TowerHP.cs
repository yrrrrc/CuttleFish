using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHP : MonoBehaviour
{
    public float towerHP;//����Ѫ��
    public AudioClip deathAudio;
    public int destroyAward1;
    public int destroyAward2;
    public int destroyAward3;

    public float nowHP;
    private bool level1=false, level2=false;//level1��־��Ѫ��״̬�Ѿ���1/3���£�level2��־��Ѫ����2/3����

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
        if(nowHP <= towerHP * 2/3 && !level2)//�ж���Ѫ����2/3���£�����Ǯ
        {
            GameObject.Find("GameMainControl").SendMessage("changeMoney",destroyAward1);
            level2 = true;
        }
        if(nowHP <= towerHP * 1/3 && !level1)//�ж�����1/3���£�����Ǯ
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
