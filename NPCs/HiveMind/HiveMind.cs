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

namespace CalamityModClassic1Point2.NPCs.HiveMind
{
	[AutoloadBossHead]
	public class HiveMind : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("The Hive Mind");
			Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 5f;
			NPC.damage = 30;
			NPC.width = 230; //324
			NPC.height = 180; //216
			NPC.defense = 10;
			NPC.lifeMax = CalamityWorld.revenge ? 3000 : 2000;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			NPC.buffImmune[44] = true;
			NPC.buffImmune[39] = true;
			NPC.buffImmune[24] = true;
			NPC.buffImmune[20] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("TemporalSadness").Type] = true;
			NPC.boss = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicID.Boss2;
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
			Player player = Main.player[NPC.target];
			bool expertMode = Main.expertMode;
			bool revenge = CalamityWorld.revenge;
			CalamityGlobalNPC.hiveMind = NPC.whoAmI;
			if (Main.netMode != NetmodeID.MultiplayerClient) 
			{
				if (revenge)
				{
					NPC.localAI[1] += 1f;
					if (NPC.localAI[1] >= 300f)
					{
						NPC.localAI[1] = 0f;
						NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("HiveBlob").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);;
					}
				}
				if (NPC.localAI[0] == 0f) 
				{
					NPC.localAI[0] = 1f;
					for (int num723 = 0; num723 < 10; num723++) 
					{
						NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("HiveBlob").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
					}
				}
			}
			bool flag100 = false;
			int num568 = 0;
			if (expertMode)
			{
				for (int num569 = 0; num569 < 200; num569++)
				{
					if (Main.npc[num569].active && Main.npc[num569].type == (Mod.Find<ModNPC>("DankCreeper").Type))
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
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.25);
					if ((float)(NPC.life + num660) < NPC.ai[3])
					{
						NPC.ai[3] = (float)NPC.life;
						int num661 = Main.rand.Next(8, 13);
						for (int num662 = 0; num662 < num661; num662++)
						{
							int x = (int)(NPC.position.X + (float)Main.rand.Next(NPC.width - 32));
							int y = (int)(NPC.position.Y + (float)Main.rand.Next(NPC.height - 32));
							int num663 = Mod.Find<ModNPC>("HiveBlob").Type;
							if (Main.rand.NextBool(4))
							{
								num663 = Mod.Find<ModNPC>("DankCreeper").Type;
							}
							int num664 = NPC.NewNPC(NPC.GetSource_FromThis(), x, y, num663, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[num664].SetDefaults(num663);
							Main.npc[num664].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
							Main.npc[num664].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
							Main.npc[num664].ai[0] = (float)(-1000 * Main.rand.Next(3));
							Main.npc[num664].ai[1] = 0f;
							if (Main.netMode == NetmodeID.Server && num664 < 200)
							{
								NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
							}
						}
						return;
					}
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Demonite, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 200;
				NPC.height = 150;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Demonite, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Demonite, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Demonite, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
			if (NPC.CountNPCS(NPCID.EaterofSouls) < 8 && NPC.CountNPCS(NPCID.DevourerHead) < 2)
			{
				if (Main.rand.NextBool(40))
				{
					Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
					int spawn = NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, NPCID.EaterofSouls);
					if (Main.netMode == NetmodeID.Server && spawn < 200)
					{
						NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, spawn, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (Main.rand.NextBool(160))
				{
					Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
					int spawn = NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, NPCID.DevourerHead);
					if (Main.netMode == NetmodeID.Server && spawn < 200)
					{
						NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, spawn, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}
		
		public override bool CheckDead()
		{
			float targetX = NPC.Center.X;
			float targetY = NPC.Center.Y;
			int spawn = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y - 10, Mod.Find<ModNPC>("HiveMindP2").Type, 0, NPC.whoAmI, targetX, targetY);
			if (Main.netMode == NetmodeID.Server && spawn < 200)
			{
				NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, spawn, 0f, 0f, 0f, 0, 0, 0);
			}
			SoundEngine.PlaySound(SoundID.Roar, NPC.position);
			return true;
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorld.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
		}
	}
}