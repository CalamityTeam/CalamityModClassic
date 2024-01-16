using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Perforator {
public class BloodyWormTooth : ModItem
{
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Bloody Worm Tooth");
		////Tooltip.SetDefault("7% increased damage reduction and increased melee stats\n14% increased damage reduction and melee stats when below 50% life");
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
        	player.GetAttackSpeed(DamageClass.Melee) *= 1.1f;
	        player.endurance += 0.14f;
		}
		else
		{
			player.GetDamage(DamageClass.Melee) *= 1.1f;
        	player.GetAttackSpeed(DamageClass.Melee) *= 1.05f;
	        player.endurance += 0.07f;
		}
	}
}}