using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class showCost4 : MonoBehaviour
{
    public GameObject GMC;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = "»‚∂‹" +'\n'+ GMC.GetComponent<GameMainControl>().TankSoldierCost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
