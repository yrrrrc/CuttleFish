using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class SoldierMove : MonoBehaviour
{
    public Transform BaseCamp;//��Ӫ
    public GameObject Range;//���������object
    public GameObject Target;//Ŀ��object
    //public float speed;//�ٶ�
    public float Hp = 100;//ʿ����Ѫ��
    public float nowHP;
    public float damage = 10;//ʿ�����˺�
    public float attackCD = 1f;//����CD
    public Animator anim;
    public AudioSource attackAudio;//������Ч
    public AudioClip deathAudio;//������Ч

    private bool isSlow;//�Ƿ����ڼ���
    private float slowTimer = 0;//����buff��ʱ��
    private float originSpeed;   
    [SerializeField] private float timer = 0;//��ʱ��
    [SerializeField] private bool isAttack = false;//�Ƿ�����attack״̬
    // Start is called before the first frame update
    //ע�����е�Target�޸Ķ�Ӧ����inSoldierRange.cs�����
    void Start()
    {
        Target = Range.GetComponent<inSoldierRange>().Target;//���range�����target
        timer = attackCD - 0.1f;
        nowHP = Hp;
        isSlow = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�������ֵ
        if(nowHP <= 0)
        {
            AudioSource.PlayClipAtPoint(deathAudio, transform.position);
            Destroy(this.gameObject);
        }

        //����buff
        if (isSlow)
        {
            slowTimer += Time.deltaTime;
            if(slowTimer >= 1f)
            {
                isSlow = false;
                this.gameObject.GetComponent<AIPath>().maxSpeed = originSpeed;//�ָ�����
                slowTimer = 0f;
            }
        }

        if (!Target)//�����˳�ս��״̬������˼·��Σ�յģ�����һ����û�кõķ���
        {
            isAttack = false;
            timer = attackCD - 0.1f;
        }

        Target = Range.GetComponent<inSoldierRange>().Target;//���range�����target

        if (isAttack)
        {
            timer += Time.deltaTime;
            if(Target && timer >= attackCD)
            {
                anim.Play("attack");
                attackAudio.Play();
                Target.SendMessage("changeHP", -1* damage,SendMessageOptions.DontRequireReceiver);
                timer = 0;//���ù���
            }
            
        }

        //������ʹ��AstarPathFinding���߼�
        if (Target)
        {
            this.gameObject.GetComponent<AIDestinationSetter>().target = Target.transform;
        }
        else
        {
            isAttack = false;//����Σ�յ����÷���,������Ҫ�Ľ�
            timer = attackCD - 0.1f;
            Range.SendMessage("resetTarget");//����Range�����resetTarget()����
            Target = Range.GetComponent<inSoldierRange>().Target;//���range�����target
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


        ////�ƶ�
        //if (Target)//����target
        //{
        //    Vector3 direction = new Vector3(Target.transform.position.x - transform.position.x,
        //                Target.transform.position.y - transform.position.y,0);//ָ��Target������
        //    Vector3 moveV = speed * direction.normalized * Time.deltaTime;//�ƶ�������
        //    transform.Translate(moveV);//�ƶ�
        //}
        //else//target��ʧ
        //{
        //    Range.SendMessage("resetTarget");//����Range�����resetTarget()����
        //    Target = Range.GetComponent<inSoldierRange>().Target;//���range�����target
        //    Vector3 direction = new Vector3(Target.transform.position.x - transform.position.x,
        //                Target.transform.position.y - transform.position.y, 0);//ָ��Target������
        //    Vector3 moveV = speed * direction.normalized * Time.deltaTime;//�ƶ�������
        //    transform.Translate(moveV);//�ƶ�
        //}


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Target && collision.collider.tag == "ArrowTower" && collision.gameObject == Target)//ײ���������˺�
        {
            //Destroy(collision.gameObject);
            //Range.SendMessage("resetTarget");//����Range�����resetTarget()����
            //Target = Range.GetComponent<inSoldierRange>().Target;//���range�����target
            isAttack = true;
            timer = 0;

        }
    }

    //�ı�Ѫ��
    void changeHP(float HPchange)
    {
        nowHP += HPchange;
        if (nowHP > Hp) nowHP = Hp;
    }

    //�ı�����,para�Ǳ���
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


