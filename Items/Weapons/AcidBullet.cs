using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class AcidBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Acid Round");
			/* Tooltip.SetDefault("Explodes into acid that inflicts the plague\n" +
                "Does more damage the higher the target's defense"); */
		}

		public override void SetDefaults()
		{
			Item.damage = 28;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 1.5f;
			Item.value = 1250;
			Item.rare = 8;
			Item.shoot = Mod.Find<ModProjectile>("AcidBullet").Type;
			Item.shootSpeed = 10f;
			Item.ammo = 97;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(150);
			recipe.AddIngredient(ItemID.MusketBall, 150);
			recipe.AddIngredient(null, "PlagueCellCluster");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}