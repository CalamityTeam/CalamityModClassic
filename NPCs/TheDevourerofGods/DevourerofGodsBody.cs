﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;

namespace CalamityModClassic1Point1.NPCs.TheDevourerofGods
{
	public class DevourerofGodsBody : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
        public override void SetDefaults()
		{
			//NPC.name = "The Devourer of Gods Body");
			//Tooltip.SetDefault("The Devourer of Gods");
			NPC.damage = 120; //70
			NPC.npcSlots = 5f;
			NPC.width = 38; //324
			NPC.height = 44; //216
			NPC.defense = 99999;
			NPC.lifeMax = 650000; //250000
			NPC.aiStyle = 6; //new
			Main.npcFrameCount[NPC.type] = 1; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.scale = 1.4f;
			NPC.alpha = 255;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.netAlways = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/CaveStoryBossBattle");
			NPC.dontCountMe = true;
			if (Main.expertMode)
			{
				NPC.scale = 1.5f;
			}
		}
		
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
		
		public override void AI()
		{
			bool expertMode = Main.expertMode;
			if (NPC.defense < 99999)
			{
				NPC.defense = 99999;
			}
			if (CalamityGlobalNPC.bossBuff || CalamityGlobalNPC.superBossBuff)
			{
				NPC.reflectsProjectiles = true;
			}
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.2f, 0.05f, 0.2f);
			if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.active = false;
            }
			if (Main.npc[(int)NPC.ai[1]].alpha < 128)
			{
				if (NPC.alpha != 0)
				{
					for (int num934 = 0; num934 < 2; num934++)
					{
						int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 182, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num935].noGravity = true;
						Main.dust[num935].noLight = true;
					}
				}
				NPC.alpha -= 42;
				if (NPC.alpha < 0)
				{
					NPC.alpha = 0;
				}
			}
			if (Main.netMode != 1)
			{
				NPC.localAI[0] += expertMode ? 4f : 3f;
				if (NPC.life <= NPC.lifeMax && NPC.life >= (NPC.lifeMax * 0.8f))
				{
					if (NPC.localAI[0] >= (float)Main.rand.Next(1400, 30000))
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							float num941 = 9f; //speed
							Vector2 vector104 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
							float num942 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector104.X + (float)Main.rand.Next(-20, 21);
							float num943 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector104.Y + (float)Main.rand.Next(-20, 21);
							float num944 = (float)Math.Sqrt((double)(num942 * num942 + num943 * num943));
							num944 = num941 / num944;
							num942 *= num944;
							num943 *= num944;
							num942 += (float)Main.rand.Next(-10, 11) * 0.05f;
							num943 += (float)Main.rand.Next(-10, 11) * 0.05f;
							int num945 = expertMode ? 55 : 80;
							int num946 = Mod.Find<ModProjectile>("DoGNebulaShot").Type;
							vector104.X += num942 * 5f;
							vector104.Y += num943 * 5f;
							int num947 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector104.X, vector104.Y, num942, num943, num946, num945, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[num947].timeLeft = 180;
							NPC.netUpdate = true;
						}
					}
				}
				else if (NPC.life < (NPC.lifeMax * 0.8f) && NPC.life >= (NPC.lifeMax * 0.6f))
				{
					if (NPC.localAI[0] >= (float)Main.rand.Next(3400, 40000))
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							float num941 = 0.1f; //speed
							Vector2 vector104 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
							float num942 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector104.X + (float)Main.rand.Next(-20, 21);
							float num943 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector104.Y + (float)Main.rand.Next(-20, 21);
							float num944 = (float)Math.Sqrt((double)(num942 * num942 + num943 * num943));
							num944 = num941 / num944;
							num942 *= num944;
							num943 *= num944;
							num942 += (float)Main.rand.Next(-30, 31) * 0.05f;
							num943 += (float)Main.rand.Next(-30, 31) * 0.05f;
							int num945 = expertMode ? 65 : 110;
							int num946 = Mod.Find<ModProjectile>("DoGBolt").Type;
							vector104.X += num942 * 5f;
							vector104.Y += num943 * 5f;
							int num947 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector104.X, vector104.Y, num942, num943, num946, num945, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[num947].timeLeft = 300;
							NPC.netUpdate = true;
						}
					}
				}
				else if (NPC.life < (NPC.lifeMax * 0.6f) && NPC.life >= (NPC.lifeMax * 0.4f))
				{
					if (NPC.localAI[0] >= (float)Main.rand.Next(1400, 50000))
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							float num941 = 15f;
							Vector2 vector104 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
							float num942 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector104.X + (float)Main.rand.Next(-20, 21);
							float num943 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector104.Y + (float)Main.rand.Next(-20, 21);
							float num944 = (float)Math.Sqrt((double)(num942 * num942 + num943 * num943));
							num944 = num941 / num944;
							num942 *= num944;
							num943 *= num944;
							num942 += (float)Main.rand.Next(-20, 21) * 0.05f;
							num943 += (float)Main.rand.Next(-20, 21) * 0.05f;
							int num945 = expertMode ? 65 : 90;
							int num946 = Mod.Find<ModProjectile>("DoGStar").Type;
							vector104.X += num942 * 5f;
							vector104.Y += num943 * 5f;
							int num947 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector104.X, vector104.Y, num942, num943, num946, num945, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[num947].timeLeft = 180;
							NPC.netUpdate = true;
						}
					}
				}
				else if (NPC.life < (NPC.lifeMax * 0.4f) && NPC.life >= (NPC.lifeMax * 0.2f))
				{
					if (NPC.localAI[0] >= (float)Main.rand.Next(6400, 60000))
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							float num941 = 8f;
							Vector2 vector104 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
							float num942 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector104.X + (float)Main.rand.Next(-20, 21);
							float num943 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector104.Y + (float)Main.rand.Next(-20, 21);
							float num944 = (float)Math.Sqrt((double)(num942 * num942 + num943 * num943));
							num944 = num941 / num944;
							num942 *= num944;
							num943 *= num944;
							num942 += (float)Main.rand.Next(-20, 21) * 0.05f;
							num943 += (float)Main.rand.Next(-20, 21) * 0.05f;
							int num945 = expertMode ? 70 : 90;
							int num946 = Mod.Find<ModProjectile>("DoGGravBomb").Type;
							vector104.X += num942 * 5f;
							vector104.Y += num943 * 5f;
							int num947 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector104.X, vector104.Y, num942, num943, num946, num945, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[num947].timeLeft = 300;
							NPC.netUpdate = true;
						}
					}
				}
				else if (NPC.life < (NPC.lifeMax * 0.2f))
				{
					if (NPC.localAI[0] >= (float)Main.rand.Next(1400, 30000))
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							float num941 = 10f;
							Vector2 vector104 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
							float num942 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector104.X + (float)Main.rand.Next(-20, 21);
							float num943 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector104.Y + (float)Main.rand.Next(-20, 21);
							float num944 = (float)Math.Sqrt((double)(num942 * num942 + num943 * num943));
							num944 = num941 / num944;
							num942 *= num944;
							num943 *= num944;
							num942 += (float)Main.rand.Next(-20, 21) * 0.05f;
							num943 += (float)Main.rand.Next(-20, 21) * 0.05f;
							int num945 = expertMode ? 60 : 100;
							int num946 = Mod.Find<ModProjectile>("DoGOrb").Type;
							vector104.X += num942 * 5f;
							vector104.Y += num943 * 5f;
							int num947 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector104.X, vector104.Y, num942, num943, num946, num945, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[num947].timeLeft = 200;
							NPC.netUpdate = true;
						}
					}
				}
			}
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			if (projectile.penetrate == -1 && !projectile.minion)
			{
				projectile.penetrate = 1;
			}
			else if (projectile.penetrate >= 1)
			{
				projectile.penetrate = 1;
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 173, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DoGBody").Type, 1f);
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 50;
				NPC.height = 50;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 20; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 40; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
			if (Main.dayTime)
			{
				if (Main.rand.Next(150) == 0)
				{
					Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
					NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("StasisProbe").Type);
					NetMessage.SendData(23, -1, -1, null, Mod.Find<ModNPC>("StasisProbe").Type, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			else if (NPC.life < (NPC.lifeMax * 0.75f) && NPC.life >= (NPC.lifeMax * 0.5f))
			{
				if (Main.rand.Next(300) == 0)
				{
					Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
					NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("StasisProbe").Type);
					NetMessage.SendData(23, -1, -1, null, Mod.Find<ModNPC>("StasisProbe").Type, 0f, 0f, 0f, 0, 0, 0);
				}
				if (Main.rand.Next(1000) == 0)
				{
					Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
					NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("DevourerSpawnHead").Type);
					NetMessage.SendData(23, -1, -1, null, Mod.Find<ModNPC>("DevourerSpawnHead").Type, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			else if (NPC.life < (NPC.lifeMax * 0.5f) && NPC.life >= (NPC.lifeMax * 0.25f))
			{
				if (Main.rand.Next(225) == 0)
				{
					Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
					NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("StasisProbe").Type);
					NetMessage.SendData(23, -1, -1, null, Mod.Find<ModNPC>("StasisProbe").Type, 0f, 0f, 0f, 0, 0, 0);
				}
				if (Main.rand.Next(750) == 0)
				{
					Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
					NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("DevourerSpawnHead").Type);
					NetMessage.SendData(23, -1, -1, null, Mod.Find<ModNPC>("DevourerSpawnHead").Type, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			else if (NPC.life < (NPC.lifeMax * 0.25f))
			{
				if (Main.rand.Next(150) == 0)
				{
					Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
					NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("StasisProbe").Type);
					NetMessage.SendData(23, -1, -1, null, Mod.Find<ModNPC>("StasisProbe").Type, 0f, 0f, 0f, 0, 0, 0);
				}
				if (Main.rand.Next(500) == 0)
				{
					Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
					NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("DevourerSpawnHead").Type);
					NetMessage.SendData(23, -1, -1, null, Mod.Find<ModNPC>("DevourerSpawnHead").Type, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * balance);
			NPC.damage = (int)(NPC.damage * 0.65f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Frostburn, 100, true);
				target.AddBuff(BuffID.Darkness, 100, true);
			}
		}
	}
}