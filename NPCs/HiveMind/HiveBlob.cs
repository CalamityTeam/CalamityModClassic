using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.HiveMind
{
	public class HiveBlob : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Hive Blob");
			Main.npcFrameCount[NPC.type] = 2;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 0.1f;
			NPC.aiStyle = -1;
			NPC.damage = 20;
			NPC.width = 25; //324
			NPC.height = 25; //216
			NPC.lifeMax = 100;
			AIType = -1;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
                new FlavorTextBestiaryInfoElement("Globs of corruption are able to suspend themselves in the air to protect their hive.")

            });
        }

        public override void AI()
		{
			bool expertMode = Main.expertMode;
			bool revenge = CalamityWorld.revenge;
			if (CalamityGlobalNPC.hiveMind < 0) 
			{
				NPC.SimpleStrikeNPC(9999, 0, false, noPlayerInteraction: true);
				NPC.netUpdate = true;
				return;
			}
			int num750 = CalamityGlobalNPC.hiveMind;
			if (NPC.ai[3] > 0f) 
			{
				num750 = (int)NPC.ai[3] - 1;
			}
			if (Main.netMode != NetmodeID.MultiplayerClient) 
			{
				NPC.localAI[0] -= 1f;
				if (NPC.localAI[0] <= 0f) 
				{
					NPC.localAI[0] = (float)Main.rand.Next(120, 480);
					NPC.ai[0] = (float)Main.rand.Next(-100, 101);
					NPC.ai[1] = (float)Main.rand.Next(-100, 101);
					NPC.netUpdate = true;
				}
			}
			NPC.TargetClosest(true);
			float num751 = 0.02f;
			float num752 = 400f;
			if ((double)Main.npc[CalamityGlobalNPC.hiveMind].life < (double)Main.npc[CalamityGlobalNPC.hiveMind].lifeMax * 0.5) 
			{
				num752 += 60f;
			}
			if ((double)Main.npc[CalamityGlobalNPC.hiveMind].life < (double)Main.npc[CalamityGlobalNPC.hiveMind].lifeMax * 0.15) 
			{
				num752 += 120f;
			}
			if (expertMode) 
			{
				float num753 = 1f - (float)NPC.life / (float)NPC.lifeMax;
				num752 += num753 * 100f;
				num751 += 0.03f;
			}
			if (revenge)
			{
				num751 += 0.1f;
			}
			if (!Main.npc[num750].active || CalamityGlobalNPC.hiveMind < 0) 
			{
				NPC.active = false;
				return;
			}
			Vector2 vector22 = new Vector2(NPC.ai[0] * 16f + 8f, NPC.ai[1] * 16f + 8f);
			float num189 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - (float)(NPC.width / 2) - vector22.X;
			float num190 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - (float)(NPC.height / 2) - vector22.Y;
			float num191 = (float)Math.Sqrt((double)(num189 * num189 + num190 * num190));
			float num754 = Main.npc[num750].position.X + (float)(Main.npc[num750].width / 2);
			float num755 = Main.npc[num750].position.Y + (float)(Main.npc[num750].height / 2);
			Vector2 vector93 = new Vector2(num754, num755);
			float num756 = num754 + NPC.ai[0];
			float num757 = num755 + NPC.ai[1];
			float num758 = num756 - vector93.X;
			float num759 = num757 - vector93.Y;
			float num760 = (float)Math.Sqrt((double)(num758 * num758 + num759 * num759));
			num760 = num752 / num760;
			num758 *= num760;
			num759 *= num760;
			if (NPC.position.X < num754 + num758) 
			{
				NPC.velocity.X = NPC.velocity.X + num751;
				if (NPC.velocity.X < 0f && num758 > 0f) 
				{
					NPC.velocity.X = NPC.velocity.X * 0.9f;
				}
			} 
			else if (NPC.position.X > num754 + num758) 
			{
				NPC.velocity.X = NPC.velocity.X - num751;
				if (NPC.velocity.X > 0f && num758 < 0f) 
				{
					NPC.velocity.X = NPC.velocity.X * 0.9f;
				}
			}
			if (NPC.position.Y < num755 + num759) 
			{
				NPC.velocity.Y = NPC.velocity.Y + num751;
				if (NPC.velocity.Y < 0f && num759 > 0f) 
				{
					NPC.velocity.Y = NPC.velocity.Y * 0.9f;
				}
			} 
			else if (NPC.position.Y > num755 + num759) 
			{
				NPC.velocity.Y = NPC.velocity.Y - num751;
				if (NPC.velocity.Y > 0f && num759 < 0f) 
				{
					NPC.velocity.Y = NPC.velocity.Y * 0.9f;
				}
			}
			if (NPC.velocity.X > 8f) 
			{
				NPC.velocity.X = 8f;
			}
			if (NPC.velocity.X < -8f) 
			{
				NPC.velocity.X = -8f;
			}
			if (NPC.velocity.Y > 8f) 
			{
				NPC.velocity.Y = 8f;
			}
			if (NPC.velocity.Y < -8f) 
			{
				NPC.velocity.Y = -8f;
			}
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (!Main.player[NPC.target].dead)
				{
					NPC.localAI[1] += (float)Main.rand.Next(1, 6);
					if (NPC.localAI[1] >= 900f)
					{
						if (!Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
						{
							float num196 = 9f;
							vector22 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							num189 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector22.X + (float)Main.rand.Next(-10, 11);
							float num197 = Math.Abs(num189 * 0.1f);
							if (num190 > 0f)
							{
								num197 = 0f;
							}
							num190 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector22.Y + (float)Main.rand.Next(-10, 11) - num197;
							num191 = (float)Math.Sqrt((double)(num189 * num189 + num190 * num190));
							num191 = num196 / num191;
							num189 *= num191;
							num190 *= num191;
							int num9 = Mod.Find<ModProjectile>("VileClot").Type;
							int damage = expertMode ? 10 : 12;
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, num189, num190, num9, damage, 0f, Main.myPlayer, 0f, 0f);
							NPC.localAI[1] = 0f;
							return;
						}
						NPC.localAI[1] = 250f;
						return;
					}
				}
			}
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.05f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Demonite, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Demonite, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}