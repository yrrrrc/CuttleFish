using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP2 : MonoBehaviour
{
    public GameObject Soldier;
    public float Hp;
    public float nowHp;
    // Start is called before the first frame update
    void Start()
    {
        Hp = Soldier.GetComponent<RemoteSoldierMoveAndAttack>().Hp;
    }
    //1->4 0->0
    // Update is called once per frame
    void Update()
    {
        float scale_x;
        nowHp = Soldier.GetComponent<RemoteSoldierMoveAndAttack>().nowHP;
        scale_x = nowHp / Hp;
        this.transform.localScale = new Vector2(scale_x, this.transform.localScale.y);
    }
}
