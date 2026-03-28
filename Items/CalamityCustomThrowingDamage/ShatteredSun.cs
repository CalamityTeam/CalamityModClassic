using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage
{
	public class ShatteredSun : CalamityDamageItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shattered Sun");
			// Tooltip.SetDefault("Throws daggers that split twice and explode upon contact");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 56;
			Item.height = 56;
			Item.damage = 40;
			Item.crit += 10;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 11;
			Item.useStyle = 1;
			Item.useTime = 11;
			Item.knockBack = 6f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.value = Item.buyPrice(1, 20, 0, 0);
			Item.rare = 10;
			Item.shoot = Mod.Find<ModProjectile>("ShatteredSun").Type;
			Item.shootSpeed = 25f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "RadiantStar");
	        recipe.AddIngredient(null, "DivineGeode", 6);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	}
}
