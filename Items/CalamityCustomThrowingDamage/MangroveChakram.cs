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
	public class MangroveChakram : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mangrove Chakram");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 38;
			Item.damage = 84;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 14;
			Item.useStyle = 1;
			Item.useTime = 14;
			Item.knockBack = 7.5f;
			Item.UseSound = SoundID.Item1;
			Item.height = 38;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
			Item.shoot = Mod.Find<ModProjectile>("MangroveChakramProjectile").Type;
			Item.shootSpeed = 15.5f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "DraedonBar", 7);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
