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
	public class GrandGuardian : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 124;
			Item.damage = 300;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 22;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 22;
			Item.useTurn = true;
			Item.knockBack = 8.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 124;
			Item.maxStack = 1;
			Item.value = 5000000;
			Item.rare = ItemRarityID.Red;
			Item.shootSpeed = 12f;
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			if (target.type == NPCID.TargetDummy)
			{
				return;
			}
			if (Main.rand.NextBool(2))
			{
				target.defense -= 30;
			}
			if (target.defense <= 0)
			{
		    	player.statLife += 12;
		    	player.HealEffect(12);
			}
			Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("RainbowBoom").Type, (int)((double)hit.Damage * 0.5f), hit.Knockback, Main.myPlayer);
			float spread = 180f * 0.0174f;
			double startAngle = Math.Atan2(Item.shootSpeed, Item.shootSpeed)- spread/2;
			double deltaAngle = spread/8f;
			double offsetAngle;
			int i;
			if (target.life <= (target.lifeMax * 0.5f))
			{
				for (i = 0; i < 1; i++ )
				{
					float randomSpeedX = (float)Main.rand.Next(9);
					float randomSpeedY = (float)Main.rand.Next(6, 15);
				   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
				   	int projectile1 = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainBolt").Type, (int)((double)hit.Damage * 0.5f), hit.Knockback, Main.myPlayer);
				    int projectile2 = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainBolt").Type, (int)((double)hit.Damage * 0.5f), hit.Knockback, Main.myPlayer);
					int projectile3 = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainBolt").Type, (int)((double)hit.Damage * 0.5f), hit.Knockback, Main.myPlayer);
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
				   	int projectile1 = Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainHeal").Type, hit.Damage, hit.Knockback, Main.myPlayer);
				    int projectile2 = Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainHeal").Type, hit.Damage, hit.Knockback, Main.myPlayer);
					int projectile3 = Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainHeal").Type, hit.Damage, hit.Knockback, Main.myPlayer);
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
	        if (Main.rand.NextBool(3))
	        {
	            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.RainbowTorch, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f);
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
