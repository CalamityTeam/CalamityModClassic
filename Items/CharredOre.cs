using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class CharredOre : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Charred Ore");
	}
	
	public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 16;
		Item.maxStack = 999;
		Item.value = 9750;
		Item.rare = ItemRarityID.LightPurple;
	}
}}