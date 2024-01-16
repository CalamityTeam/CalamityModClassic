using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.DesertScourge {
public class OceanCrest : ModItem
{
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Ocean Crest");
		////Tooltip.SetDefault("Ocean NPCs become friendly and provides waterbreathing");
		Item.width = 18;
		Item.height = 26;
		Item.value = 100000;
		Item.rare = 2;
		Item.accessory = true;
		Item.expert = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.npcTypeNoAggro[65] = true;
		player.npcTypeNoAggro[220] = true;
		player.npcTypeNoAggro[64] = true;
		player.npcTypeNoAggro[67] = true;
		player.npcTypeNoAggro[221] = true;
		player.gills = true;
		/*if (player.breath <= player.breathMax + 2)
		{
		    player.breath = player.breathMax + 3;
		}*/
	}
}}