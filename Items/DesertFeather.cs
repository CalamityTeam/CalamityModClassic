using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class DesertFeather : ModItem
{
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Desert Feather");
		Item.width = 24;
		Item.height = 24;
		Item.maxStack = 999;
		Item.value = 5000;
		Item.rare = 2;
	}
}}