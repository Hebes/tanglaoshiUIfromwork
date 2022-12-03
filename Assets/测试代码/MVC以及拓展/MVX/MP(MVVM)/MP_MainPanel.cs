using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP_MainPanel : BasePanel
{
    //1.找控件 通过集成小框架中的 UI基类实现了  
    //2.逻辑处理 
    //3.数据更新

    // Start is called before the first frame update
    void Start()
    {
        //第一次打开时更新
        UpdateInfo(PlayerModel.Data);

        //PlayerModel.Data.AddEventListener(UpdateInfo);

        EventCenter.GetInstance().AddEventListener<PlayerModel>("玩家数据", UpdateInfo);
    }

    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "btnRole":
                //处理角色面板打开
                UIManager.GetInstance().ShowPanel<MP_RolePanel>("RolePanel");
                break;
        }
    }

    public void UpdateInfo(PlayerModel data)
    {
        //直接在这获取控件 进行更新
        GetControl<Text>("txtName").text = data.PlayerName;
        GetControl<Text>("txtLev").text = "LV." +  data.Lev;

        GetControl<Text>("txtMoney").text = data.Money.ToString();
        GetControl<Text>("txtGem").text = data.Gem.ToString();
        GetControl<Text>("txtPower").text = data.Power.ToString();
    }

    private void OnDestroy()
    {
        //PlayerModel.Data.RemoveEventListener(UpdateInfo);

        EventCenter.GetInstance().RemoveEventListener<PlayerModel>("玩家数据", UpdateInfo);
    }
}
