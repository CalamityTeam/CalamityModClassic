using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.BrimstoneWaifu {
public class Abaddon : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abaddon");
		//Tooltip.SetDefault("Makes you immune to Brimstone Flames");
	}
	
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.value = 100000;
		Item.rare = ItemRarityID.Pink;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
	}
}}