using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.DesertScourge {
public class OceanCrest : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ocean Crest");
		//Tooltip.SetDefault("Ocean NPCs become friendly and provides waterbreathing");
	}
	
	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 26;
		Item.value = 100000;
		Item.rare = ItemRarityID.Green;
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
	}
}}