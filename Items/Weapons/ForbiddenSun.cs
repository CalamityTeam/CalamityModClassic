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
	public class ForbiddenSun : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Forbidden Sun");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 80;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 33;
	        Item.width = 28;
	        Item.height = 30;
	        Item.useTime = 30;
	        Item.useAnimation = 30;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 7f;
	        Item.value = 500000;
	        Item.rare = ItemRarityID.Yellow;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("ForbiddenSunProjectile").Type;
	        Item.shootSpeed = 9f;
	    }
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "CruptixBar", 6);
			recipe.AddIngredient(ItemID.LivingFireBlock, 50);
	        recipe.AddTile(TileID.Bookcases);
	        recipe.Register();
		}
	}
}