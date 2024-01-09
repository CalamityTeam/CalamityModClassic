using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class EbonianGel : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Blighted Gel");
	}
		
	public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 14;
		Item.maxStack = 999;
		Item.value = 1000;
		Item.rare = ItemRarityID.Green;
	}
}}