using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.DesertScourge {
public class VictoryShard : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Victory Shard");
	}
	
	public override void SetDefaults()
	{
		Item.width = 14;
		Item.height = 14;
		Item.maxStack = 999;
		Item.value = 5000;
		Item.rare = ItemRarityID.Green;
	}
}}