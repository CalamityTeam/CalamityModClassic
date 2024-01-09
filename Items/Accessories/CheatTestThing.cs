using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class CheatTestThing : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("lol");
	}
	
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.value = 1;
		Item.rare = ItemRarityID.Blue;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		bool playerName = player.name == "Fabsol";
		if (playerName)
	    {
	   		modPlayer.lol = true;
	   	}
		else if (!player.immune)
	   	{
	   		player.KillMe(PlayerDeathReason.ByOther(12), 1000.0, 0, false);
	   	}
	}
}}