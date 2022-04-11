using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealHealing : MonoBehaviour
{
    public GameObject Range;
    public Animator anim;
    public AudioSource healAudio;


    [SerializeField] private float timer = 0;
    [SerializeField] private GameObject Target;
    [SerializeField] private float healDamage;//治疗量
    // Start is called before the first frame update
    void Start()
    {
        healDamage = this.gameObject.transform.parent.gameObject.GetComponent<HealMoveAndAttack>().Healdamage;
    }

    // Update is called once per frame
    void Update()
    {
        Target = Range.GetComponent<HealRange>().Target;//获得range里面的target

        //以下操作使得碰撞体只有效0.1f
        if (timer <= 0.1f) timer += Time.deltaTime;
        if(timer > 0.1f && timer < 10f)
        {
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            timer = 11f;
        }
    }

    void startHeal()
    {
        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        anim.Play("heal");
        healAudio.Play();
        timer = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
            if (Target && collision.gameObject.tag == "Soldier")
            {
               collision.gameObject.SendMessage("changeHP", healDamage);
            }
        
    }
}
