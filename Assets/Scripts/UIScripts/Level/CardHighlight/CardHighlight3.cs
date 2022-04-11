using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CardHighlight3 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GMC;
    private GameObject[] Card;
    void Start()
    {
        GMC = GameObject.Find("GameMainControl");
        Card = GameObject.FindGameObjectsWithTag("Card");
        Card[0].GetComponent<Image>().color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GMC.GetComponent<GameMainControl>().isPause&&Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.currentSelectedGameObject!=null && EventSystem.current.currentSelectedGameObject.tag == "Card")
            {
                for(int i = 0; i < 4; i++)
                {
                    Card[i].GetComponent<Image>().color = Color.white;
                }
                EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.yellow;
            }
        }
    }
}
