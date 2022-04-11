using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFac : MonoBehaviour
{
    public float shootCD;//���������ʱ��
    public GameObject Range;
    public GameObject ArrowPrefab;//�ӵ���Ԥ����
    public float damage = 30f;//����������
    public int attackMode;//�Ƿ�Ϊ��Χ������ ��0Ϊ��Χ����
    public AudioSource attackAudio;

    private GameObject Target;//������Ŀ��
    private GameObject Arrow;//�����ɵ��ӵ�
    [SerializeField]private float timer;//��ʱ��
    // Start is called before the first frame update
    void Start()
    {
        timer = shootCD - 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

        Target = Range.GetComponent<inArrowTowerRange>().Target;//�鿴�Ƿ���Target
        
        if (Target)
        {
            timer += Time.deltaTime;
            if (timer >= shootCD)
            {
                attackAudio.Play();
                Vector3 pos = transform.position;
                Arrow = Instantiate(ArrowPrefab, pos, Quaternion.identity);
                Arrow.SendMessage("chooseTarget", Target);//��target���ݸ��ӵ�
                Arrow.SendMessage("setMode", attackMode);//���ӵ�ģʽ��Ϣ���ݸ��ӵ�
                Arrow.SendMessage("setDamage", damage);//����������Ϣ���ݸ��ӵ�
                timer = 0;
            }
        }
        else
        {
            if(timer<shootCD - 0.1f)
            timer += Time.deltaTime;
        }

    }
}
