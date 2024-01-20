using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories
{
	public class FrostFlare : ModItem
	{
		public override void SetStaticDefaults()
		{
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 11));
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.lifeRegen = 2;
			Item.value = 500000;
			Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			modPlayer.frostFlare = true;
			player.resistCold = true;
			player.buffImmune[44] = true;
			player.buffImmune[46] = true;
			player.buffImmune[47] = true;
			if(player.statLife > (player.statLifeMax2 * 0.75f))
			{
				player.GetDamage(DamageClass.Melee) += 0.05f;
				player.GetDamage(DamageClass.Magic) += 0.05f;
				player.GetDamage(DamageClass.Ranged) += 0.05f;
				player.GetDamage(DamageClass.Throwing) += 0.05f;
				player.GetDamage(DamageClass.Summon) += 0.05f;
				player.GetCritChance(DamageClass.Melee) += 5;
				player.GetCritChance(DamageClass.Magic) += 5;
				player.GetCritChance(DamageClass.Ranged) += 5;
				player.GetCritChance(DamageClass.Throwing) += 5;
			}
			if(player.statLife < (player.statLifeMax2 * 0.25f))
			{
				player.statDefense += 20;
				player.runAcceleration *= 1.5f;
				player.maxRunSpeed *= 1.5f;
			}
		}
	}
}