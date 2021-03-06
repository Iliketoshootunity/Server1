
 //===================================================
//作    者：肖海林
//创建时间：2018-11-06 10:56:28
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// Item数据管理
/// </summary>
public partial class ItemDBModel : AbstractDBModel<ItemDBModel, ItemEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "Item.data"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override ItemEntity MakeEntity(GameDataTableParser parse)
	{
		ItemEntity entity = new ItemEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.Name = parse.GetFieldValue("Name");
		entity.Type = parse.GetFieldValue("Type").ToInt();
		entity.UsedLevel = parse.GetFieldValue("UsedLevel").ToInt();
		entity.UsedMethod = parse.GetFieldValue("UsedMethod");
		entity.Quality = parse.GetFieldValue("Quality").ToInt();
		entity.Description = parse.GetFieldValue("Description");
		entity.UsedItems = parse.GetFieldValue("UsedItems");
		entity.maxAmount = parse.GetFieldValue("maxAmount").ToInt();
		entity.packSort = parse.GetFieldValue("packSort").ToInt();
		return entity;
 	}
}
