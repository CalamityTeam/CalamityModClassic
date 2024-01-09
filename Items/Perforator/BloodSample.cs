using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Perforator {
public class BloodSample : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Blood Sample");
	}
	
	public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 22;
		Item.scale = 0.5f;
		Item.maxStack = 999;
		Item.value = 2500;
		Item.rare = ItemRarityID.Green;
	}
}}