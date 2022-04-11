using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealRange : MonoBehaviour
{
    public GameObject Target;

    [SerializeField] private GameObject BaseCamp;
    [SerializeField] private bool TargetLock = false;//����Ŀ��
    // Start is called before the first frame update
    void Start()
    {
        BaseCamp = GameObject.Find("BaseCamp");
        Target = BaseCamp;
        TargetLock = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!Target)//���target�Ƿ񻹴���
        {
            resetTarget();//��������ָ���Ӫ
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (TargetLock == false && collision.tag == "Soldier" && !collision.gameObject.GetComponent<HealMoveAndAttack>())//ת��Ŀ��Ϊʿ��
        {
            Target = collision.gameObject;
            TargetLock = true;
        }
    }
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ArrowTower")
        {
            Target = collision.gameObject;
        }
    }*/

    void resetTarget()
    {
        Target = BaseCamp;
        TargetLock = false;
    }
}
