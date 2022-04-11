using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    public GameObject Target;//射向的目标
    public float speed;//速度
    public int attackMode;//子弹类型
    public GameObject ShellPrefab;//预设的终点范围子弹

    private Vector3 direction;//指向target的向量
    private Vector3 moveV;//运动的向量
    private Vector3 lastTargetPosition;//在target消失前最后的position
    private GameObject Shell;//刚生成的范围子弹
    private bool mode3flag=false;//子弹类型为3时使用的一个变量
    private Vector3 face;

    [SerializeField] private float damage = 10;//子弹的damage
    // Start is called before the first frame update
    void Start()
    {
        lastTargetPosition = new Vector3(-100, 0, 0);//初始化
        this.transform.Rotate(0, 0, 0);
        //Tower = transform.parent.gameObject;//获取父对象
        //transform.position = Tower.transform.position;//设置初始位置
        //target应该由生成子弹的时候决定,而不应该在start()里
        //Target = Range.GetComponent<inArrowTowerRange>().Target;//从range中刷新获得目标
    }

    // Update is called once per frame
    void Update()
    {
        if ((Target&&attackMode!=2)||(attackMode==2&&!mode3flag)) //改变方向的条件：不是穿透子弹且目标存在，或穿透子弹第一次确定目标
        {
            lastTargetPosition = Target.transform.position;
            direction = new Vector3(Target.transform.position.x - transform.position.x,
                        Target.transform.position.y - transform.position.y,0);
            moveV = speed * direction.normalized * Time.deltaTime;
            mode3flag = true;
            if(attackMode==2)
            {
                face = this.transform.right;
                float angle = Vector3.SignedAngle(face, direction, Vector3.forward);
                this.transform.Rotate(0, 0, angle);
                direction = new Vector3(Target.transform.position.x - transform.position.x,
                        Target.transform.position.y - transform.position.y, 0);
                moveV = speed * direction.normalized * Time.deltaTime;
            }
        }
        else
        {
            //Target不存在之后就沿当前方向前进
            if(attackMode!=2)
            {
                direction = new Vector3(lastTargetPosition.x - transform.position.x,
                       lastTargetPosition.y - transform.position.y, 0);//方向指向lastTargetPosition
            }
            moveV = speed * direction.normalized * Time.deltaTime;

            //如果在跟lastTargetPosition的距离平方绝对值小于0.01f
            if (Mathf.Abs(direction.sqrMagnitude) <= 0.01f)
            {
                Destroy(this.gameObject);
            }
        }

        //移动
        if (attackMode != 2)
            transform.Translate(moveV);
        else
            transform.Translate(speed * Time.deltaTime,0, 0, Space.Self);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Soldier")//是否碰到士兵
        {
            switch(attackMode)
            {
                case 0://普通子弹
                    {
                        Destroy(this.gameObject);//摧毁当前子弹
                        collision.gameObject.SendMessage("changeHP", -1 * damage, SendMessageOptions.DontRequireReceiver);
                        break;
                    }
                case 1://AOE子弹
                    {
                        Destroy(this.gameObject);//摧毁当前子弹
                        Vector3 pos = transform.position;
                        Shell = Instantiate(ShellPrefab, pos, Quaternion.identity);
                        Shell.SendMessage("setDamage", damage);
                        break;
                    }
                case 2://穿透子弹
                    {
                        collision.gameObject.SendMessage("changeHP", -1 * damage, SendMessageOptions.DontRequireReceiver);
                        damage -= 7;
                        if(damage<=0) Destroy(this.gameObject);//摧毁当前子弹
                        break;
                    }
                default: break;
            }

        }
    }

    void chooseTarget(GameObject target)
    {
        Target = target;
    }

    void setDamage(float damageSend)
    {
        damage = damageSend;
    }
	
    void setMode(int mode)
    {
        attackMode = mode;
    }
}
