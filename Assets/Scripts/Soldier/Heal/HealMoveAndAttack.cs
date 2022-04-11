using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HealMoveAndAttack : MonoBehaviour
{
    public Transform BaseCamp;//��Ӫ
    public GameObject Range;//���������object
    public GameObject attackRange;//���Ʒ�Χ��Object
    public GameObject Target;//Ŀ��object
    //public float speed;//�ٶ�
    public float Hp = 100;//ʿ����Ѫ��
    public float Healdamage = 10;//����
    public float HealCD = 3f;//����CD
    public float speed = 1;
    public AudioClip deathAudio;//������Ч


    private bool isSlow;//�Ƿ����ڼ���
    private float slowTimer = 0;//����buff��ʱ��
    private float originSpeed;
    public float nowHP;
    [SerializeField] private float timer = 0;//��ʱ��
    private GameObject Arrow;
    // Start is called before the first frame update
    //ע�����е�Target�޸Ķ�Ӧ����inSoldierRange.cs�����
    void Start()
    {
        Target = Range.GetComponent<HealRange>().Target;//���range�����target
        BaseCamp = GameObject.Find("BaseCamp").transform;
        timer = 0;
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


        Target = Range.GetComponent<HealRange>().Target;//���range�����target

            timer += Time.deltaTime;
            if (Target && timer >= HealCD)
            {
                Vector3 pos = transform.position;
                attackRange.SendMessage("startHeal");
                timer = 0;//���ù���
            }

        //�ƶ�

        if (Target)
        {
            Vector3 direction = new Vector3(Target.transform.position.x - transform.position.x,
                        Target.transform.position.y - transform.position.y, 0);//ָ��Target������
            Vector3 moveV = speed * direction.normalized * Time.deltaTime;//�ƶ�������
            if(direction.sqrMagnitude >= 1)//������Ŀ�����
            {
                transform.Translate(moveV);//�ƶ�
            }
            

        }
        else//target��ʧ
        {
            Range.SendMessage("resetTarget");//����Range�����resetTarget()����
            Target = Range.GetComponent<HealRange>().Target;//���range�����target
            if (Target)
            {
                Vector3 direction = new Vector3(Target.transform.position.x - transform.position.x,
                        Target.transform.position.y - transform.position.y, 0);//ָ��Target������
                Vector3 moveV = speed * direction.normalized * Time.deltaTime;//�ƶ�������
                transform.Translate(moveV);//�ƶ�
            }
            
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
