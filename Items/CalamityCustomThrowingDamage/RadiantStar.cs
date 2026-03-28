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
	public class RadiantStar : CalamityDamageItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Radiant Star");
			// Tooltip.SetDefault("Throws daggers that explode and split after a while");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 52;
			Item.damage = 33;
			Item.crit += 8;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 12;
			Item.useStyle = 1;
			Item.useTime = 12;
			Item.knockBack = 5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 48;
			Item.value = Item.buyPrice(0, 60, 0, 0);
			Item.rare = 7;
			Item.shoot = Mod.Find<ModProjectile>("RadiantStar").Type;
			Item.shootSpeed = 20f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "Prismalline");
	        recipe.AddIngredient(null, "AstralBar", 10);
			recipe.AddIngredient(null, "Stardust", 15);
			recipe.AddIngredient(ItemID.FallenStar, 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
