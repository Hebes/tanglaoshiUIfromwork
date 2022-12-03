/*---------------------------------
 *Title:Excel转Asset自动化成代码生成工具
 *Author:暗沉
 *Date:2022/11/8 17:44:24
 *Description:Excel转Asset自动化成代码生成工具
 *注意:以下文件是自动生成的，任何手动修改都会被下次生成覆盖,若手动修改后,尽量避免自动生成
---------------------------------*/
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Asset
{
	[CreateAssetMenu(fileName ="CharectAssetData", menuName = "Inventory/Charect")]
	public class CharectAssetData:ScriptableObject
	{
		public List<Charect> CharectList;
		/// <summary>
		/// Asset生成代码 方法一
		/// </summary>
		public void CreatAsset(List<Charect> Charects)
		{
			CharectAssetData manager = (CharectAssetData)ScriptableObject.CreateInstance<CharectAssetData>();
			manager.CharectList = Charects;
			AssetDatabase.CreateAsset(manager,"Assets/UIAutomateTool/Editor/OutPut/Assets/CharectAssetData.asset");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}

	[System.Serializable]
	public class Charect
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
		/// <summary>
		/// 攻击力
		/// </summary>
		[Header("攻击力")]
		public float Attack;
		/// <summary>
		/// 是否完成
		/// </summary>
		[Header("是否完成")]
		public bool OK;
		/// <summary>
		/// 图片
		/// </summary>
		[Header("图片")]
		public Sprite Sprite;
	}
}
