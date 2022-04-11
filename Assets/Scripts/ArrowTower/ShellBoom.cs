using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellBoom : MonoBehaviour
{
    // Start is called before the first frame update
    public float boomtime;//��ը����ʱ��
    public Animator ani;
    private float timer;//��ʱ��
    private float damage;//������
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetFloat("damage", damage);
        timer += Time.deltaTime;
        if (timer >= boomtime)//��ը�����ݻ�ʵ��
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Soldier"&&timer<=5*Time.deltaTime)//�жϱ�ը��Χ������
        {
            collision.gameObject.SendMessage("changeHP", -1 * damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    void setDamage(float damageSend)
    {
        damage = damageSend;
    }
}
