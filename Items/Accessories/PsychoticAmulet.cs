using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class PsychoticAmulet : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.value = 150000;
		Item.rare = ItemRarityID.Yellow;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
		modPlayer.pAmulet = true;
		player.shroomiteStealth = true;
		player.GetDamage(DamageClass.Melee) += 0.05f;
		player.GetCritChance(DamageClass.Melee) += 5;
		player.GetDamage(DamageClass.Ranged) += 0.05f;
		player.GetCritChance(DamageClass.Ranged) += 5;
	}
}}