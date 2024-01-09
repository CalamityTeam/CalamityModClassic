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
	public class Animosity : ModItem
	{
	    public override void SetDefaults()
	    {
	        Item.damage = 28;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 70;
	        Item.height = 18;
	        Item.useTime = 4;
	        Item.reuseDelay = 15;
	        Item.useAnimation = 12;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 2f;
	        Item.value = 900000;
	        Item.rare = ItemRarityID.LightPurple;
	        Item.UseSound = SoundID.Item31;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 11f;
	        Item.useAmmo = 97;
	    }
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool AltFunctionUse(Player player)
		{
			return true;
		}
	    
	    public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
	    		Item.useTime = 4;
	    		Item.reuseDelay = 13;
	        	Item.useAnimation = 12;
	        	Item.UseSound = SoundID.Item31;
	        	Item.shootSpeed = 10f;
			}
			else
			{
				Item.useTime = 35;
	    		Item.reuseDelay = 0;
	        	Item.useAnimation = 35;
	        	Item.UseSound = SoundID.Item40;
	        	Item.shootSpeed = 15f;
			}
			return base.CanUseItem(player);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	if (player.altFunctionUse == 2)
	    	{
	    		float SpeedX = velocity.X + (float) Main.rand.Next(-10, 11) * 0.05f;
		    	float SpeedY = velocity.Y + (float) Main.rand.Next(-10, 11) * 0.05f;
		    	Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
	    	else
	    	{
	    		Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, 242, (int)((double)damage * 3.9f), knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 50)
	    		return false;
	    	return true;
	    }
	}
}