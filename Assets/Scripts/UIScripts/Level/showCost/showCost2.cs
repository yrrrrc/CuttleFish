using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class showCost2 : MonoBehaviour
{
    public GameObject GMC;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = "Ô¶³Ì" +'\n'+ GMC.GetComponent<GameMainControl>().remoteSoldierCost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
