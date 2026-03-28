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
	public class FeatherKnife : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Feather Knife");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 18;
			Item.damage = 20;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 13;
			Item.useStyle = 1;
			Item.useTime = 13;
			Item.knockBack = 2f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 32;
			Item.maxStack = 999;
			Item.value = 300;
			Item.rare = 3;
			Item.shoot = Mod.Find<ModProjectile>("FeatherKnifeProjectile").Type;
			Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(30);
	        recipe.AddIngredient(null, "AerialiteBar");
	        recipe.AddTile(TileID.SkyMill);
	        recipe.Register();
		}
	}
}
