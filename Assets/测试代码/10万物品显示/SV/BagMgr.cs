using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道具信息
/// </summary>
public class Item
{
    public int id;
    public int num;
}

/// <summary>
/// 背包管理器 主要管理背包的一些公共数据 和 公共方法
/// </summary>
public class BagMgr : BaseManager<BagMgr> {

    public List<Item> items = new List<Item>();

    /// <summary>
    /// 这个方法 是我们模拟获取数据的方法
    /// 在实际开发中 数据应该是从服务器 或者 是本地文件中读取出来的
    /// </summary>
    public void InitItemsInfo()
    {
        for( int i = 0; i < 100000; ++i )
        {
            Item item = new Item();
            item.id = i;
            item.num = i;

            items.Add(item);
        }
    }

}
