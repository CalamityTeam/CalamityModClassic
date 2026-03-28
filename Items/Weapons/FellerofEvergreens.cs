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
	public class FellerofEvergreens : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Feller of Evergreens");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 15;
	        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
	        Item.width = 36;
	        Item.height = 36;
	        Item.useTime = 25;
	        Item.useAnimation = 25;
	        Item.useTurn = true;
	        Item.axe = 15;
	        Item.useStyle = 1;
	        Item.knockBack = 5;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 2;
	        Item.UseSound = SoundID.Item1;
	        Item.autoReuse = true;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddRecipeGroup(RecipeGroupID.Wood, 15);
            recipe.AddIngredient(ItemID.TungstenBar, 10);
	        recipe.AddIngredient(ItemID.TungstenAxe);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	        recipe = CreateRecipe();
	        recipe.AddRecipeGroup(RecipeGroupID.Wood, 15);
            recipe.AddIngredient(ItemID.SilverBar, 10);
	        recipe.AddIngredient(ItemID.SilverAxe);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	    }
	}
}