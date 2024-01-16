using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.PlaguebringerGoliath {
public class PlagueCellCluster : ModItem
{
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Plague Cell Canister");
		Item.width = 20;
		Item.height = 20;
		Item.maxStack = 999;
		Item.value = 3000;
		Item.rare = 8;
	}
}}