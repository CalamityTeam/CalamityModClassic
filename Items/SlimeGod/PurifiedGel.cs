using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.SlimeGod {
public class PurifiedGel : ModItem
{
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Purified Gel");
		Item.width = 16;
		Item.height = 14;
		Item.maxStack = 999;
		Item.value = 3750;
		Item.rare = 5;
	}
}}