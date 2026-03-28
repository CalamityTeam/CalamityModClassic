using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Summon
{
    public class DrewsSandyWaifu : ModProjectile
    {
    	public int dust = 3;
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Oxy's Waifu");
			Main.projFrames[Projectile.type] = 5;
			Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 42;
			Projectile.height = 98;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 0f;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 20 -
				(NPC.downedGolemBoss ? 5 : 0) -
        		(NPC.downedMoonlord ? 5 : 0) -
        		(CalamityWorldPreTrailer.downedDoG ? 4 : 0) -
        		(CalamityWorldPreTrailer.downedYharon ? 3 : 0);
        }

		public override void AI()
		{
			bool flag64 = Projectile.type == Mod.Find<ModProjectile>("DrewsSandyWaifu").Type;
			Player player = Main.player[Projectile.owner];
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (!modPlayer.sandBoobWaifu && !modPlayer.allWaifus)
			{
				Projectile.active = false;
				return;
			}
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.dWaifu = false;
				}
				if (modPlayer.dWaifu)
				{
					Projectile.timeLeft = 2;
				}
			}
			dust--;
			if (dust >= 0)
			{
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue = Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base;
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue = Projectile.damage;
				int num501 = 50;
				for (int num502 = 0; num502 < num501; num502++)
				{
					int num503 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 16f), Projectile.width, Projectile.height - 16, 32, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num503].velocity *= 2f;
					Main.dust[num503].scale *= 1.15f;
				}
			}
			if (Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base != Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue)
			{
				int damage2 = (int)(((float)Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue /
					Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue) *
					Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base);
				Projectile.damage = damage2;
			}
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 16)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 3)
			{
				Projectile.frame = 0;
			}
			if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
			{
				Projectile.spriteDirection = -Projectile.direction;
			}
			float num636 = 100f; //150
			float num = (float)Main.rand.Next(90, 111) * 0.01f;
			num *= Main.essScale;
			Lighting.AddLight(Projectile.Center, 0.7f * num, 0.6f * num, 0f * num);
			float num637 = 0.05f;
			for (int num638 = 0; num638 < 1000; num638++)
			{
				bool flag23 = (Main.projectile[num638].type == Mod.Find<ModProjectile>("DrewsSandyWaifu").Type);
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
			if (Vector2.Distance(player.Center, Projectile.Center) > 400f)
			{
				Projectile.ai[0] = 1f;
				Projectile.tileCollide = false;
				Projectile.netUpdate = true;
			}
			bool flag26 = false;
			if (!flag26)
			{
				flag26 = (Projectile.ai[0] == 1f);
			}
			float num650 = 7f; //6
			if (flag26)
			{
				num650 = 18f; //15
			}
			Vector2 center2 = Projectile.Center;
			Vector2 vector48 = player.Center - center2 + new Vector2(-250f, -60f); //-60
			float num651 = vector48.Length();
			if (num651 > 200f && num650 < 10f) //200 and 8
			{
				num650 = 10f; //8
			}
			if (num651 < num636 && flag26 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
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
				Projectile.velocity.X = -0.22f;
				Projectile.velocity.Y = -0.12f;
			}
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 16)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 4)
			{
				Projectile.frame = 0;
			}
			if (Projectile.ai[1] > 0f)
			{
				Projectile.ai[1] += (float)Main.rand.Next(1, 4);
			}
			if (Projectile.ai[1] > 220f)
			{
				Projectile.ai[1] = 0f;
				Projectile.netUpdate = true;
			}
			if (Projectile.localAI[0] < 120f)
			{
				Projectile.localAI[0] += 1f;
			}
			if (Projectile.ai[0] == 0f)
			{
				int num658 = Mod.Find<ModProjectile>("CactusHealOrb").Type;
				if (Projectile.ai[1] == 0f && Projectile.localAI[0] >= 120f)
				{
					Projectile.ai[1] += 1f;
					if (Main.myPlayer == Projectile.owner && Main.player[Projectile.owner].statLife < Main.player[Projectile.owner].statLifeMax2)
					{
						SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
						int num226 = 36;
						for (int num227 = 0; num227 < num226; num227++)
						{
							Vector2 vector6 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f;
							vector6 = vector6.RotatedBy((double)((float)(num227 - (num226 / 2 - 1)) * 6.28318548f / (float)num226), default(Vector2)) + Projectile.Center;
							Vector2 vector7 = vector6 - Projectile.Center;
							int num228 = Dust.NewDust(vector6 + vector7, 0, 0, 107, vector7.X * 1.5f, vector7.Y * 1.5f, 100, new Color(0, 200, 0), 1f);
							Main.dust[num228].noGravity = true;
							Main.dust[num228].noLight = true;
							Main.dust[num228].velocity = vector7;
						}
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, -6f, num658, 0, 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
		}
    }
}