using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPTower : MonoBehaviour
{
    public GameObject Soldier;
    public float Hp;
    public float nowHp;
    // Start is called before the first frame update
    void Start()
    {
        Hp = Soldier.GetComponent<TowerHP>().towerHP;
    }
    //1->4 0->0
    // Update is called once per frame
    void Update()
    {
        float scale_x;
        nowHp = Soldier.GetComponent<TowerHP>().nowHP;
        scale_x = nowHp / Hp;
        this.transform.localScale = new Vector2(scale_x, this.transform.localScale.y);
    }
}
