using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class ManeaterBulb : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Maneater Bulb");
	}
		
	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.maxStack = 999;
		Item.value = 10000;
		Item.rare = ItemRarityID.Green;
	}
}}