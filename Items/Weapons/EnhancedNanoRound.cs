using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class EnhancedNanoRound : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Enhanced Nano Round");
			// Tooltip.SetDefault("Confuses enemies and releases a cloud of nanites when enemies die");
		}

		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 5.5f;
			Item.value = 500;
			Item.rare = 3;
			Item.shoot = Mod.Find<ModProjectile>("EnhancedNanoRound").Type;
			Item.shootSpeed = 8f;
			Item.ammo = 97;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(250);
			recipe.AddIngredient(ItemID.NanoBullet, 250);
			recipe.AddIngredient(null, "EssenceofEleum");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}