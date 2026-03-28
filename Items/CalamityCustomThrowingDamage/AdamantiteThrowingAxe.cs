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
	public class AdamantiteThrowingAxe : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Adamantite Throwing Axe");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 26;
			Item.damage = 37;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 12;
			Item.useStyle = 1;
			Item.useTime = 12;
			Item.knockBack = 3.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 30;
			Item.maxStack = 999;
			Item.value = 1600;
			Item.rare = 4;
			Item.shoot = Mod.Find<ModProjectile>("AdamantiteThrowingAxeProjectile").Type;
			Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(25);
	        recipe.AddIngredient(ItemID.AdamantiteBar);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
