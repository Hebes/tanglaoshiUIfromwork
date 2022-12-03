using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RolePanel : MonoBehaviour
{
    //1.找控件
    public Text txtLev;
    public Text txtHp;
    public Text txtAtk;
    public Text txtDef;
    public Text txtCrit;
    public Text txtMiss;
    public Text txtLuck;

    public Button btnClose;
    public Button btnLevUp;

    private static RolePanel panel;

    public static void ShowMe()
    {
        if (panel == null)
        {
            //实例化面板对象
            GameObject res = Resources.Load<GameObject>("UI/RolePanel");
            GameObject obj = Instantiate(res);
            //设置它的父对象 为Canvas
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);

            panel = obj.GetComponent<RolePanel>();
        }
        //如果是隐藏的形式hide 在这要显示
        panel.gameObject.SetActive(true);
        //显示完面板 更新该面板的信息
        panel.UpdateInfo();
    }

    public static void HideMe()
    {
        if (panel != null)
        {
            //方式一 直接删
            //Destroy(panel.gameObject);
            //panel = null;
            //方式二 设置可见为隐藏
            panel.gameObject.SetActive(false);
        }
    }


    //4.显影
    // Start is called before the first frame update
    void Start()
    {
        //2.监听事件
        btnClose.onClick.AddListener(ClickClose);
        btnLevUp.onClick.AddListener(ClickLevUp);
    }

    public void ClickClose()
    {
        Debug.Log("关闭");

        HideMe();
    }

    public void ClickLevUp()
    {
        Debug.Log("升级");
        //升级其实就是 数据的更新
        //的数据是存在本地 
        //获取本地数据
        int lev = PlayerPrefs.GetInt("PlayerLev", 1);
        int hp = PlayerPrefs.GetInt("PlayerHp", 100);
        int atk = PlayerPrefs.GetInt("PlayerAtk", 20);
        int def = PlayerPrefs.GetInt("PlayerDef", 10);
        int crit = PlayerPrefs.GetInt("PlayerCrit", 20);
        int miss = PlayerPrefs.GetInt("PlayerMiss", 10);
        int luck = PlayerPrefs.GetInt("PlayerLuck", 40);
        //然后通过一定的升级规则去改变它 
        lev += 1;
        hp += lev;
        atk += lev;
        def += lev;
        crit += lev;
        miss += lev;
        luck += lev;
        //存起来
        PlayerPrefs.SetInt("PlayerLev", lev);
        PlayerPrefs.SetInt("PlayerHp", hp);
        PlayerPrefs.SetInt("PlayerAtk", atk);
        PlayerPrefs.SetInt("PlayerDef", def);
        PlayerPrefs.SetInt("PlayerCrit", crit);
        PlayerPrefs.SetInt("PlayerMiss", miss);
        PlayerPrefs.SetInt("PlayerLuck", luck);

        //同步更新面板上的数据
        UpdateInfo();

        //更新主面板的内容
        MainPanel.Panel.UpdateInfo();
    }

    //3.更新面板
    public void UpdateInfo()
    {
        txtLev.text = "LV." + PlayerPrefs.GetInt("PlayerLev", 1);
        txtHp.text = PlayerPrefs.GetInt("PlayerHp", 100).ToString();
        txtAtk.text = PlayerPrefs.GetInt("PlayerAtk", 20).ToString();
        txtDef.text = PlayerPrefs.GetInt("PlayerDef", 10).ToString();
        txtCrit.text = PlayerPrefs.GetInt("PlayerCrit", 20).ToString();
        txtMiss.text = PlayerPrefs.GetInt("PlayerMiss", 10).ToString();
        txtLuck.text = PlayerPrefs.GetInt("PlayerLuck", 40).ToString();
    }
}
