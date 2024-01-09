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
	public class Meowthrower : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Meowthrower");
			//Tooltip.SetDefault("Consumes gel at a 50% chance");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 28;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 74;
			Item.height = 24;
			Item.useTime = 10;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 1.25f;
			Item.UseSound = SoundID.Item34;
			Item.value = 100000;
			Item.rare = ItemRarityID.Orange;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("MeowFire").Type;
			Item.shootSpeed = 5.5f;
			Item.useAmmo = 23;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 50)
	    		return false;
	    	return true;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	float SpeedA = velocity.X;
	   		float SpeedB = velocity.Y;
	        int num6 = Main.rand.Next(1, 3);
	        for (int index = 0; index < num6; ++index)
	        {
	      	 	float num7 = velocity.X;
	            float num8 = velocity.Y;
	            float SpeedX = velocity.X + (float) Main.rand.Next(-15, 16) * 0.05f;
	            float SpeedY = velocity.Y + (float) Main.rand.Next(-15, 16) * 0.05f;
	    		switch (Main.rand.Next(3))
				{
	    			case 1: type = Mod.Find<ModProjectile>("MeowFire").Type; break;
	    			case 2: type = Mod.Find<ModProjectile>("MeowFire2").Type; break;
	    			default: break;
				}
	            Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	}
	    	return false;
		}
	}
}