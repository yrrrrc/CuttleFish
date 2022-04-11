using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBeginAttack : MonoBehaviour
{
    public GameObject Range;
    [SerializeField]private GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Target = Range.GetComponent<inSoldierRange>().Target;//获得range里面的target
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Target && collision.gameObject.tag == "ArrowTower" && collision.gameObject == Target)
        {
            this.gameObject.transform.parent.gameObject.SendMessage("canAttack");
        }
    }
}
