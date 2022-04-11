using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFac : MonoBehaviour
{
    public float shootCD;//重新射击的时间
    public GameObject Range;
    public GameObject ArrowPrefab;//子弹的预制体
    public float damage = 30f;//炮塔的威力
    public int attackMode;//是否为范围攻击塔 非0为范围攻击
    public AudioSource attackAudio;

    private GameObject Target;//炮塔的目标
    private GameObject Arrow;//刚生成的子弹
    [SerializeField]private float timer;//计时器
    // Start is called before the first frame update
    void Start()
    {
        timer = shootCD - 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

        Target = Range.GetComponent<inArrowTowerRange>().Target;//查看是否有Target
        
        if (Target)
        {
            timer += Time.deltaTime;
            if (timer >= shootCD)
            {
                attackAudio.Play();
                Vector3 pos = transform.position;
                Arrow = Instantiate(ArrowPrefab, pos, Quaternion.identity);
                Arrow.SendMessage("chooseTarget", Target);//将target传递给子弹
                Arrow.SendMessage("setMode", attackMode);//将子弹模式信息传递给子弹
                Arrow.SendMessage("setDamage", damage);//将攻击力信息传递给子弹
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
