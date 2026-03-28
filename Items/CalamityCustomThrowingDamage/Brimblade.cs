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
	public class Brimblade : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brimblade");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 26;
			Item.damage = 32;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
			Item.useAnimation = 18;
			Item.useStyle = 1;
			Item.useTime = 18;
			Item.knockBack = 6.5f;
			Item.UseSound = SoundID.Item1;
			Item.height = 26;
            Item.value = Item.buyPrice(0, 48, 0, 0);
            Item.rare = 6;
			Item.shoot = Mod.Find<ModProjectile>("Brimblade").Type;
			Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "UnholyCore", 4);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
