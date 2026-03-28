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
	public class Lazhar : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lazhar");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 75;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 4;
	        Item.width = 42;
	        Item.height = 20;
	        Item.useTime = 7;
	        Item.useAnimation = 7;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 95, 0, 0);
            Item.rare = 9;
	        Item.UseSound = SoundID.Item12;
	        Item.autoReuse = true;
	        Item.shootSpeed = 15f;
	        Item.shoot = Mod.Find<ModProjectile>("SolarBeam2").Type;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	float SpeedX = velocity.X + (float) Main.rand.Next(-15, 16) * 0.05f;
		    float SpeedY = velocity.Y + (float) Main.rand.Next(-15, 16) * 0.05f;
		    Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.HeatRay);
	        recipe.AddIngredient(ItemID.FragmentSolar, 10);
	        recipe.AddIngredient(ItemID.ChlorophyteBar, 6);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}