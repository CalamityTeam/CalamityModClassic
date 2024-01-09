using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.HiveMind {
public class TrueShadowScale : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("True Shadow Scale");
	}
	
	public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 22;
		Item.maxStack = 999;
		Item.value = 2500;
		Item.rare = ItemRarityID.Green;
	}
}}