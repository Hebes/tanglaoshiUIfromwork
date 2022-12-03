using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : MonoBehaviour
{
    //1.找控件
    public Button btnRole;
    public Button btnSill;

    public Text txtName;
    public Text txtLev;
    public Text txtMoney;
    public Text txtGem;
    public Text txtPower;

    //2.提供面板更新的相关方法给外部
    public void UpdateInfo( PlayerModel data )
    {
        txtName.text = data.PlayerName;
        txtLev.text = "LV." + data.Lev;
        txtMoney.text = data.Money.ToString();
        txtGem.text = data.Gem.ToString();
        txtPower.text = data.Power.ToString();
    }
    
}
