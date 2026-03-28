using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Amidias
{
	public class AmidiasTrident : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Amidias' Trident");
			// Tooltip.SetDefault("Gains the power to shoot whirlpools after Skeletron has been defeated");
		}

		public override void SetDefaults()
		{
			Item.width = 44;
			Item.damage = 20;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true;
			Item.useTurn = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 17;
			Item.useStyle = 5;
			Item.useTime = 17;
			Item.knockBack = 4.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 44;
			Item.value = Item.buyPrice(0, 2, 0, 0);
			Item.rare = 2;
			Item.shoot = Mod.Find<ModProjectile>("AmidiasTridentProj").Type;
			Item.shootSpeed = 6f;
		}

		public override bool CanUseItem(Player player)
		{
			for (int i = 0; i < 1000; ++i)
			{
				if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
				{
					return false;
				}
			}
			return true;
		}
	}
}
