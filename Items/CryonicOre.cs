using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class CryonicOre : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Cryonic Ore");
	}
		
	public override void SetDefaults()
	{
		Item.width = 13;
		Item.height = 10;
		Item.maxStack = 999;
		Item.value = 5750;
		Item.rare = ItemRarityID.Orange;
	}
}}