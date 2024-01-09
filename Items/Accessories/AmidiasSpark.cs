using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class AmidiasSpark : ModItem
{	
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.value = 10000;
		Item.rare = ItemRarityID.Blue;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.aSpark = true;
	}
}}