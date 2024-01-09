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
	public class BrimstoneFury : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Brimstone Fury");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 17;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 28;
	        Item.height = 58;
	        Item.useTime = 22;
	        Item.useAnimation = 22;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 3.75f;
	        Item.value = 300000;
	        Item.rare = ItemRarityID.LightPurple;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("BrimstoneBolt").Type;
	        Item.shootSpeed = 13f;
	        Item.useAmmo = 40;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	for (int i = 0; i < 3; i++)
	    	{
	            float SpeedX = velocity.X + 25 * 0.05f;
	            float SpeedY = velocity.Y + 25 * 0.05f;
	            float SpeedX2 = velocity.X - 25 * 0.05f;
	            float SpeedY2 = velocity.Y - 25 * 0.05f;
	            float SpeedX3 = velocity.X + 0 * 0.05f;
	            float SpeedY3 = velocity.Y + 0 * 0.05f;
	        	Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("BrimstoneBolt").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
	        	Projectile.NewProjectile(source, position.X, position.Y, SpeedX2, SpeedY2, Mod.Find<ModProjectile>("BrimstoneBolt").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
	        	Projectile.NewProjectile(source, position.X, position.Y, SpeedX3, SpeedY3, Mod.Find<ModProjectile>("BrimstoneBolt").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
	    	}
	    	return false;
		}
	
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "UnholyCore", 5);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}