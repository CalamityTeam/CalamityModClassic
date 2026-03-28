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
	public class GrandGuardian : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Grand Guardian");
			/* Tooltip.SetDefault("Has a chance to lower enemy defense by 15 when striking them\n" +
			           "If enemy defense is 0 or below your attacks will heal you\n" +
			           "Striking enemies causes a large explosion\n" +
			           "Striking enemies that have under half life will make you release rainbow bolts\n" +
			           "Enemies spawn healing orbs on death"); */
		}

		public override void SetDefaults()
		{
			Item.width = 124;
			Item.damage = 160;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 22;
			Item.useStyle = 1;
			Item.useTime = 22;
			Item.useTurn = true;
			Item.knockBack = 8.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 124;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 10;
			Item.shootSpeed = 12f;
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (Main.rand.Next(5) == 0)
			{
				target.defense -= 15;
			}
			if (target.defense <= 0 && target.canGhostHeal)
			{
		    	player.statLife += 4;
		    	player.HealEffect(4);
			}
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("RainbowBoom").Type, (int)((double)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative) * 0.5), 0f, Main.myPlayer);
			float spread = 180f * 0.0174f;
			double startAngle = Math.Atan2(Item.shootSpeed, Item.shootSpeed) - spread / 2;
			double deltaAngle = spread / 8f;
			double offsetAngle;
			int i;
			if (target.life <= (target.lifeMax * 0.5f))
			{
				for (i = 0; i < 1; i++ )
				{
					float randomSpeedX = (float)Main.rand.Next(9);
					float randomSpeedY = (float)Main.rand.Next(6, 15);
				   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
				   	int projectile1 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainBolt").Type, (int)((double)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative) * 0.75), Item.knockBack, Main.myPlayer);
				    int projectile2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainBolt").Type, (int)((double)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative) * 0.75), Item.knockBack, Main.myPlayer);
					int projectile3 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainBolt").Type, (int)((double)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative) * 0.75), Item.knockBack, Main.myPlayer);
				    Main.projectile[projectile1].velocity.X = -randomSpeedX;
				    Main.projectile[projectile1].velocity.Y = -randomSpeedY;
				    Main.projectile[projectile2].velocity.X = randomSpeedX;
				    Main.projectile[projectile2].velocity.Y = -randomSpeedY;
				    Main.projectile[projectile3].velocity.X = 0f;
				    Main.projectile[projectile3].velocity.Y = -randomSpeedY;
				}
			}
			if (target.life <= 0)
			{
		   		for (i = 0; i < 1; i++ )
				{
					float randomSpeedX = (float)Main.rand.Next(9);
					float randomSpeedY = (float)Main.rand.Next(6, 15);
				   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
				   	int projectile1 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainHeal").Type, Item.damage, Item.knockBack, Main.myPlayer);
				    int projectile2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainHeal").Type, Item.damage, Item.knockBack, Main.myPlayer);
					int projectile3 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainHeal").Type, Item.damage, Item.knockBack, Main.myPlayer);
				    Main.projectile[projectile1].velocity.X = -randomSpeedX;
				    Main.projectile[projectile1].velocity.Y = -randomSpeedY;
				    Main.projectile[projectile2].velocity.X = randomSpeedX;
				    Main.projectile[projectile2].velocity.Y = -randomSpeedY;
				    Main.projectile[projectile3].velocity.X = 0f;
				    Main.projectile[projectile3].velocity.Y = -randomSpeedY;
				}
			}
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(3) == 0)
	        {
	            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 66, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f);
	            Main.dust[dust].noGravity = true;
	        }
	    }
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "MajesticGuard");
			recipe.AddIngredient(null, "BarofLife", 10);
			recipe.AddIngredient(null, "GalacticaSingularity", 3);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	}
}
