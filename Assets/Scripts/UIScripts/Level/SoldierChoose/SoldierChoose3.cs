using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class SoldierChoose3 : MonoBehaviour
{
    private GameObject[] Card;
    public GameObject GMC;
    public GameObject S1;
    public GameObject S2;
    public GameObject S3;
    public GameObject S4;
    // Start is called before the first frame update

    void Start()
    {
        Card = GameObject.FindGameObjectsWithTag("Card");
        S1 = GameObject.Find("S1");
        S2 = GameObject.Find("S2");
        S3 = GameObject.Find("S3");
        S4 = GameObject.Find("S4");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < 4; i++)
            {
                Card[i].GetComponent<Image>().color = Color.white;
            }
            soldierChoose(1);
            S1.GetComponent<Image>().color = Color.yellow;          
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            for (int i = 0; i < 4; i++)
            {
                Card[i].GetComponent<Image>().color = Color.white;
            }
            soldierChoose(2);
            S2.GetComponent<Image>().color = Color.yellow;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < 4; i++)
            {
                Card[i].GetComponent<Image>().color = Color.white;
            }
            soldierChoose(3);
            S3.GetComponent<Image>().color = Color.yellow;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < 4; i++)
            {
                Card[i].GetComponent<Image>().color = Color.white;
            }
            soldierChoose(4);
            S4.GetComponent<Image>().color = Color.yellow;
        }
    }

    public void soldierChoose(int i)
    {
        GMC.SendMessage("soldierChoose", i);
    }
}
