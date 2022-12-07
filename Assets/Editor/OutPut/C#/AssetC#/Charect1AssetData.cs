/*---------------------------------
 *Title:Excel转Asset自动化成代码生成工具
 *Author:暗沉
 *Date:2022/12/6 21:35:01
 *Description:Excel转Asset自动化成代码生成工具
 *注意:以下文件是自动生成的，任何手动修改都会被下次生成覆盖,若手动修改后,尽量避免自动生成
---------------------------------*/
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Asset
{
	[CreateAssetMenu(fileName ="Charect1AssetData", menuName = "Inventory/Charect1")]
	public class Charect1AssetData:ScriptableObject
	{
		public List<Charect1> Charect1List;
		/// <summary>
		/// Asset生成代码 方法一
		/// </summary>
		public void CreatAsset(List<Charect1> Charect1s)
		{
			Charect1AssetData manager = (Charect1AssetData)ScriptableObject.CreateInstance<Charect1AssetData>();
			manager.Charect1List = Charect1s;
			AssetDatabase.CreateAsset(manager,"Assets/Editor/OutPut/Assets/Charect1AssetData.asset");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}

	[System.Serializable]
	public class Charect1
	{
		/// <summary>
		/// 序号
		/// </summary>
		[Header("序号")]
		public int ID;
		/// <summary>
		/// 名称
		/// </summary>
		[Header("名称")]
		public string Name;
		/// <summary>
		/// 技能
		/// </summary>
		[Header("技能")]
		public List<int> Skill;
		/// <summary>
		/// 伤害
		/// </summary>
		[Header("伤害")]
		public float Damage;
	}
}
