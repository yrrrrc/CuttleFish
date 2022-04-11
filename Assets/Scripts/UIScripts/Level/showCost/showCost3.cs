using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class showCost3 : MonoBehaviour
{
    public GameObject GMC;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = "жнаф" +'\n'+ GMC.GetComponent<GameMainControl>().healSoldierCost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
