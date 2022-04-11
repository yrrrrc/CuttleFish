using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class GameMainControl : MonoBehaviour
{
    // ��Ϸ�����ؽű�
    public bool isPause = false;//��Ϸ��ͣ
    public float placeRangeRadius;//ʿ�����õ������С�뾶
    public Sprite warningRange;//��ɫ�ľ�������
    public Sprite normalPlaceRange;//�����ľ�������
    public GameObject placeRange;//�ж������GameObject
    public GameObject Pass;//����ʤ����ʾ
    public int soldierNum;//ѡ���ʿ�������
    public GameObject soldierPrefeb;//ʿ��Ԥ����
    public GameObject meleeSoldier;//��սʿ����Ԥ����
    public int meleeSoldierCost;//��սʿ���Ļ���
    public GameObject remoteSoldier;//Զ��ʿ����Ԥ����
    public int remoteSoldierCost;//Զ��ʿ���Ļ���
    public GameObject healSoldier;//����ʿ����Ԥ����
    public int healSoldierCost;//����ʿ���Ļ���
    public GameObject TankSoldier;//��ܵ�Ԥ����
    public int TankSoldierCost;//��ܵĻ���
    public GameObject BaseCamp;//��Ӫ
    public GameObject Warning;//������Χ��ʾ
    public float scaleSpeed = 10.0f;//���������ٶ�
    public int dragSpeed = 100;//�϶��ٶ�
    public int money;//����Դ
    public AudioSource soldierCreate;//ϸ�����ɵ���Ч
    public AudioSource errorAudio;//������Ч


    [SerializeField]private int soldierCost;//��ǰѡ��������ĵ���Դ
    private Vector3 screenPosition;//GameMainControl��Screen Position
    private Vector3 mousePositionOnScreen;//�������Screen Position
    private Vector3 mousePositionInWorld;//�������World Position
    private Vector3 deltPos;//�����ƶ�����
    private float warningTimer = 0;//�����ʱ��
    private bool isWarning = false;//�Ƿ����ھ���
    

    void Start()
    {
        isPause = false;
        Time.timeScale = 1f;
        BaseCamp = GameObject.Find("BaseCamp");
        warningTimer = 0;
    }

    void Update()
    {
        //�������ѡ���Ӧ��ʿ��
        switch (soldierNum) {
            case 1: soldierPrefeb = meleeSoldier; soldierCost = meleeSoldierCost; break;//��ս
            case 2: soldierPrefeb = remoteSoldier; soldierCost = remoteSoldierCost; break;//Զ��
            case 3: soldierPrefeb = healSoldier; soldierCost = healSoldierCost; break;//����
            case 4: soldierPrefeb = TankSoldier; soldierCost = TankSoldierCost; break;//���
        }

        if (isWarning)//�ж����򾯸��Ƿ����
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
            Pass.SetActive(true);//Pass���ڵ���
            //ʤ����UI�ӿ��ڴ�
        }

        if (!isPause)
        {
            if (Input.GetMouseButtonDown(0) && money >= soldierCost)//���Ǯ��,�����Ļ����ʿ��
            {
                if (!(EventSystem.current.currentSelectedGameObject!=null&& (EventSystem.current.currentSelectedGameObject.tag == "Button"|| EventSystem.current.currentSelectedGameObject.tag == "Card")))//���δ�㵽��ť����ʿ��
                {
                    
                    //���GameMainControl���ڵ��������
                    screenPosition = Camera.main.WorldToScreenPoint(transform.position);
                    //��������������
                    mousePositionOnScreen = Input.mousePosition;
                    mousePositionOnScreen.z = screenPosition.z;
                    //�������������ת��Ϊ��������
                    mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
                    if(mousePositionInWorld.magnitude < placeRangeRadius)//����ʿ����������
                    {
                        isWarning = true;
                        placeRange.GetComponent<SpriteRenderer>().sprite = warningRange;
                        warningTimer = 0f;
                        errorAudio.Play();
                        //ʿ������ʧ�ܵ���ʾ��
                        Warning.SetActive(true);
                    }
                    else
                    {
                        soldierCreate.Play();
                        Instantiate(soldierPrefeb, mousePositionInWorld, Quaternion.identity);
                        money -= soldierCost;//�۳�Money
                    }                    
                }

            }
            if (Input.GetMouseButton(1))//����Ҽ��϶�
            {
                deltPos.x = Input.GetAxis("Mouse X");//��ȡ����ƫ����
                deltPos.y = Input.GetAxis("Mouse Y");//��ȡ����ƫ����
                Camera.main.transform.position -= deltPos*dragSpeed*Time.deltaTime*0.3f;
            }
            
        }
        //�����ֵ�Ч��
        //Camera.main.fieldOfView ���������Ұ
        //Camera.main.orthographicSize �����������ͶӰ
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

    //�ı�money�Ľӿ�,ע������
    public void changeMoney(int moneyChange)
    {
        money += moneyChange;
    }
}
