using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class Stardust : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Stardust");
	}
		
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 18;
		Item.maxStack = 999;
		Item.value = 5000;
		Item.rare = ItemRarityID.Pink;
	}
}}