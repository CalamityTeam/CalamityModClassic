using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.SlimeGod {
public class ManaOverloader : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 30;
		Item.height = 30;
		Item.value = 15000;
		Item.rare = ItemRarityID.Pink;
		Item.accessory = true;
		Item.expert = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.statManaMax2 += 50;
		player.GetDamage(DamageClass.Magic) += 0.1f;
		if(player.statMana >= (player.statManaMax2 * 0.5f))
		{
			player.lifeRegen -= 3;
		}
		if (player.statMana < (player.statManaMax2 * 0.05f))
		{
			player.ghostHeal = true;
		}
	}
}}