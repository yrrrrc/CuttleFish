using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inSoldierRange : MonoBehaviour
{
    public GameObject Target;

    [SerializeField] private GameObject BaseCamp;
    [SerializeField]private bool TargetLock = false;//锁定目标
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

        if (!Target)//检查target是否还存在
        {
            resetTarget();//不存在则指向大本营
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (TargetLock==false&&collision.tag == "ArrowTower")//转移目标为箭塔
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
