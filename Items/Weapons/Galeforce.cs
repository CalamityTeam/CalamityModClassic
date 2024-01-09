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
	public class Galeforce : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Galeforce");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 16;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 22;
	        Item.height = 48;
	        Item.useTime = 19;
	        Item.useAnimation = 19;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 4;
	        Item.value = 75000;
	        Item.rare = ItemRarityID.Orange;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 20f;
	        Item.useAmmo = 40;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "AerialiteBar", 8);
	        recipe.AddIngredient(ItemID.SunplateBlock, 3);
	        recipe.AddTile(TileID.SkyMill);
	        recipe.Register();
	    }
	}
}