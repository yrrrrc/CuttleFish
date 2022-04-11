using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class SoldierMove : MonoBehaviour
{
    public Transform BaseCamp;//大本营
    public GameObject Range;//自身的区域object
    public GameObject Target;//目标object
    //public float speed;//速度
    public float Hp = 100;//士兵的血量
    public float nowHP;
    public float damage = 10;//士兵的伤害
    public float attackCD = 1f;//攻击CD
    public Animator anim;
    public AudioSource attackAudio;//攻击音效
    public AudioClip deathAudio;//死亡音效

    private bool isSlow;//是否正在减速
    private float slowTimer = 0;//减速buff计时器
    private float originSpeed;   
    [SerializeField] private float timer = 0;//计时器
    [SerializeField] private bool isAttack = false;//是否正在attack状态
    // Start is called before the first frame update
    //注：所有的Target修改都应该在inSoldierRange.cs里进行
    void Start()
    {
        Target = Range.GetComponent<inSoldierRange>().Target;//获得range里面的target
        timer = attackCD - 0.1f;
        nowHP = Hp;
        isSlow = false;
    }

    // Update is called once per frame
    void Update()
    {
        //检查生命值
        if(nowHP <= 0)
        {
            AudioSource.PlayClipAtPoint(deathAudio, transform.position);
            Destroy(this.gameObject);
        }

        //减速buff
        if (isSlow)
        {
            slowTimer += Time.deltaTime;
            if(slowTimer >= 1f)
            {
                isSlow = false;
                this.gameObject.GetComponent<AIPath>().maxSpeed = originSpeed;//恢复移速
                slowTimer = 0f;
            }
        }

        if (!Target)//用来退出战斗状态。这种思路是危险的，但是一下子没有好的方法
        {
            isAttack = false;
            timer = attackCD - 0.1f;
        }

        Target = Range.GetComponent<inSoldierRange>().Target;//获得range里面的target

        if (isAttack)
        {
            timer += Time.deltaTime;
            if(Target && timer >= attackCD)
            {
                anim.Play("attack");
                attackAudio.Play();
                Target.SendMessage("changeHP", -1* damage,SendMessageOptions.DontRequireReceiver);
                timer = 0;//重置攻击
            }
            
        }

        //以下是使用AstarPathFinding的逻辑
        if (Target)
        {
            this.gameObject.GetComponent<AIDestinationSetter>().target = Target.transform;
        }
        else
        {
            isAttack = false;//这是危险的重置方法,可能需要改进
            timer = attackCD - 0.1f;
            Range.SendMessage("resetTarget");//触发Range里面的resetTarget()函数
            Target = Range.GetComponent<inSoldierRange>().Target;//获得range里面的target
            if (Target)
            {
                this.gameObject.GetComponent<AIDestinationSetter>().target = Target.transform;
            } 
            else
            {
                this.gameObject.GetComponent<AIDestinationSetter>().target = null;
                Debug.Log("Yes");
            }
        }


        ////移动
        //if (Target)//存在target
        //{
        //    Vector3 direction = new Vector3(Target.transform.position.x - transform.position.x,
        //                Target.transform.position.y - transform.position.y,0);//指向Target的向量
        //    Vector3 moveV = speed * direction.normalized * Time.deltaTime;//移动的向量
        //    transform.Translate(moveV);//移动
        //}
        //else//target灭失
        //{
        //    Range.SendMessage("resetTarget");//触发Range里面的resetTarget()函数
        //    Target = Range.GetComponent<inSoldierRange>().Target;//获得range里面的target
        //    Vector3 direction = new Vector3(Target.transform.position.x - transform.position.x,
        //                Target.transform.position.y - transform.position.y, 0);//指向Target的向量
        //    Vector3 moveV = speed * direction.normalized * Time.deltaTime;//移动的向量
        //    transform.Translate(moveV);//移动
        //}


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Target && collision.collider.tag == "ArrowTower" && collision.gameObject == Target)//撞到箭塔给伤害
        {
            //Destroy(collision.gameObject);
            //Range.SendMessage("resetTarget");//触发Range里面的resetTarget()函数
            //Target = Range.GetComponent<inSoldierRange>().Target;//获得range里面的target
            isAttack = true;
            timer = 0;

        }
    }

    //改变血量
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
            originSpeed = this.gameObject.GetComponent<AIPath>().maxSpeed;
            this.gameObject.GetComponent<AIPath>().maxSpeed *= para;
            isSlow = true;
        }
        
    }

}


