using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.DesertScourge {
public class VictoryShard : ModItem
{
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Victory Shard");
		Item.width = 14;
		Item.height = 14;
		Item.maxStack = 999;
		Item.value = 5000;
		Item.rare = 2;
	}
}}