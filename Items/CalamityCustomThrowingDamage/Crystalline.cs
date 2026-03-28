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
	public class Crystalline : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crystalline");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 44;
			Item.damage = 18;
			Item.crit += 4;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 18;
			Item.useStyle = 1;
			Item.useTime = 18;
			Item.knockBack = 3f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 44;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 2;
			Item.shoot = Mod.Find<ModProjectile>("Crystalline").Type;
			Item.shootSpeed = 10f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "WulfrumKnife", 50);
	        recipe.AddIngredient(ItemID.Diamond, 3);
	        recipe.AddIngredient(ItemID.FallenStar, 3);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
