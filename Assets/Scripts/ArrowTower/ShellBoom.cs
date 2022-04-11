using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellBoom : MonoBehaviour
{
    // Start is called before the first frame update
    public float boomtime;//爆炸持续时间
    public Animator ani;
    private float timer;//计时器
    private float damage;//攻击力
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetFloat("damage", damage);
        timer += Time.deltaTime;
        if (timer >= boomtime)//爆炸结束摧毁实体
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Soldier"&&timer<=5*Time.deltaTime)//判断爆炸范围内物体
        {
            collision.gameObject.SendMessage("changeHP", -1 * damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    void setDamage(float damageSend)
    {
        damage = damageSend;
    }
}
