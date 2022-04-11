using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class GameMainControl : MonoBehaviour
{
    // 游戏的主控脚本
    public bool isPause = false;//游戏暂停
    public float placeRangeRadius;//士兵放置的区域大小半径
    public Sprite warningRange;//红色的警告区域
    public Sprite normalPlaceRange;//正常的警告区域
    public GameObject placeRange;//判断区域的GameObject
    public GameObject Pass;//控制胜利显示
    public int soldierNum;//选择的士兵的序号
    public GameObject soldierPrefeb;//士兵预制体
    public GameObject meleeSoldier;//近战士兵的预制体
    public int meleeSoldierCost;//近战士兵的花费
    public GameObject remoteSoldier;//远程士兵的预制体
    public int remoteSoldierCost;//远程士兵的花费
    public GameObject healSoldier;//治疗士兵的预制体
    public int healSoldierCost;//治疗士兵的花费
    public GameObject TankSoldier;//肉盾的预制体
    public int TankSoldierCost;//肉盾的花费
    public GameObject BaseCamp;//大本营
    public GameObject Warning;//超出范围提示
    public float scaleSpeed = 10.0f;//滚轮缩放速度
    public int dragSpeed = 100;//拖动速度
    public int money;//总资源
    public AudioSource soldierCreate;//细胞生成的音效
    public AudioSource errorAudio;//错误音效


    [SerializeField]private int soldierCost;//当前选择兵种消耗的资源
    private Vector3 screenPosition;//GameMainControl的Screen Position
    private Vector3 mousePositionOnScreen;//鼠标点击的Screen Position
    private Vector3 mousePositionInWorld;//鼠标点击的World Position
    private Vector3 deltPos;//鼠标的移动方向
    private float warningTimer = 0;//警告计时器
    private bool isWarning = false;//是否正在警告
    

    void Start()
    {
        isPause = false;
        Time.timeScale = 1f;
        BaseCamp = GameObject.Find("BaseCamp");
        warningTimer = 0;
    }

    void Update()
    {
        //按照序号选择对应的士兵
        switch (soldierNum) {
            case 1: soldierPrefeb = meleeSoldier; soldierCost = meleeSoldierCost; break;//近战
            case 2: soldierPrefeb = remoteSoldier; soldierCost = remoteSoldierCost; break;//远程
            case 3: soldierPrefeb = healSoldier; soldierCost = healSoldierCost; break;//治疗
            case 4: soldierPrefeb = TankSoldier; soldierCost = TankSoldierCost; break;//肉盾
        }

        if (isWarning)//判断区域警告是否结束
        {
            warningTimer += Time.deltaTime;
            if(warningTimer >= 1f)
            {
                isWarning = false;
                warningTimer = 0;
                placeRange.GetComponent<SpriteRenderer>().sprite = normalPlaceRange;
                Warning.SetActive(false);
            }
        }

        if (!BaseCamp)
        {
            isPause = true;
            Time.timeScale = 0f;
            Pass.SetActive(true);//Pass窗口弹窗
            //胜利的UI接口在此
        }

        if (!isPause)
        {
            if (Input.GetMouseButtonDown(0) && money >= soldierCost)//如果钱够,点击屏幕生成士兵
            {
                if (!(EventSystem.current.currentSelectedGameObject!=null&& (EventSystem.current.currentSelectedGameObject.tag == "Button"|| EventSystem.current.currentSelectedGameObject.tag == "Card")))//如果未点到按钮生成士兵
                {
                    
                    //获得GameMainControl所在的相机坐标
                    screenPosition = Camera.main.WorldToScreenPoint(transform.position);
                    //获得鼠标的相机坐标
                    mousePositionOnScreen = Input.mousePosition;
                    mousePositionOnScreen.z = screenPosition.z;
                    //将鼠标的相机坐标转换为世界坐标
                    mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
                    if(mousePositionInWorld.magnitude < placeRangeRadius)//放置士兵超出区域
                    {
                        isWarning = true;
                        placeRange.GetComponent<SpriteRenderer>().sprite = warningRange;
                        warningTimer = 0f;
                        errorAudio.Play();
                        //士兵放置失败的提示语
                        Warning.SetActive(true);
                    }
                    else
                    {
                        soldierCreate.Play();
                        Instantiate(soldierPrefeb, mousePositionInWorld, Quaternion.identity);
                        money -= soldierCost;//扣除Money
                    }                    
                }

            }
            if (Input.GetMouseButton(1))//鼠标右键拖动
            {
                deltPos.x = Input.GetAxis("Mouse X");//获取鼠标的偏移量
                deltPos.y = Input.GetAxis("Mouse Y");//获取鼠标的偏移量
                Camera.main.transform.position -= deltPos*dragSpeed*Time.deltaTime*0.3f;
            }
            
        }
        //鼠标滚轮的效果
        //Camera.main.fieldOfView 摄像机的视野
        //Camera.main.orthographicSize 摄像机的正交投影
        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView <= 5)
                Camera.main.fieldOfView += 2;
            if (Camera.main.orthographicSize <= 20)
                Camera.main.orthographicSize += 0.5F;
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView > 2)
                Camera.main.fieldOfView -= 2;
            if (Camera.main.orthographicSize >= 1)
                Camera.main.orthographicSize -= 0.5F;
        }
    }

    public void soldierChoose(int i)
    {
        soldierNum = i;
    }

    //改变money的接口,注意正负
    public void changeMoney(int moneyChange)
    {
        money += moneyChange;
    }
}
