using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Leviathan {
public class IOU : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("IOU an item");
		//Tooltip.SetDefault("Use to craft any Leviathan weapon you want\nCombine with Living Shards from Plantera to get your item!");
	}
	
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.value = 100;
		Item.rare = ItemRarityID.Blue;
	}
}}