using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 格子类对象  他是放在背包里的一个一个的道具格子
/// 主要用来显示 单组道具信息的
/// </summary>
public class BagItem : BasePanel, IItemBase<Item> {

    /// <summary>
    /// 这个方法 是用于初始化 道具格子信息
    /// </summary>
    /// <param name="info"></param>
	public void InitInfo(Item info)
    {
        //先读取道具表 
        //根据表中数据 来更新信息
        //更新图标
        //更新名字

        //更新道具数量
        GetControl<Text>("txtNum").text = info.num.ToString();
    }
}
