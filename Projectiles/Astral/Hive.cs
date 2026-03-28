using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Astral
{
	public class Hive : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hive");
			Main.projFrames[Projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			Projectile.width = 38;
			Projectile.height = 60;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.sentry = true;
			Projectile.timeLeft = Projectile.SentryLifeTime;
			Projectile.penetrate = -1;
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
			if (Projectile.frameCounter > 5)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 5)
			{
				Projectile.frame = 0;
			}
			Projectile.velocity.Y += 0.5f;

			if (Projectile.velocity.Y > 10f)
			{
				Projectile.velocity.Y = 10f;
			}

			int target = 0;
			float num633 = 800f;
			Vector2 vector46 = Projectile.position;
			bool flag25 = false;
			if (Main.player[Projectile.owner].HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[Main.player[Projectile.owner].MinionAttackTargetNPC];
				if (npc.CanBeChasedBy(Projectile, false))
				{
					float num646 = Vector2.Distance(npc.Center, Projectile.Center);
					if ((Vector2.Distance(Projectile.Center, vector46) > num646 && num646 < num633) || !flag25)
					{
						num633 = num646;
						vector46 = npc.Center;
						flag25 = true;
						target = npc.whoAmI;
					}
				}
			}
			else
			{
				for (int num645 = 0; num645 < 200; num645++)
				{
					NPC nPC2 = Main.npc[num645];
					if (nPC2.CanBeChasedBy(Projectile, false))
					{
						float num646 = Vector2.Distance(nPC2.Center, Projectile.Center);
						if ((Vector2.Distance(Projectile.Center, vector46) > num646 && num646 < num633) || !flag25)
						{
							num633 = num646;
							vector46 = nPC2.Center;
							flag25 = true;
							target = num645;
						}
					}
				}
			}
			if (Projectile.owner == Main.myPlayer && flag25)
			{
				if (Projectile.ai[0] != 0f)
				{
					Projectile.ai[0] -= 1f;
					return;
				}
				Projectile.ai[1] += 1f;
				if ((Projectile.ai[1] % 15f) == 0f)
				{
					float velocityX = Main.rand.NextFloat(-0.4f, 0.4f);
					float velocityY = Main.rand.NextFloat(-0.3f, -0.5f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, velocityX, velocityY, Mod.Find<ModProjectile>("Hiveling").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, (float)target, 0f);
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
