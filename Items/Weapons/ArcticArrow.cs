using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class ArcticArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Arctic Arrow");
			// Tooltip.SetDefault("Freezes enemies for a short time");
		}

		public override void SetDefaults()
		{
			Item.damage = 16;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 22;
			Item.height = 36;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 1.5f;
			Item.value = 1200;
			Item.rare = 3;
			Item.shoot = Mod.Find<ModProjectile>("ArcticArrow").Type;
			Item.shootSpeed = 13f;
			Item.ammo = 40;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(250);
			recipe.AddIngredient(null, "CryoBar");
			recipe.AddTile(TileID.IceMachine);
			recipe.Register();
		}
	}
}