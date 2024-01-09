using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class ArkoftheCosmos : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 76;
			Item.damage = 380;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.crit += 15;
			Item.knockBack = 9.5f;
			Item.UseSound = SoundID.Item60;
			Item.autoReuse = true;
			Item.height = 76;
			Item.value = 30000000;
			Item.shoot = Mod.Find<ModProjectile>("EonBeam").Type;
			Item.shootSpeed = 28f;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
		{
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(108, 45, 199);
	            }
	        }
	    }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        switch (Main.rand.Next(4))
			{
		    	case 0: type = Mod.Find<ModProjectile>("EonBeam").Type; break;
		    	case 1: type = Mod.Find<ModProjectile>("EonBeamV2").Type; break;
		    	case 2: type = Mod.Find<ModProjectile>("EonBeamV3").Type; break;
		    	case 3: type = Mod.Find<ModProjectile>("EonBeamV4").Type; break;
			}
	        int projectile = Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, Main.myPlayer);
	        Main.projectile[projectile].timeLeft = 160;
	        Main.projectile[projectile].tileCollide = false;
			float num72 = Main.rand.Next(22, 30);
			damage = Main.rand.Next(500, 621);
	    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
	    	float num78 = (float)Main.mouseX + Main.screenPosition.X + vector2.X;
			float num79 = (float)Main.mouseY + Main.screenPosition.Y + vector2.Y;
			if (player.gravDir == -1f)
			{
				num79 = Main.screenPosition.Y + (float)Main.screenHeight + (float)Main.mouseY + vector2.Y;
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
			int num107 = 4;
			for (int num108 = 0; num108 < num107; num108++)
			{
				vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(-(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y);
				vector2.X = (vector2.X + player.Center.X) / 2f;
				vector2.Y -= (float)(100 * num108);
				num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
				num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
				num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
				num80 = num72 / num80;
				num78 *= num80;
				num79 *= num80;
				float speedX4 = num78 + (float)Main.rand.Next(-360, 361) * 0.02f;
				float speedY5 = num79 + (float)Main.rand.Next(-360, 361) * 0.02f;
				int projectileFire = Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, speedY5, Mod.Find<ModProjectile>("Galaxia2").Type, damage, knockback, player.whoAmI, 0f, (float)Main.rand.Next(3));
				Main.projectile[projectileFire].timeLeft = 80;
			}
	    	return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "FourSeasonsGalaxia");
			recipe.AddIngredient(null, "ArkoftheElements");
			recipe.AddIngredient(null, "NightmareFuel", 5);
        	recipe.AddIngredient(null, "EndothermicEnergy", 5);
			recipe.AddIngredient(null, "HellcasterFragment", 5);
			recipe.AddIngredient(null, "DarksunFragment", 5);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(5))
			{
				int num250 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, DustID.RainbowTorch, (float)(player.direction * 2), 0f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.3f);
				Main.dust[num250].velocity *= 0.2f;
				Main.dust[num250].noGravity = true;
			}
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	bool jungle = player.ZoneJungle;
	        bool snow = player.ZoneSnow;
	       	bool beach = player.ZoneBeach;
	       	bool corrupt = player.ZoneCorrupt;
	       	bool crimson = player.ZoneCrimson;
	       	bool dungeon = player.ZoneDungeon;
	       	bool desert = player.ZoneDesert;
	       	bool glow = player.ZoneGlowshroom;
	       	bool hell = player.ZoneUnderworldHeight;
	       	bool holy = player.ZoneHallow;
	       	bool nebula = player.ZoneTowerNebula;
	       	bool stardust = player.ZoneTowerStardust;
	       	bool solar = player.ZoneTowerSolar;
	       	bool vortex = player.ZoneTowerVortex;
	       	bool bloodMoon = Main.bloodMoon;
	       	bool snowMoon = Main.snowMoon;
	       	bool pumpkinMoon = Main.pumpkinMoon;
	       	if (bloodMoon)
	       	{
	       		player.AddBuff(BuffID.Battle, 600);
	       	}
	       	if (snowMoon)
	       	{
	       		player.AddBuff(BuffID.RapidHealing, 600);
	       	}
	       	if (pumpkinMoon)
	       	{
	       		player.AddBuff(BuffID.WellFed, 600);
	       	}
	       	if (jungle)
	       	{
	       		player.AddBuff(BuffID.Thorns, 600);
	       	}
	       	else if (snow)
	       	{
	       		player.AddBuff(BuffID.Warmth, 600);
	       	}
	       	else if (beach)
	       	{
	       		player.AddBuff(BuffID.Wet, 600);
	       	}
	       	else if (corrupt)
	       	{
	       		player.AddBuff(BuffID.Wrath, 600);
	       	}
	       	else if (crimson)
	       	{
	       		player.AddBuff(BuffID.Rage, 600);
	       	}
	       	else if (dungeon)
	       	{
	       		player.AddBuff(BuffID.Dangersense, 600);
	       	}
	       	else if (desert)
	       	{
	       		player.AddBuff(BuffID.Endurance, 600);
	       	}
	       	else if (glow)
	       	{
	       		player.AddBuff(BuffID.Spelunker, 600);
	       	}
	       	else if (hell)
	       	{
	       		player.AddBuff(BuffID.Inferno, 600);
	       	}
	       	else if (holy)
	       	{
	       		player.AddBuff(BuffID.Heartreach, 600);
	       	}
	       	else if (nebula)
	       	{
	       		player.AddBuff(BuffID.MagicPower, 600);
	       	}
	       	else if (stardust)
	       	{
	       		player.AddBuff(BuffID.Summoning, 600);
	       	}
	       	else if (solar)
	       	{
	       		player.AddBuff(BuffID.Titan, 600);
	       	}
	       	else if (vortex)
	       	{
	       		player.AddBuff(BuffID.AmmoReservation, 600);
	       	}
	       	else
	       	{
	       		player.AddBuff(BuffID.DryadsWard, 600);
	       	}
		}
	}
}
