using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleView : MonoBehaviour
{
    //1.找控件
    public Button btnClose;
    public Button btnLevUp;

    public Text txtLev;
    public Text txtHp;
    public Text txtAtk;
    public Text txtDef;
    public Text txtCrit;
    public Text txtMiss;
    public Text txtLuck;

    //2.提供面板更新的相关方法给外部
    public void UpdateInfo(PlayerModel data)
    {
        txtLev.text = "LV." + data.Lev;
        txtHp.text = data.HP.ToString();
        txtAtk.text = data.Atk.ToString();
        txtDef.text = data.Def.ToString();
        txtCrit.text = data.Crit.ToString();
        txtMiss.text = data.Miss.ToString();
        txtLuck.text = data.Luck.ToString();
    }
}
