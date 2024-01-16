using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.DevourerMunsters {
public class ArmoredShell : ModItem
{
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Armored Shell");
		Item.width = 24;
		Item.height = 30;
		////Tooltip.SetDefault("Highly resistant to pierce and explosion damage");
		Item.maxStack = 999;
		Item.value = 15000;
		Item.rare = 10;
	}
}}