using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP_RolePanel : BasePanel
{
    //1.找控件
    //2.处理逻辑
    //3.面板更新
    // Start is called before the first frame update
    void Start()
    {
        //第一次显示时 更新面板
        UpdateInfo(PlayerModel.Data);

        //PlayerModel.Data.AddEventListener(UpdateInfo);


        EventCenter.GetInstance().AddEventListener<PlayerModel>("玩家数据", UpdateInfo);
    }

    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "btnClose":
                UIManager.GetInstance().HidePanel("RolePanel");
                break;
            case "btnLevUp":
                PlayerModel.Data.LevUp();
                break;
        }
    }

    public void UpdateInfo(PlayerModel data)
    {
        GetControl<Text>("txtLev").text = "LV." + data.Lev;
        GetControl<Text>("txtHp").text = data.HP.ToString();
        GetControl<Text>("txtAtk").text = data.Atk.ToString();
        GetControl<Text>("txtDef").text = data.Def.ToString();
        GetControl<Text>("txtCrit").text = data.Crit.ToString();
        GetControl<Text>("txtMiss").text = data.Miss.ToString();
        GetControl<Text>("txtLuck").text = data.Luck.ToString();
    }

    private void OnDestroy()
    {
        //PlayerModel.Data.RemoveEventListener(UpdateInfo);

        EventCenter.GetInstance().RemoveEventListener<PlayerModel>("玩家数据", UpdateInfo);
    }
}
