using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePresenter : MonoBehaviour
{
    private MVP_RoleView roleView;

    private static RolePresenter presenter = null;

    public static RolePresenter Presenter
    {
        get
        {
            return presenter;
        }
    }

    public static void ShowMe()
    {
        if (presenter == null)
        {
            //实例化面板对象
            GameObject res = Resources.Load<GameObject>("UI/RolePanel");
            GameObject obj = Instantiate(res);
            //设置它的父对象 为Canvas
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);

            presenter = obj.GetComponent<RolePresenter>();
        }
        //如果是隐藏的形式hide 在这要显示
        presenter.gameObject.SetActive(true);

    }

    public static void HideMe()
    {
        if (presenter != null)
        {
            //方式一 直接删
            //Destroy(panel.gameObject);
            //panel = null;
            //方式二 设置可见为隐藏
            presenter.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        roleView = this.GetComponent<MVP_RoleView>();
        //第一次更新面板
        //roleView.UpdateInfo(PlayerModel.Data);
        UpdateInfo(PlayerModel.Data);

        roleView.btnClose.onClick.AddListener(ClickCloseBtn);
        roleView.btnLevUp.onClick.AddListener(ClickLevUpBtn);

        //告知数据模块 当更新时 通知哪个函数做处理
        PlayerModel.Data.AddEventListener(UpdateInfo);
    }


    private void ClickCloseBtn()
    {
        HideMe();
    }

    private void ClickLevUpBtn()
    {
        //通过数据模块 进行升级 达到数据改变
        PlayerModel.Data.LevUp();
    }

    private void UpdateInfo(PlayerModel data)
    {
        if (roleView != null)
        {
            //roleView.UpdateInfo(data);
            //直接在P中得到V界面的控件 进行修改 断开V和M的联系
            roleView.txtLev.text = "LV." + data.Lev;
            roleView.txtHp.text = data.HP.ToString();
            roleView.txtAtk.text = data.Atk.ToString();
            roleView.txtDef.text = data.Def.ToString();
            roleView.txtCrit.text = data.Crit.ToString();
            roleView.txtMiss.text = data.Miss.ToString();
            roleView.txtLuck.text = data.Luck.ToString();
        }
    }

    private void OnDestroy()
    {
        PlayerModel.Data.RemoveEventListener(UpdateInfo);
    }
}
