using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteSoldierMoveAndAttack : MonoBehaviour
{
    public Transform BaseCamp;//��Ӫ
    public GameObject Range;//���������object
    public GameObject Target;//Ŀ��object
    //public float speed;//�ٶ�
    public float Hp = 50;//ʿ����Ѫ��
    public float damage = 20;//ʿ�����˺�
    public float attackCD = 0.8f;//����CD
    public float speed = 1;
    public GameObject ArrowPrefab;//�ӵ���Ԥ����
    public AudioSource attackAudio;//������Ч
    public AudioClip deathAudio;//������Ч

    private bool isSlow;//�Ƿ����ڼ���
    private float slowTimer = 0;//����buff��ʱ��
    private float originSpeed;
    public float nowHP;
    [SerializeField] private float timer = 0;//��ʱ��
    [SerializeField] private bool isAttack = false;//�Ƿ�����attack״̬
    private GameObject Arrow;
    // Start is called before the first frame update
    //ע�����е�Target�޸Ķ�Ӧ����inSoldierRange.cs�����
    void Start()
    {
        Target = Range.GetComponent<inSoldierRange>().Target;//���range�����target
        BaseCamp = GameObject.Find("BaseCamp").transform;
        timer = attackCD - 0.1f;
        nowHP = Hp;
    }

    // Update is called once per frame
    void Update()
    {
        //�������ֵ
        if (nowHP <= 0)
        {
            AudioSource.PlayClipAtPoint(deathAudio, transform.position);
            Destroy(this.gameObject);
        }

        //����buff
        if (isSlow)
        {
            slowTimer += Time.deltaTime;
            if (slowTimer >= 1f)
            {
                isSlow = false;
                speed = originSpeed;//�ָ�����
                slowTimer = 0f;
            }
        }

        if (!Target)
        {
            isAttack = false;
            timer = attackCD - 0.1f;
        }

        Target = Range.GetComponent<inSoldierRange>().Target;//���range�����target

        if (isAttack)
        {
            timer += Time.deltaTime;
            if (Target && timer >= attackCD)
            {
                Vector3 pos = transform.position;
                attackAudio.Play();
                Arrow = Instantiate(ArrowPrefab, pos, Quaternion.identity);
                Arrow.SendMessage("chooseTarget", Target);//��target���ݸ��ӵ�
                Arrow.SendMessage("setDamage", damage);
                timer = 0;//���ù���
            }

        }

        //�ƶ�
        if (Target)
        {
            Vector3 direction = new Vector3(Target.transform.position.x - transform.position.x,
                        Target.transform.position.y - transform.position.y, 0);//ָ��Target������
            Vector3 moveV = speed * direction.normalized * Time.deltaTime;//�ƶ�������
            if (!isAttack)//������ʱ���ƶ�
            {
                transform.Translate(moveV);//�ƶ�
            }
            
        }
        else//target��ʧ
        {
            isAttack = false;
            timer = attackCD - 0.1f;
            Range.SendMessage("resetTarget");//����Range�����resetTarget()����
            Target = Range.GetComponent<inSoldierRange>().Target;//���range�����target
            if (Target)
            {
                Vector3 direction = new Vector3(Target.transform.position.x - transform.position.x,
                            Target.transform.position.y - transform.position.y, 0);//ָ��Target������
                Vector3 moveV = speed * direction.normalized * Time.deltaTime;//�ƶ�������
            transform.Translate(moveV);//�ƶ�
            }
            
        }
            //this.gameObject.GetComponent<AIDestinationSetter>().target = Target.transform;
        }
    void canAttack()
    {
        if (!isAttack)
        {
            isAttack = true;
            timer = 0;
        }
        
    }

    //�ı�Ѫ���Ľӿ�
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
            originSpeed = speed;
            speed *= para;
            isSlow = true;
        }
        
    }
}

