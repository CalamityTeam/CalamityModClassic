using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class Vesuvius : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 62;  //The width of the .png file in pixels divided by 2.
			Item.damage = 11;  //Keep this reasonable please.
			Item.mana = 6;
			Item.DamageType = DamageClass.Magic;  //Dictates whether this is a melee-class weapon.
			Item.useAnimation = 20;
			Item.useTime = 20;  //Ranges from 1 to 55. 
			Item.noMelee = true;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3f;  //Ranges from 1 to 9.
			Item.UseSound = SoundID.Item88;
			Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
			Item.height = 62;  //The height of the .png file in pixels divided by 2.
			Item.value = 5000000;  //Value is calculated in copper coins.
			Item.shootSpeed = 11f;
			Item.shoot = Mod.Find<ModProjectile>("AsteroidMolten").Type;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(255, Main.DiscoG, 0);
	            }
	        }
	    }
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool CanUseItem(Player player)
		{
			bool betsy = CalamityWorld1Point2.downedBetsy;
			if (player.altFunctionUse == 2)
			{
				Item.mana = 9;
	    		Item.useTime = betsy ? 20 : 25;
	    		Item.useAnimation = betsy ? 20 : 25;
			}
			else
			{
				Item.mana = 6;
	    		Item.useTime = betsy ? 15 : 20;
	    		Item.useAnimation = betsy ? 15 : 20;
			}
			return base.CanUseItem(player);
		}
		
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	    {
			float damageMult = 1f + //1
				(NPC.downedSlimeKing ? 0.3f : 0f) + //1.5
				(NPC.downedBoss1 ? 0.3f : 0f) + //2
				(NPC.downedBoss2 ? 0.3f : 0f) + //2.5
				(NPC.downedQueenBee ? 0.1f : 0f) + //2.75
				(NPC.downedBoss3 ? 0.3f : 0f) + //3.25
				(Main.hardMode ? 0.3f : 0f) + //5.25
				(NPC.downedMechBoss1 ? 0.3f : 0f) + //5.75
				(NPC.downedMechBoss2 ? 0.3f : 0f) + //6.25
				(NPC.downedMechBoss3 ? 0.3f : 0f) + //6.75
				(NPC.downedPlantBoss ? 1f : 0f) + //8.25
				(NPC.downedGolemBoss ? 1f : 0f) + //9.25
				(NPC.downedAncientCultist ? 1f : 0f) + //10.25
				(NPC.downedMoonlord ? 5f : 0f) + //22.25
				(CalamityWorld1Point2.downedProvidence ? 10f : 0f) + //35
				(CalamityWorld1Point2.downedDoG ? 10f : 0f) + //46
				(CalamityWorld1Point2.downedYharon ? 33f : 0f); //86
			damage.Base = (int)((double)damage.Base * damageMult);
	    }
		
		public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
		{
			float kbMult = 1f +
				(NPC.downedSlimeKing ? 0.1f : 0f) +
				(NPC.downedBoss1 ? 0.1f : 0f) + 
				(NPC.downedBoss2 ? 0.1f : 0f) + 
				(NPC.downedQueenBee ? 0.15f : 0f) +
				(NPC.downedBoss3 ? 0.15f : 0f) +
				(Main.hardMode ? 0.15f : 0f) +
				(NPC.downedMechBossAny ? 0.1f : 0f) +
				(NPC.downedPlantBoss ? 0.15f : 0f) +
				(NPC.downedGolemBoss ? 0.1f : 0f) +
				(NPC.downedFishron ? 0.15f : 0f) +
				(NPC.downedAncientCultist ? 0.15f : 0f) +
				(NPC.downedMoonlord ? 0.35f : 0f) +
				(CalamityWorld1Point2.downedProvidence ? 0.15f : 0f) +
				(CalamityWorld1Point2.downedDoG ? 0.15f : 0f) +
				(CalamityWorld1Point2.downedYharon ? 0.2f : 0f);
			knockback = knockback * kbMult;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			bool wallOfFlesh = Main.hardMode;
			bool betsy = CalamityWorld1Point2.downedBetsy;
			bool moonLord = NPC.downedMoonlord;
			if (moonLord)
			{
				Item.shootSpeed = 20f;
			}
			else if (wallOfFlesh)
			{
				Item.shootSpeed = 15f;
			}
			else
			{
				Item.shootSpeed = 11f;
			}
			int projAmt = betsy ? 3 : 1;
	    	if (player.altFunctionUse == 2)
	    	{
	    		int num6 = Main.rand.Next(1, 3) + projAmt;
			    for (int index = 0; index < num6; ++index)
			    {
			        float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
			        float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
			        Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.5f + (float)Main.rand.NextDouble() * 0.9f);
			    }
	    		return false;
	    	}
	    	else
	    	{
				float num72 = Item.shootSpeed;
		    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
		    	float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
				float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
				if (player.gravDir == -1f)
				{
					num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
				}
				float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
				float num81 = num80;
				if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
				{
					num78 = (float)player.direction;
					num79 = 0f;
					num80 = num72;
				}
				else
				{
					num80 = num72 / num80;
				}
		    	num78 *= num80;
				num79 *= num80;
				int num112 = betsy ? 4 : 2;
				for (int num113 = 0; num113 < num112; num113++) 
				{
					vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
					vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
					vector2.Y -= (float)(100 * num113);
					num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X + (float)Main.rand.Next(-40, 41) * 0.03f;
					num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
					if (num79 < 0f) 
					{
						num79 *= -1f;
					}
					if (num79 < 20f) 
					{
						num79 = 20f;
					}
					num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
					num80 = num72 / num80;
					num78 *= num80;
					num79 *= num80;
					float num114 = num78;
					float num115 = num79 + (float)Main.rand.Next(-40, 41) * 0.02f;
					Projectile.NewProjectile(source, vector2.X, vector2.Y, num114 * 0.75f, num115 * 0.75f, type, damage, knockback, player.whoAmI, 0f, 0.5f + (float)Main.rand.NextDouble() * 0.9f); //0.3
				}
	    		return false;
	    	}
		}
	}
}
