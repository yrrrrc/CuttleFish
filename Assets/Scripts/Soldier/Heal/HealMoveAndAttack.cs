using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HealMoveAndAttack : MonoBehaviour
{
    public Transform BaseCamp;//大本营
    public GameObject Range;//自身的区域object
    public GameObject attackRange;//治疗范围的Object
    public GameObject Target;//目标object
    //public float speed;//速度
    public float Hp = 100;//士兵的血量
    public float Healdamage = 10;//奶量
    public float HealCD = 3f;//攻击CD
    public float speed = 1;
    public AudioClip deathAudio;//死亡音效


    private bool isSlow;//是否正在减速
    private float slowTimer = 0;//减速buff计时器
    private float originSpeed;
    public float nowHP;
    [SerializeField] private float timer = 0;//计时器
    private GameObject Arrow;
    // Start is called before the first frame update
    //注：所有的Target修改都应该在inSoldierRange.cs里进行
    void Start()
    {
        Target = Range.GetComponent<HealRange>().Target;//获得range里面的target
        BaseCamp = GameObject.Find("BaseCamp").transform;
        timer = 0;
        nowHP = Hp;
    }

    // Update is called once per frame
    void Update()
    {
        //检查生命值
        if (nowHP <= 0)
        {
            AudioSource.PlayClipAtPoint(deathAudio, transform.position);
            Destroy(this.gameObject);
        }

        //减速buff
        if (isSlow)
        {
            slowTimer += Time.deltaTime;
            if (slowTimer >= 1f)
            {
                isSlow = false;
                speed = originSpeed;//恢复移速
                slowTimer = 0f;
            }
        }


        Target = Range.GetComponent<HealRange>().Target;//获得range里面的target

            timer += Time.deltaTime;
            if (Target && timer >= HealCD)
            {
                Vector3 pos = transform.position;
                attackRange.SendMessage("startHeal");
                timer = 0;//重置攻击
            }

        //移动

        if (Target)
        {
            Vector3 direction = new Vector3(Target.transform.position.x - transform.position.x,
                        Target.transform.position.y - transform.position.y, 0);//指向Target的向量
            Vector3 moveV = speed * direction.normalized * Time.deltaTime;//移动的向量
            if(direction.sqrMagnitude >= 1)//保持与目标距离
            {
                transform.Translate(moveV);//移动
            }
            

        }
        else//target灭失
        {
            Range.SendMessage("resetTarget");//触发Range里面的resetTarget()函数
            Target = Range.GetComponent<HealRange>().Target;//获得range里面的target
            if (Target)
            {
                Vector3 direction = new Vector3(Target.transform.position.x - transform.position.x,
                        Target.transform.position.y - transform.position.y, 0);//指向Target的向量
                Vector3 moveV = speed * direction.normalized * Time.deltaTime;//移动的向量
                transform.Translate(moveV);//移动
            }
            
        }
    }
    
    //改变血量的接口
    void changeHP(float HPchange)
    {
        nowHP += HPchange;
        if (nowHP > Hp) nowHP = Hp;
    }

    //改变移速,para是倍数
    void changeSpeed(float para)
    {
        if (!isSlow)
        {
            originSpeed = speed;
            speed *= para;
            isSlow = true;
        }
        
    }
}
