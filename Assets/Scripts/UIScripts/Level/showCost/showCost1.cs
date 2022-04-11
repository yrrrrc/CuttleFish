using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class showCost1 : MonoBehaviour
{
    public GameObject GMC;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text ="½üÕ½"+'\n'+ GMC.GetComponent<GameMainControl>().meleeSoldierCost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
