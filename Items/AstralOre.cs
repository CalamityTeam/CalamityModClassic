﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class AstralOre : ModItem
{
	public override void SetStaticDefaults()
 	{
 		//DisplayName.SetDefault("Astral Ore");
 	}
	
	public override void SetDefaults()
	{
		Item.width = 13;
		Item.height = 10;
		Item.maxStack = 999;
		Item.value = 3750;
		Item.rare = ItemRarityID.LightRed;
	}
}}