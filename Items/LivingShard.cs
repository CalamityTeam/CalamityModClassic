using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Items {
public class LivingShard : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Living Shard");
	}
		
	public override void SetDefaults()
	{
		Item.width = 14;
		Item.height = 14;
		Item.maxStack = 999;
		Item.value = 15000;
		Item.rare = ItemRarityID.Lime;
	}
}}