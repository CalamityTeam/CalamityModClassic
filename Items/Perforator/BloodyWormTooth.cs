using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Perforator {
public class BloodyWormTooth : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 12;
		Item.height = 15;
		Item.value = 100000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if(player.statLife < (player.statLifeMax2 * 0.5f))
		{
			player.GetDamage(DamageClass.Melee) *= 1.2f;
        	player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
	        player.endurance += 0.12f;
		}
		else
		{
			player.GetDamage(DamageClass.Melee) *= 1.1f;
        	player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
	        player.endurance += 0.06f;
		}
	}
}}