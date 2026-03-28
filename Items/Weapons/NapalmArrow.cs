using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class NapalmArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Napalm Arrow");
			// Tooltip.SetDefault("Explodes into fire shards");
		}

		public override void SetDefaults()
		{
			Item.damage = 13;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 22;
			Item.height = 36;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 1.5f;
			Item.value = 1000;
			Item.rare = 3;
			Item.shoot = Mod.Find<ModProjectile>("NapalmArrow").Type;
			Item.shootSpeed = 13f;
			Item.ammo = 40;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(250);
			recipe.AddIngredient(null, "EssenceofChaos");
			recipe.AddIngredient(ItemID.Torch);
			recipe.AddIngredient(ItemID.WoodenArrow, 250);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}