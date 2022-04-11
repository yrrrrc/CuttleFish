using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    public GameObject Target;//�����Ŀ��
    public float speed;//�ٶ�
    public int attackMode;//�ӵ�����
    public GameObject ShellPrefab;//Ԥ����յ㷶Χ�ӵ�

    private Vector3 direction;//ָ��target������
    private Vector3 moveV;//�˶�������
    private Vector3 lastTargetPosition;//��target��ʧǰ����position
    private GameObject Shell;//�����ɵķ�Χ�ӵ�
    private bool mode3flag=false;//�ӵ�����Ϊ3ʱʹ�õ�һ������
    private Vector3 face;

    [SerializeField] private float damage = 10;//�ӵ���damage
    // Start is called before the first frame update
    void Start()
    {
        lastTargetPosition = new Vector3(-100, 0, 0);//��ʼ��
        this.transform.Rotate(0, 0, 0);
        //Tower = transform.parent.gameObject;//��ȡ������
        //transform.position = Tower.transform.position;//���ó�ʼλ��
        //targetӦ���������ӵ���ʱ�����,����Ӧ����start()��
        //Target = Range.GetComponent<inArrowTowerRange>().Target;//��range��ˢ�»��Ŀ��
    }

    // Update is called once per frame
    void Update()
    {
        if ((Target&&attackMode!=2)||(attackMode==2&&!mode3flag)) //�ı䷽������������Ǵ�͸�ӵ���Ŀ����ڣ���͸�ӵ���һ��ȷ��Ŀ��
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
            //Target������֮����ص�ǰ����ǰ��
            if(attackMode!=2)
            {
                direction = new Vector3(lastTargetPosition.x - transform.position.x,
                       lastTargetPosition.y - transform.position.y, 0);//����ָ��lastTargetPosition
            }
            moveV = speed * direction.normalized * Time.deltaTime;

            //����ڸ�lastTargetPosition�ľ���ƽ������ֵС��0.01f
            if (Mathf.Abs(direction.sqrMagnitude) <= 0.01f)
            {
                Destroy(this.gameObject);
            }
        }

        //�ƶ�
        if (attackMode != 2)
            transform.Translate(moveV);
        else
            transform.Translate(speed * Time.deltaTime,0, 0, Space.Self);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Soldier")//�Ƿ�����ʿ��
        {
            switch(attackMode)
            {
                case 0://��ͨ�ӵ�
                    {
                        Destroy(this.gameObject);//�ݻٵ�ǰ�ӵ�
                        collision.gameObject.SendMessage("changeHP", -1 * damage, SendMessageOptions.DontRequireReceiver);
                        break;
                    }
                case 1://AOE�ӵ�
                    {
                        Destroy(this.gameObject);//�ݻٵ�ǰ�ӵ�
                        Vector3 pos = transform.position;
                        Shell = Instantiate(ShellPrefab, pos, Quaternion.identity);
                        Shell.SendMessage("setDamage", damage);
                        break;
                    }
                case 2://��͸�ӵ�
                    {
                        collision.gameObject.SendMessage("changeHP", -1 * damage, SendMessageOptions.DontRequireReceiver);
                        damage -= 7;
                        if(damage<=0) Destroy(this.gameObject);//�ݻٵ�ǰ�ӵ�
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
