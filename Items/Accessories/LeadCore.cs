using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class LeadCore : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Lead Core");
		//Tooltip.SetDefault("Grants immunity to the irradiated debuff");
	}
	
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.rare = ItemRarityID.Red;
		Item.value = 150000;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.buffImmune[Mod.Find<ModBuff>("Irradiated").Type] = true;
	}
}}