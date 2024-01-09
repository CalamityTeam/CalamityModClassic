using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Providence {
[AutoloadEquip(EquipType.Shield)]
public class ElysianAegis : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 48;
		Item.height = 42;
		Item.value = 10000000;
		Item.expert = true;
		Item.rare = ItemRarityID.Cyan;
		Item.defense = 10;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.dashMod = 3;
		modPlayer.elysianAegis = true;
		player.noKnockback = true;
		player.fireWalk = true;
		player.lifeRegen += 3;
		player.statLifeMax2 += 100;
	}
}}