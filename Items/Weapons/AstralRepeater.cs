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
	public class AstralRepeater : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Astral Repeater");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 80;
	        Item.crit += 25;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 50;
	        Item.height = 34;
	        Item.useTime = 4;
	        Item.reuseDelay = 15;
	        Item.useAnimation = 12;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 2.5f;
	        Item.value = 350000;
	        Item.rare = ItemRarityID.Lime;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 16f;
	        Item.useAmmo = 40;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "AstralBar", 7);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}