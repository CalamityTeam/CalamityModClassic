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
	public class DarkechoGreatbow : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Darkecho Greatbow");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 37;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 38;
	        Item.height = 68;
	        Item.useTime = 22;
	        Item.useAnimation = 22;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 4;
	        Item.value = 175000;
	        Item.rare = ItemRarityID.Pink;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 16f;
	        Item.useAmmo = 40;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	for (int i = 0; i < 2; i++)
	    	{
	    		float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
	        	float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
	    		switch (Main.rand.Next(3))
				{
	    			case 1: type = ProjectileID.UnholyArrow; break;
	    			default: break;
				}
	        	Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
	    	}
	    	return false;
		}
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "VerstaltiteBar", 8);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}