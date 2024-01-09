using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class AirSpinner : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Air Spinner");
		}

	    public override void SetDefaults()
	    {
	    	Item.CloneDefaults(ItemID.Valor);
	        Item.damage = 21;
	        Item.useTime = 22;
	        Item.useAnimation = 22;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.channel = true;
	        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
	        Item.knockBack = 4;
	        Item.value = 50000;
	        Item.rare = ItemRarityID.Orange;
	        Item.autoReuse = false;
	        Item.shoot = Mod.Find<ModProjectile>("AirSpinnerProjectile").Type;
	    }
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "AerialiteBar", 6);
	        recipe.AddTile(TileID.SkyMill);
	        recipe.Register();
		}
	}
}