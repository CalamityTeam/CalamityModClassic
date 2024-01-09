using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class GypsyPowder : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Gypsy Powder");
	}
		
	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.maxStack = 999;
		Item.value = 50000;
		Item.rare = ItemRarityID.Pink;
	}
}}