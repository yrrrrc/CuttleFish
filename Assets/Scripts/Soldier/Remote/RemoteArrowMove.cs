using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteArrowMove : MonoBehaviour
{
    public GameObject Target;//�����Ŀ��
    public float speed = 3f;//�ٶ�

    private Vector3 direction;//ָ��target������
    private Vector3 moveV;//�˶�������
    private Vector3 lastTargetPosition;//��target��ʧǰ����position
    [SerializeField] private float damage = 10;//�ӵ���damage
    // Start is called before the first frame update
    void Start()
    {
        lastTargetPosition = new Vector3(-100, 0, 0);//��ʼ��
        //Tower = transform.parent.gameObject;//��ȡ������
        //transform.position = Tower.transform.position;//���ó�ʼλ��
        //targetӦ���������ӵ���ʱ�����,����Ӧ����start()��
        //Target = Range.GetComponent<inArrowTowerRange>().Target;//��range��ˢ�»��Ŀ��
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
            //Target������֮����ص�ǰ����ǰ��
            direction = new Vector3(lastTargetPosition.x - transform.position.x,
                       lastTargetPosition.y - transform.position.y, 0);//����ָ��lastTargetPosition
            moveV = speed * direction.normalized * Time.deltaTime;

            //����ڸ�lastTargetPosition�ľ���ƽ������ֵС��0.01f
            if (Mathf.Abs(direction.sqrMagnitude) <= 0.01f)
            {
                Destroy(this.gameObject);
            }
        }

        //�ƶ�
        transform.Translate(moveV);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ArrowTower")//�Ƿ�������
        {
            Destroy(this.gameObject);//�ݻٵ�ǰ�ӵ�
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
