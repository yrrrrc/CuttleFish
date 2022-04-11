using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteArrowMove : MonoBehaviour
{
    public GameObject Target;//射向的目标
    public float speed = 3f;//速度

    private Vector3 direction;//指向target的向量
    private Vector3 moveV;//运动的向量
    private Vector3 lastTargetPosition;//在target消失前最后的position
    [SerializeField] private float damage = 10;//子弹的damage
    // Start is called before the first frame update
    void Start()
    {
        lastTargetPosition = new Vector3(-100, 0, 0);//初始化
        //Tower = transform.parent.gameObject;//获取父对象
        //transform.position = Tower.transform.position;//设置初始位置
        //target应该由生成子弹的时候决定,而不应该在start()里
        //Target = Range.GetComponent<inArrowTowerRange>().Target;//从range中刷新获得目标
    }

    // Update is called once per frame
    void Update()
    {
        if (Target)
        {
            lastTargetPosition = Target.transform.position;
            direction = new Vector3(Target.transform.position.x - transform.position.x,
                        Target.transform.position.y - transform.position.y, 0);
            moveV = speed * direction.normalized * Time.deltaTime;
        }
        else
        {
            //Target不存在之后就沿当前方向前进
            direction = new Vector3(lastTargetPosition.x - transform.position.x,
                       lastTargetPosition.y - transform.position.y, 0);//方向指向lastTargetPosition
            moveV = speed * direction.normalized * Time.deltaTime;

            //如果在跟lastTargetPosition的距离平方绝对值小于0.01f
            if (Mathf.Abs(direction.sqrMagnitude) <= 0.01f)
            {
                Destroy(this.gameObject);
            }
        }

        //移动
        transform.Translate(moveV);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ArrowTower")//是否碰到塔
        {
            Destroy(this.gameObject);//摧毁当前子弹
            collision.gameObject.SendMessage("changeHP", -1 * damage, SendMessageOptions.DontRequireReceiver);
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
}
