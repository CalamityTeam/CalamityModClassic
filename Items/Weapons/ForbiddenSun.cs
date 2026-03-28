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
	public class ForbiddenSun : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Forbidden Sun");
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
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 7f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
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