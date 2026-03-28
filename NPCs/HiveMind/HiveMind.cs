using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.NPCs.HiveMind
{
	[AutoloadBossHead]
	public class HiveMind : ModNPC
	{
		int burrowTimer = 720;
        int oldDamage = 10;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Hive Mind");
			Main.npcFrameCount[NPC.type] = 4;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 5f;
			NPC.damage = 10;
			NPC.width = 150; //324
			NPC.height = 120; //216
			NPC.defense = 10;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 1800 : 1200;
            if (CalamityWorldPreTrailer.death)
            {
                NPC.lifeMax = 3300;
            }
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = CalamityWorldPreTrailer.death ? 400000 : 350000;
            }
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("TemporalSadness").Type] = true;
			NPC.knockBackResist = 0f;
			NPC.boss = true;
            NPC.value = 0f;
            NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/HiveMind");
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override void AI()
		{
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];
			if (!player.active || player.dead)
			{
				if (NPC.timeLeft > 60)
					NPC.timeLeft = 60;
				if (NPC.localAI[3] < 120f) 
				{
					float[] aiArray = NPC.localAI;
					int number = 3;
					float num244 = aiArray[number];
					aiArray[number] = num244 + 1f;
				}
				if (NPC.localAI[3] > 60f) 
				{
					NPC.velocity.Y = NPC.velocity.Y + (NPC.localAI[3] - 60f) * 0.5f;
					NPC.noGravity = true;
					NPC.noTileCollide = true;
					if (burrowTimer > 30)
						burrowTimer = 30;
				}
				return;
			}
			if (NPC.localAI[3] > 0f) 
			{
				float[] aiArray = NPC.localAI;
				int number = 3;
				float num244 = aiArray[number];
				aiArray[number] = num244 - 1f;
				return;
			}
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			CalamityGlobalNPC.hiveMind = NPC.whoAmI;
			if (Main.netMode != 1) 
			{
				if (revenge)
				{
					NPC.localAI[1] += 1f;
					if (NPC.localAI[1] >= 600f)
					{
						NPC.localAI[1] = 0f;
						NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("HiveBlob").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
					}
				}
				if (NPC.localAI[0] == 0f) 
				{
					NPC.localAI[0] = 1f;
					for (int num723 = 0; num723 < 5; num723++) 
					{
						NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("HiveBlob").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
					}
				}
			}
			bool flag100 = false;
			int num568 = 0;
			if (expertMode)
			{
				for (int num569 = 0; num569 < 200; num569++)
				{
					if (Main.npc[num569].active && Main.npc[num569].type == Mod.Find<ModNPC>("DankCreeper").Type)
					{
						flag100 = true;
						num568++;
					}
				}
				NPC.defense += num568 * 25;
			}
			if (expertMode)
			{
				if (!flag100)
				{
					NPC.defense = 10;
				}
			}
			if (NPC.ai[3] == 0f && NPC.life > 0)
			{
				NPC.ai[3] = (float)NPC.lifeMax;
			}
	       	if (NPC.life > 0)
			{
				if (Main.netMode != 1)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.25);
					if ((float)(NPC.life + num660) < NPC.ai[3])
					{
						NPC.ai[3] = (float)NPC.life;
						int num661 = Main.rand.Next(3, 6);
						for (int num662 = 0; num662 < num661; num662++)
						{
							int x = (int)(NPC.position.X + (float)Main.rand.Next(NPC.width - 32));
							int y = (int)(NPC.position.Y + (float)Main.rand.Next(NPC.height - 32));
							int num663 = Mod.Find<ModNPC>("HiveBlob").Type;
							if (Main.rand.Next(3) == 0 || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
							{
								num663 = Mod.Find<ModNPC>("DankCreeper").Type;
							}
							int num664 = NPC.NewNPC(NPC.GetSource_FromThis(null), x, y, num663, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[num664].SetDefaults(num663, default);
							Main.npc[num664].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
							Main.npc[num664].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
							if (Main.netMode == 2 && num664 < 200)
							{
								NetMessage.SendData(23, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
							}
						}
						return;
					}
				}
			}
			burrowTimer--;
			if (burrowTimer < -120)
			{
				burrowTimer = 600;
				NPC.scale = 1f;
				NPC.alpha = 0;
				NPC.dontTakeDamage = false;
                NPC.damage = oldDamage;
            }
			else if (burrowTimer < -60)
			{
				NPC.scale += 0.0165f;
				NPC.alpha -= 4;
				int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.Center.Y), NPC.width, NPC.height / 2, 14, 0f, -3f, 100, default(Color), 2.5f * NPC.scale);
				Main.dust[num622].velocity *= 2f;
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
				for (int i = 0; i < 2; i++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.Center.Y), NPC.width, NPC.height / 2, 14, 0f, -3f, 100, default(Color), 3.5f * NPC.scale);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 3.5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.Center.Y), NPC.width, NPC.height / 2, 14, 0f, -3f, 100, default(Color), 2.5f * NPC.scale);
					Main.dust[num624].velocity *= 1f;
				}
			}
			else if (burrowTimer == -60)
			{
				NPC.scale = 0.01f;
				if (Main.netMode != 1)
				{
					NPC.Center = player.Center;
					NPC.position.Y = player.position.Y - NPC.height;
					int tilePosX = (int)NPC.Center.X / 16;
					int tilePosY = (int)(NPC.position.Y + NPC.height) / 16 + 1;
				}
                NPC.netUpdate = true;
            }
			else if (burrowTimer < 0)
			{
				NPC.scale -= 0.0165f;
				NPC.alpha += 4;
				int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.Center.Y), NPC.width, NPC.height / 2, 14, 0f, -3f, 100, default(Color), 2.5f * NPC.scale);
				Main.dust[num622].velocity *= 2f;
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
				for (int i = 0; i < 2; i++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.Center.Y), NPC.width, NPC.height / 2, 14, 0f, -3f, 100, default(Color), 3.5f * NPC.scale);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 3.5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.Center.Y), NPC.width, NPC.height / 2, 14, 0f, -3f, 100, default(Color), 2.5f * NPC.scale);
					Main.dust[num624].velocity *= 1f;
				}
			}
			else if (burrowTimer == 0)
			{
				if (!player.active || player.dead)
				{
					burrowTimer = 30;
				}
				else
				{
					NPC.dontTakeDamage = true;
                    oldDamage = NPC.damage;
                    NPC.damage = 0;
                }
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
            NPC.damage = (int)(NPC.damage * 0.8f);
        }
		
		public override void HitEffect(NPC.HitInfo hit)
		{
            if (NPC.life > 0)
            {
                if (NPC.CountNPCS(NPCID.EaterofSouls) < 3 && NPC.CountNPCS(NPCID.DevourerHead) < 1)
                {
                    if (Main.rand.Next(60) == 0 && Main.netMode != 1)
                    {
                        Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
                        NPC.NewNPC(NPC.GetSource_FromThis(null), (int)spawnAt.X, (int)spawnAt.Y, NPCID.EaterofSouls);
                    }
                    if (Main.rand.Next(150) == 0 && Main.netMode != 1)
                    {
                        Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
                        NPC.NewNPC(NPC.GetSource_FromThis(null), (int)spawnAt.X, (int)spawnAt.Y, NPCID.DevourerHead);
                    }
                }
                int num285 = 0;
                while ((double)num285 < NPC.damage / (double)NPC.lifeMax * 100.0)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 14, (float)hit.HitDirection, -1f, 0, default(Color), 1f);
                    num285++;
                }
            }
            else
            {
	            if (Main.netMode != NetmodeID.Server)
	            {
		            Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
			            Mod.Find<ModGore>("HiveMindGore").Type, 1f);
		            Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
			            Mod.Find<ModGore>("HiveMindGore2").Type, 1f);
		            Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
			            Mod.Find<ModGore>("HiveMindGore3").Type, 1f);
		            Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
			            Mod.Find<ModGore>("HiveMindGore4").Type, 1f);
	            }
	            if (Main.netMode != 1)
				{
					if (NPC.CountNPCS(Mod.Find<ModNPC>("HiveMindP2").Type) < 1)
					{
						NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("HiveMindP2").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, NPC.target);
						SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
					}
				}
            }
		}

		public override bool CanHitPlayer (Player target, ref int cooldownSlot)
		{
			return NPC.scale == 1f; //no damage when shrunk
		}

		public override bool? DrawHealthBar (byte hbPosition, ref float scale, ref Vector2 position)
		{
			return NPC.scale == 1f;
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
		}
	}
}