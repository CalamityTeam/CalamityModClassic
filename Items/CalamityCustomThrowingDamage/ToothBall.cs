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
	public class ToothBall : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tooth Ball");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 30;
			Item.damage = 20;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 13;
			Item.crit = 8;
			Item.useStyle = 1;
			Item.useTime = 13;
			Item.knockBack = 2.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 30;
			Item.maxStack = 999;
			Item.value = 1000;
			Item.rare = 3;
			Item.shoot = Mod.Find<ModProjectile>("ToothBallProjectile").Type;
			Item.shootSpeed = 16f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(100);
	        recipe.AddIngredient(null, "BloodSample");
	        recipe.AddIngredient(ItemID.Vertebrae);
	        recipe.AddIngredient(ItemID.CrimtaneBar);
	        recipe.AddTile(TileID.DemonAltar);
	        recipe.Register();
		}
	}
}
