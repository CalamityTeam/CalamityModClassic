using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;

namespace CalamityModClassic1Point2.NPCs.Scavenger
{
	public class ScavengerHead : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ravager");
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 0;
			NPC.width = 64; //324
			NPC.height = 80; //216
			NPC.defense = 70;
			NPC.lifeMax = 20000;
			NPC.knockBackResist = 0f;
			AIType = -1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.noGravity = true;
			NPC.alpha = 255;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = null;
			if (CalamityWorld1Point2.downedProvidence)
			{
				NPC.damage = 0;
				NPC.defense = 150;
				NPC.lifeMax = 400000;
			}
		}
		
		public override void AI()
		{
			bool provy = CalamityWorld1Point2.downedProvidence;
			bool expertMode = Main.expertMode;
			Player player = Main.player[NPC.target];
			NPC.noTileCollide = true;
			if (CalamityGlobalNPC1Point2.scavenger < 0)
            {
                NPC.SimpleStrikeNPC(9999, 0, false, noPlayerInteraction: true);
                return;
			}
			if (NPC.timeLeft > 1800)
			{
				NPC.timeLeft = 1800;
			}
			float speed = 12f;
			Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y);
			float centerX = Main.npc[CalamityGlobalNPC1Point2.scavenger].Center.X - center.X;
			float centerY = Main.npc[CalamityGlobalNPC1Point2.scavenger].Center.Y - center.Y;
			centerY -= 20f;
			centerX -= 0f;
			float totalSpeed = (float)Math.Sqrt((double)(centerX * centerX + centerY * centerY));
			if (totalSpeed < 20f) 
			{
				NPC.rotation = 0f;
				NPC.velocity.X = centerX;
				NPC.velocity.Y = centerY;
			} 
			else
			{
				totalSpeed = speed / totalSpeed;
				NPC.velocity.X = centerX * totalSpeed;
				NPC.velocity.Y = centerY * totalSpeed;
				NPC.rotation = NPC.velocity.X * 0.1f;
			}
			if (NPC.alpha > 0) 
			{
				NPC.alpha -= 10;
				if (NPC.alpha < 0) 
				{
					NPC.alpha = 0;
				}
				NPC.ai[1] = 30f;
			}
			if (NPC.ai[0] == 0f) 
			{
				NPC.ai[1] += 1f;
				int nukeTimer = 300;
				if (NPC.ai[1] < 20f || NPC.ai[1] > (float)(nukeTimer - 20)) 
				{
					NPC.localAI[0] = 1f;
				} 
				else
				{
					NPC.localAI[0] = 0f;
				}
				if (NPC.ai[1] >= (float)nukeTimer) 
				{
					SoundEngine.PlaySound(SoundID.Item62, NPC.position);
					NPC.TargetClosest(true);
					NPC.ai[1] = 0f;
					Vector2 shootFromVector = new Vector2(NPC.Center.X, NPC.Center.Y - 20f);
					float nukeSpeed = 1f;
					float playerDistanceX = player.position.X + (float)player.width * 0.5f - shootFromVector.X;
					float playerDistanceY = player.position.Y + (float)player.height * 0.5f - shootFromVector.Y;
					float totalPlayerDistance = (float)Math.Sqrt((double)(playerDistanceX * playerDistanceX + playerDistanceY * playerDistanceY));
					totalPlayerDistance = nukeSpeed / totalPlayerDistance;
					playerDistanceX *= totalPlayerDistance;
					playerDistanceY *= totalPlayerDistance;
					int nukeDamage = expertMode ? 82 : 90;
					int projectileType = Mod.Find<ModProjectile>("ScavengerNuke").Type;
					if (Main.netMode != NetmodeID.MultiplayerClient) 
					{
						int nuke = Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, playerDistanceX, playerDistanceY, projectileType, nukeDamage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[nuke].velocity.Y = -15f;
					}
				}
			} 
			else if (NPC.ai[0] == 1f)
			{
				NPC.TargetClosest(true);
				Vector2 shootFromVector = new Vector2(NPC.Center.X, NPC.Center.Y - 20f);
				NPC.ai[1] += 1f;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.6) 
				{
					NPC.ai[1] += 1f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.3) 
				{
					NPC.ai[1] += 1f;
				}
				int laserTimer = 300;
				if (NPC.ai[1] < 20f || NPC.ai[1] > (float)(laserTimer - 20)) 
				{
					NPC.localAI[0] = 1f;
				} 
				else
				{
					NPC.localAI[0] = 0f;
				}
				if (NPC.ai[1] >= (float)laserTimer) 
				{
					SoundEngine.PlaySound(SoundID.Item33, NPC.position);
					NPC.TargetClosest(true);
					NPC.ai[1] = 0f;
					float laserSpeed = 1f;
					float playerDistanceX = player.position.X + (float)player.width * 0.5f - shootFromVector.X;
					float playerDistanceY = player.position.Y + (float)player.height * 0.5f - shootFromVector.Y;
					float totalPlayerDistance = (float)Math.Sqrt((double)(playerDistanceX * playerDistanceX + playerDistanceY * playerDistanceY));
					totalPlayerDistance = laserSpeed / totalPlayerDistance;
					playerDistanceX *= totalPlayerDistance;
					playerDistanceY *= totalPlayerDistance;
					int laserDamage = expertMode ? 45 : 49;
					int projectileType = Mod.Find<ModProjectile>("ScavengerLaser").Type;
					if (Main.netMode != NetmodeID.MultiplayerClient) 
					{
						int laser = Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, playerDistanceX, playerDistanceY, projectileType, laserDamage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[laser].velocity.Y = -12f;
					}
				}
				NPC.ai[2] += 1f;
				if (NPC.life < NPC.lifeMax / 3) 
				{
					NPC.ai[2] += 1f;
				}
				if (NPC.life < NPC.lifeMax / 4) 
				{
					NPC.ai[2] += 1f;
				}
				if (NPC.life < NPC.lifeMax / 5) 
				{
					NPC.ai[2] += 1f;
				}
				if (!Collision.CanHit(NPC.Center, 1, 1, player.Center, 1, 1)) 
				{
					NPC.ai[2] += 4f;
				}
				if (NPC.ai[2] > (float)(600 + Main.rand.Next(201))) 
				{
					SoundEngine.PlaySound(SoundID.Item33, NPC.position);
					NPC.ai[2] = 0f;
					int laserDamage = expertMode ? 45 : 49;
					int projectileType = Mod.Find<ModProjectile>("ScavengerLaser").Type;
					shootFromVector = new Vector2(NPC.Center.X, NPC.Center.Y - 20f);
					float laserSpeed = 1f;
					float playerDistanceX = player.position.X + (float)player.width * 0.5f - shootFromVector.X;
					float playerDistanceY = player.position.Y + (float)player.height * 0.5f - shootFromVector.Y;
					float totalPlayerDistance = (float)Math.Sqrt((double)(playerDistanceX * playerDistanceX + playerDistanceY * playerDistanceY));
					totalPlayerDistance = laserSpeed / totalPlayerDistance;
					playerDistanceX *= totalPlayerDistance;
					playerDistanceY *= totalPlayerDistance;
					shootFromVector.X += playerDistanceX * 3f;
					shootFromVector.Y += playerDistanceY * 3f;
					if (Main.netMode != NetmodeID.MultiplayerClient) 
					{
						int laser = Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, playerDistanceX, playerDistanceY, projectileType, laserDamage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[laser].timeLeft = 300;
						Main.projectile[laser].velocity.Y = -9f;
					}
				}
			}
			if (NPC.life < NPC.lifeMax / 2) 
			{
				NPC.ai[0] = 1f;
				return;
			}
			NPC.ai[0] = 0f;
			return;
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = 0;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life > 0)
			{
				int num285 = 0;
				while ((double)num285 < hit.Damage / (double)NPC.lifeMax * 100.0)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, (float)hit.HitDirection, -1f, 0, default(Color), 1f);
					num285++;
				}
			}
			else if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.position.Y + NPC.height, Mod.Find<ModNPC>("ScavengerHead2").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
			}
		}
	}
}