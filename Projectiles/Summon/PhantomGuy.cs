using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Summon
{
    public class PhantomGuy : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Phantom");
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 0.5f;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
        }

        public override void AI()
        {
        	if (Projectile.localAI[0] == 0f)
        	{
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue = Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base;
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue = Projectile.damage;
				int num226 = 36;
				for (int num227 = 0; num227 < num226; num227++)
				{
					Vector2 vector6 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f;
					vector6 = vector6.RotatedBy((double)((float)(num227 - (num226 / 2 - 1)) * 6.28318548f / (float)num226), default(Vector2)) + Projectile.Center;
					Vector2 vector7 = vector6 - Projectile.Center;
					int num228 = Dust.NewDust(vector6 + vector7, 0, 0, 180, vector7.X * 1.75f, vector7.Y * 1.75f, 100, default(Color), 1.1f);
					Main.dust[num228].noGravity = true;
					Main.dust[num228].velocity = vector7;
				}
				Projectile.localAI[0] += 1f;
        	}
			if (Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base != Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue)
			{
				int damage2 = (int)(((float)Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue /
					Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue) *
					Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base);
				Projectile.damage = damage2;
			}
			float num633 = 700f;
			float num634 = 1300f;
			float num635 = 2600f;
			float num636 = 600f;
			bool flag64 = Projectile.type == Mod.Find<ModProjectile>("PhantomGuy").Type;
			Player player = Main.player[Projectile.owner];
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			player.AddBuff(Mod.Find<ModBuff>("Phantom").Type, 3600);
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.pGuy = false;
				}
				if (modPlayer.pGuy)
				{
					Projectile.timeLeft = 2;
				}
			}
			float num637 = 0.05f;
			for (int num638 = 0; num638 < 1000; num638++)
			{
				bool flag23 = (Main.projectile[num638].type == Mod.Find<ModProjectile>("PhantomGuy").Type);
				if (num638 != Projectile.whoAmI && Main.projectile[num638].active && Main.projectile[num638].owner == Projectile.owner && flag23 && Math.Abs(Projectile.position.X - Main.projectile[num638].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[num638].position.Y) < (float)Projectile.width)
				{
					if (Projectile.position.X < Main.projectile[num638].position.X)
					{
						Projectile.velocity.X = Projectile.velocity.X - num637;
					}
					else
					{
						Projectile.velocity.X = Projectile.velocity.X + num637;
					}
					if (Projectile.position.Y < Main.projectile[num638].position.Y)
					{
						Projectile.velocity.Y = Projectile.velocity.Y - num637;
					}
					else
					{
						Projectile.velocity.Y = Projectile.velocity.Y + num637;
					}
				}
			}
			Vector2 vector46 = Projectile.position;
			bool flag25 = false;
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if (npc.CanBeChasedBy(Projectile, false))
				{
					float num646 = Vector2.Distance(npc.Center, Projectile.Center);
					if ((Vector2.Distance(Projectile.Center, vector46) > num646 && num646 < num633) || !flag25)
					{
						num633 = num646;
						vector46 = npc.Center;
						flag25 = true;
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
						}
					}
				}
			}
			float num647 = num634;
			if (flag25)
			{
				num647 = num635;
			}
			if (Vector2.Distance(player.Center, Projectile.Center) > num647)
			{
				Projectile.ai[0] = 1f;
				Projectile.netUpdate = true;
			}
			if (flag25 && Projectile.ai[0] == 0f)
			{
				Vector2 vector47 = vector46 - Projectile.Center;
				float num648 = vector47.Length();
				vector47.Normalize();
				if (num648 > 200f)
				{
					float scaleFactor2 = 18f; //12
					vector47 *= scaleFactor2;
					Projectile.velocity = (Projectile.velocity * 40f + vector47) / 41f;
				}
				else
				{
					float num649 = 9f;
					vector47 *= -num649;
					Projectile.velocity = (Projectile.velocity * 40f + vector47) / 41f;
				}
			}
			else
			{
				bool flag26 = false;
				if (!flag26)
				{
					flag26 = (Projectile.ai[0] == 1f);
				}
				float num650 = 12f;
				if (flag26)
				{
					num650 = 30f;
				}
				Vector2 center2 = Projectile.Center;
				Vector2 vector48 = player.Center - center2 + new Vector2(0f, -120f);
				float num651 = vector48.Length();
				if (num651 > 200f && num650 < 16f)
				{
					num650 = 16f;
				}
				if (num651 < num636 && flag26)
				{
					Projectile.ai[0] = 0f;
					Projectile.netUpdate = true;
				}
				if (num651 > 2000f)
				{
					Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
					Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.height / 2);
					Projectile.netUpdate = true;
				}
				if (num651 > 70f)
				{
					vector48.Normalize();
					vector48 *= num650;
					Projectile.velocity = (Projectile.velocity * 40f + vector48) / 41f;
				}
				else if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
				{
					Projectile.velocity.X = -0.15f;
					Projectile.velocity.Y = -0.05f;
				}
			}
			if (flag25)
			{
				Projectile.rotation = (vector46 - Projectile.Center).ToRotation() + 3.14159274f;
			}
			else
			{
				Projectile.rotation = Projectile.velocity.ToRotation() + 3.14159274f;
			}
			if (Projectile.ai[1] > 0f)
			{
				Projectile.ai[1] += (float)Main.rand.Next(1, 4);
			}
			if (Projectile.ai[1] > 90f)
			{
				Projectile.ai[1] = 0f;
				Projectile.netUpdate = true;
			}
			if (Projectile.ai[0] == 0f)
			{
				float scaleFactor3 = 6f;
				int num658 = Mod.Find<ModProjectile>("GhostFire").Type;
				if (flag25 && Projectile.ai[1] == 0f)
				{
					SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
					Projectile.ai[1] += 1f;
					if (Main.myPlayer == Projectile.owner)
					{
						Vector2 value19 = vector46 - Projectile.Center;
						value19.Normalize();
						value19 *= scaleFactor3;
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, value19.X, value19.Y, num658, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
						Projectile.netUpdate = true;
					}
				}
			}
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(100, 250, 250, Projectile.alpha);
        }

		public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
		{
			return false;
		}
	}
}