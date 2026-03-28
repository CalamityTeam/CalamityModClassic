using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class MangroveChakramMelee : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mangrove Chakram");
		}

		public override void SetDefaults()
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
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.height = 38;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
            Item.shoot = Mod.Find<ModProjectile>("MangroveChakramProjectileMelee").Type;
			Item.shootSpeed = 15.5f;
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
