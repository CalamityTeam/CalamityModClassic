using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class TheFirstShadowflame : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("The First Shadowflame");
		//Tooltip.SetDefault("One of the first magical artifacts, granted to a disheveled race of humans long ago by the Tyrant King Yharim\nLittle did the humans know of the horrid curse that lied within...\nGrants shadowflame powers to all minions");
	}
	
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.value = 150000;
		Item.rare = ItemRarityID.Pink;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.shadowMinions = true;
	}
}}