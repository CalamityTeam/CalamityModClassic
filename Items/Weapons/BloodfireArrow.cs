using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class BloodfireArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bloodfire Arrow");
			// Tooltip.SetDefault("Heals you a small amount on enemy hits");
		}

		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 36;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 3.5f;
			Item.value = 2000;
			Item.shoot = Mod.Find<ModProjectile>("BloodfireArrow").Type;
			Item.shootSpeed = 10f;
			Item.ammo = 40;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(250);
			recipe.AddIngredient(null, "BloodstoneCore");
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}