using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class ElysianArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Elysian Arrow");
			// Tooltip.SetDefault("Summons meteors from the sky on death");
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 22;
			Item.height = 36;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 3f;
			Item.value = 2000;
			Item.shoot = Mod.Find<ModProjectile>("ElysianArrow").Type;
			Item.shootSpeed = 10f;
			Item.ammo = 40;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(150);
			recipe.AddIngredient(null, "UnholyEssence");
			recipe.AddIngredient(ItemID.HolyArrow, 150);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}