using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inArrowTowerRange : MonoBehaviour
{
    public GameObject Target;
    private bool TargetLock=false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Target) TargetLock = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (TargetLock==false && collision.gameObject.tag == "Soldier")//检测到士兵并确认目标
        {
            Target = collision.gameObject;
            TargetLock = true;
        }
    }

/*    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ArrowTower")
        {
            Target = collision.gameObject;
        }
    }*/
}
