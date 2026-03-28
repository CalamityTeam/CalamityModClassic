using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Summon
{
	public class Spikecrag : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Spikecrag");
			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			Projectile.width = 70;
			Projectile.height = 42;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.sentry = true;
			Projectile.timeLeft = Projectile.SentryLifeTime;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 15;
		}

		public override void AI()
		{
			if (Projectile.localAI[0] == 0f)
			{
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue = Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base;
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue = Projectile.damage;
				Projectile.localAI[0] += 1f;
			}
			if (Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base != Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue)
			{
				int damage2 = (int)(((float)Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue /
					Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue) *
					Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base);
				Projectile.damage = damage2;
			}

			Projectile.frameCounter++;
			if (Projectile.frameCounter > 4)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 3)
			{
				Projectile.frame = 0;
			}
			Projectile.velocity.Y += 0.5f;

			if (Projectile.velocity.Y > 10f)
			{
				Projectile.velocity.Y = 10f;
			}

			float centerX = Projectile.Center.X;
			float centerY = Projectile.Center.Y;
			float num474 = 1000f;
			bool homeIn = false;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].CanBeChasedBy(Projectile, false))
				{
					float num476 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
					float num477 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
					float num478 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num476) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num477);
					if (num478 < num474)
					{
						num474 = num478;
						homeIn = true;
					}
				}
			}
			if (Projectile.owner == Main.myPlayer && homeIn)
			{
				if (Projectile.ai[0] != 0f)
				{
					Projectile.ai[0] -= 1f;
					return;
				}
				Projectile.ai[1] += 1f;
				if ((Projectile.ai[1] % 10f) == 0f)
				{
					int amount = Main.rand.Next(6, 10);
					for (int i = 0; i < amount; i++)
					{
						float velocityX = Main.rand.NextFloat(-10f, 10f);
						float velocityY = Main.rand.NextFloat(-15f, -8f);
						Projectile.NewProjectile(Projectile.GetSource_FromThis(null), Projectile.oldPosition.X + (float)(Projectile.width / 2), Projectile.oldPosition.Y + (float)(Projectile.height / 2), velocityX, velocityY, Mod.Find<ModProjectile>("SpikecragSpike").Type, (int)((double)Projectile.damage * 0.80), 0f, Projectile.owner, 0f, 0f);
					}
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.penetrate == 0)
			{
				Projectile.Kill();
			}
			return false;
		}

		public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
		{
			return false;
		}
	}
}
