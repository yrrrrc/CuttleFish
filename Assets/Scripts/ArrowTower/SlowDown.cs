using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{
    public float slowMultiply;//移动速度乘数
    // Start is called before the first frame update
    void Start()
    {
        slowMultiply = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Soldier")
        {
            collision.gameObject.SendMessage("changeSpeed", slowMultiply, SendMessageOptions.DontRequireReceiver);
        }
    }
}
