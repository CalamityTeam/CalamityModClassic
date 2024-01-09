using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class Ectoblood : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ectoblood");
	}
		
	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 32;
		Item.maxStack = 999;
		Item.value = 3500;
		Item.rare = ItemRarityID.Yellow;
	}
}}