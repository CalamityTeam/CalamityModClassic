using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class MeldBlob : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Meld Blob");
	}
		
	public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 14;
		Item.maxStack = 999;
		Item.value = 23750;
		Item.rare = ItemRarityID.Yellow;
	}
}}