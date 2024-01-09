using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class BloodflareCore : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.value = 2500000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (player.whoAmI == Main.myPlayer)
		{
			int drain = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, 0f, 476, 40, 0f, Main.myPlayer, 0f, 0f);
			Main.projectile[drain].usesLocalNPCImmunity = true;
			Main.projectile[drain].localNPCHitCooldown = 5;
		}
		if (player.statDefense < 100)
		{
			player.GetDamage(DamageClass.Melee) += 0.15f;
			player.GetDamage(DamageClass.Magic) += 0.15f;
			player.GetDamage(DamageClass.Ranged) += 0.15f;
			player.GetDamage(DamageClass.Throwing) += 0.15f;
			player.GetDamage(DamageClass.Summon) += 0.15f;
		}
		if(player.statLife <= (player.statLifeMax2 * 0.15f))
		{
			player.endurance += 0.3f;
			player.GetCritChance(DamageClass.Melee) += 20;
			player.GetDamage(DamageClass.Melee) += 0.2f;
			player.GetCritChance(DamageClass.Magic) += 20;
			player.GetDamage(DamageClass.Magic) += 0.2f;
			player.GetCritChance(DamageClass.Ranged) += 20;
			player.GetDamage(DamageClass.Ranged) += 0.2f;
			player.GetCritChance(DamageClass.Throwing) += 20;
			player.GetDamage(DamageClass.Throwing) += 0.2f;
			player.GetDamage(DamageClass.Summon) += 0.2f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.5f))
		{
			player.endurance += 0.15f;
			player.GetCritChance(DamageClass.Melee) += 10;
			player.GetDamage(DamageClass.Melee) += 0.1f;
			player.GetCritChance(DamageClass.Magic) += 10;
			player.GetDamage(DamageClass.Magic) += 0.1f;
			player.GetCritChance(DamageClass.Ranged) += 10;
			player.GetDamage(DamageClass.Ranged) += 0.1f;
			player.GetCritChance(DamageClass.Throwing) += 10;
			player.GetDamage(DamageClass.Throwing) += 0.1f;
			player.GetDamage(DamageClass.Summon) += 0.1f;
		}
	}
}}