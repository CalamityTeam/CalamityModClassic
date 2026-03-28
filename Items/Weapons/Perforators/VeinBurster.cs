using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Perforators
{
	public class VeinBurster : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Vein Burster");
		}

		public override void SetDefaults()
		{
			Item.width = 52;
			Item.damage = 22;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 25;
			Item.useStyle = 1;
			Item.useTime = 25;
			Item.useTurn = true;
			Item.knockBack = 4.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 50;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
			Item.shoot = Mod.Find<ModProjectile>("BloodBall").Type;
			Item.shootSpeed = 5f;
		}
		
		public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.Vertebrae, 5);
	        recipe.AddIngredient(ItemID.CrimtaneBar, 5);
	        recipe.AddIngredient(null, "BloodSample", 15);
	        recipe.AddTile(TileID.DemonAltar);
	        recipe.Register();
	    }
	}
}
