using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class contents : MonoBehaviour
{
    public GameObject Text;
    public string[] text = new string[] { "药物打击者，摸鱼病毒最主要的单体防御设施，能够侦测远距离药物并发起快速打击。血量1230，攻击力60，攻速2", "药物毁灭者，摸鱼病毒最主要的群体防御设施，能够侦测远距离药物并发起范围打击。血量1200，攻击力50，攻速1", "药物穿刺者，摸鱼病毒次要的群体防御设施，能够侦测远距离药物并发起穿透性打击，穿透伤害随穿过目标递减。血量1620，攻击力35/-7，攻速1.5", "摸鱼病毒本毒，摸鱼病毒核心指挥，能够侦测远距离药物并发起高伤害单体攻击和范围攻击，但攻速较慢，只要攻破它就能胜利。血量4000，攻击力500/80，攻速0.1/0.25", "生化炸弹，一种威力非常大的范围伤害陷阱，千万不要靠近它。攻击力400", "开拓者α型，肉搏厮杀是他们的天性，用自己的身体溶解病毒是他们的美学。生命值110，攻击力26，攻速0.8，花费10", "开拓者β型，一次偶然的实验失败造就了它们，它们鄙视α的美学，认为自己的手不该被病毒玷污。生命值48，攻击力25，攻速1.5，花费20", "拯救者，它们是药物中的飞升者，用法阵咏唱救赎那些即将堕落的灵魂。生命值500，治疗量90，攻速0.5，花费250", "毁灭者，用强大的身体身先士卒，抵挡病毒的疯狂攻击，但移动缓慢。血量1260，攻击力114，攻速0.5，花费40" };
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void textDisplay(int i)
    {
        Text.GetComponent<Text>().text = text[i];
    }
}
