using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBoom : MonoBehaviour
{
    public float trapDamage;
    public GameObject BoomPrefab;//预设的终点爆炸物，可以使用prefab里的Shell
    private GameObject Boom;//刚生成的范围爆炸物
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Soldier")
        {
            Destroy(this.gameObject);//摧毁当前陷阱
            Vector3 pos = transform.position;
            Boom = Instantiate(BoomPrefab, pos, Quaternion.identity);
            Boom.SendMessage("setDamage", trapDamage);
        }
    }
}
