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
	public class Prismalline : CalamityDamageItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Prismalline");
			// Tooltip.SetDefault("Throws daggers that split after a while");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 46;
			Item.damage = 22;
			Item.crit += 4;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 14;
			Item.useStyle = 1;
			Item.useTime = 14;
			Item.knockBack = 5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 46;
			Item.value = Item.buyPrice(0, 36, 0, 0);
			Item.rare = 5;
			Item.shoot = Mod.Find<ModProjectile>("Prismalline").Type;
			Item.shootSpeed = 16f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "Crystalline");
	        recipe.AddIngredient(null, "EssenceofEleum", 5);
	        recipe.AddIngredient(null, "SeaPrism", 5);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
