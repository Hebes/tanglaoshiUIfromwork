/*---------------------------------
 *Title:Excel解析转Json C#自动化成代码生成工具
 *Author:暗沉
 *Date:2022/11/8 17:42:10
 *Description:Excel解析转Json C#自动化成代码生成工具
 *注意:以下文件是自动生成的，任何手动修改都会被下次生成覆盖,若手动修改后,尽量避免自动生成
---------------------------------*/
using UnityEngine.UI;
using System.Collections.Generic;

	public class Charect1JsonData
	{
		public string table { get ; set; }
		public List<Charect1JsonInfo> Charect1JsonInfo { get ; set; }
	}
	public class Charect1JsonInfo
	{
		/// <summary>
		/// 序号
		/// </summary>
		public string ID{ get ; set; }

		/// <summary>
		/// 名称
		/// </summary>
		public string Name{ get ; set; }

		/// <summary>
		/// 技能
		/// </summary>
		public string Skill{ get ; set; }

		/// <summary>
		/// 伤害
		/// </summary>
		public string Damage{ get ; set; }

	}
