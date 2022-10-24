using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包面板 主要是用来更新背包逻辑
/// </summary>
public class BagPanel : BasePanel
{
    public RectTransform content;

    CustomSV<Item, BagItem> sv;

    // Use this for initialization
    void Start()
    {
        sv = new CustomSV<Item, BagItem>();
        //初始预设体名
        sv.InitItemResName("UI/BagItem");
        //初始化格子间隔大小 以及 一行几列
        sv.InitItemSizeAndCol(200, 150, 2);
        //初始化COntent父对象以及可视范围
        sv.InitContentAndSVH(content, 925);
        //初始化数据来源
        sv.InitInfos(BagMgr.GetInstance().items);
    }

    // Update is called once per frame
    void Update()
    {
        sv.CheckShowOrHide();
    }
}
