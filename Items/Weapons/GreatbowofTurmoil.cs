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
	public class GreatbowofTurmoil : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Greatbow of Turmoil");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 70;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 18;
	        Item.height = 36;
	        Item.useTime = 17;
	        Item.useAnimation = 17;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 4f;
	        Item.value = 300000;
	        Item.rare = ItemRarityID.Yellow;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 17f;
	        Item.useAmmo = 40;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	for (int i = 0; i < 3; i++)
	    	{
		    	float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
		       	float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
		    	switch (Main.rand.Next(6))
				{
		    		case 1: type = ProjectileID.CursedArrow; break;
		    		case 2: type = ProjectileID.HellfireArrow; break;
		    		case 3: type = ProjectileID.IchorArrow; break;
		    		default: break;
				}
		        Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, Main.myPlayer);
	    	}
	    	return false;
		}
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "CruptixBar", 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}