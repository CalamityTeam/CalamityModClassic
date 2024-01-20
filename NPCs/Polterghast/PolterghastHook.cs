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

namespace CalamityModClassic1Point2.NPCs.Polterghast
{
	public class PolterghastHook : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Polterghast Hook");
			Main.npcFrameCount[NPC.type] = 2;
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 190;
			NPC.width = 40; //324
			NPC.height = 40; //216
			NPC.defense = 50;
			NPC.lifeMax = 20000;
			NPC.dontTakeDamage = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.HitSound = SoundID.NPCHit34;
			NPC.DeathSound = SoundID.NPCDeath39;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
                new FlavorTextBestiaryInfoElement("Spooky.")

            });
        }

        public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.3f, 1f, 1f);
			bool expertMode = Main.expertMode;
			bool flag48 = false;
			bool flag49 = false;
			if (CalamityGlobalNPC1Point2.ghostBoss < 0) 
			{
				NPC.active = false;
				NPC.netUpdate = true;
				return;
			}
			if (Main.player[Main.npc[CalamityGlobalNPC1Point2.ghostBoss].target].dead) 
			{
				flag49 = true;
			}
			if (((CalamityGlobalNPC1Point2.ghostBoss != -1 && !Main.player[Main.npc[CalamityGlobalNPC1Point2.ghostBoss].target].ZoneDungeon)) | flag49) 
			{
				NPC.localAI[0] -= 6f;
				flag48 = true;
			}
			if (Main.netMode == NetmodeID.MultiplayerClient) 
			{
				if (NPC.ai[0] == 0f) 
				{
					NPC.ai[0] = (float)((int)(NPC.Center.X / 16f));
				}
				if (NPC.ai[1] == 0f) 
				{
					NPC.ai[1] = (float)((int)(NPC.Center.X / 16f));
				}
			}
			if (Main.netMode != NetmodeID.MultiplayerClient) 
			{
				if (NPC.ai[0] == 0f || NPC.ai[1] == 0f) 
				{
					NPC.localAI[0] = 0f;
				}
				NPC.localAI[0] -= 1f;
				if (Main.npc[CalamityGlobalNPC1Point2.ghostBoss].life < Main.npc[CalamityGlobalNPC1Point2.ghostBoss].lifeMax / 2) 
				{
					NPC.localAI[0] -= 2f;
				}
				if (Main.npc[CalamityGlobalNPC1Point2.ghostBoss].life < Main.npc[CalamityGlobalNPC1Point2.ghostBoss].lifeMax / 4) 
				{
					NPC.localAI[0] -= 3f;
				}
				if (Main.npc[CalamityGlobalNPC1Point2.ghostBoss].life < Main.npc[CalamityGlobalNPC1Point2.ghostBoss].lifeMax / 5) 
				{
					NPC.localAI[0] -= 3f;
				}
				if (Main.npc[CalamityGlobalNPC1Point2.ghostBoss].life < Main.npc[CalamityGlobalNPC1Point2.ghostBoss].lifeMax / 10) 
				{
					NPC.localAI[0] -= 3f;
				}
				if (flag48) 
				{
					NPC.localAI[0] -= 10f;
				}
				if (!flag49 && NPC.localAI[0] <= 0f && NPC.ai[0] != 0f) 
				{
					int num;
					for (int num763 = 0; num763 < 200; num763 = num + 1) 
					{
						if (num763 != NPC.whoAmI && Main.npc[num763].active && Main.npc[num763].type == NPC.type && (Main.npc[num763].velocity.X != 0f || Main.npc[num763].velocity.Y != 0f)) 
						{
							NPC.localAI[0] = (float)Main.rand.Next(60, 300);
						}
						num = num763;
					}
				}
				if (NPC.localAI[0] <= 0f) 
				{
					NPC.localAI[0] = (float)Main.rand.Next(300, 600);
					bool flag50 = false;
					int num764 = 0;
					while (!flag50 && num764 <= 1000) 
					{
						num764++;
						int num765 = (int)(Main.player[Main.npc[CalamityGlobalNPC1Point2.ghostBoss].target].Center.X / 16f);
						int num766 = (int)(Main.player[Main.npc[CalamityGlobalNPC1Point2.ghostBoss].target].Center.Y / 16f);
						if (NPC.ai[0] == 0f) 
						{
							num765 = (int)((Main.player[Main.npc[CalamityGlobalNPC1Point2.ghostBoss].target].Center.X + Main.npc[CalamityGlobalNPC1Point2.ghostBoss].Center.X) / 32f);
							num766 = (int)((Main.player[Main.npc[CalamityGlobalNPC1Point2.ghostBoss].target].Center.Y + Main.npc[CalamityGlobalNPC1Point2.ghostBoss].Center.Y) / 32f);
						}
						if (flag49) 
						{
							num765 = (int)Main.npc[CalamityGlobalNPC1Point2.ghostBoss].position.X / 16;
							num766 = (int)(Main.npc[CalamityGlobalNPC1Point2.ghostBoss].position.Y + 400f) / 16;
						}
						int num767 = 20;
						num767 += (int)(100f * ((float)num764 / 1000f));
						int num768 = num765 + Main.rand.Next(-num767, num767 + 1);
						int num769 = num766 + Main.rand.Next(-num767, num767 + 1);
						if (Main.npc[CalamityGlobalNPC1Point2.ghostBoss].life < Main.npc[CalamityGlobalNPC1Point2.ghostBoss].lifeMax / 2 && Main.rand.NextBool(4)) 
						{
							NPC.TargetClosest(true);
							int num770 = (int)(Main.player[NPC.target].Center.X / 16f);
							int num771 = (int)(Main.player[NPC.target].Center.Y / 16f);
							if (Main.tile[num770, num771].WallType > 0) 
							{
								num768 = num770;
								num769 = num771;
							}
						}
						try 
						{
							if (WorldGen.SolidTile(num768, num769) || (Main.tile[num768, num769].WallType > 0 && (num764 > 500 || Main.npc[CalamityGlobalNPC1Point2.ghostBoss].life < Main.npc[CalamityGlobalNPC1Point2.ghostBoss].lifeMax / 2))) 
							{
								flag50 = true;
								NPC.ai[0] = (float)num768;
								NPC.ai[1] = (float)num769;
								NPC.netUpdate = true;
							}
						} 
						catch
						{
						}
					}
				}
			}
			if (NPC.ai[0] > 0f && NPC.ai[1] > 0f) 
			{
				float num772 = 10f;
				if (Main.npc[CalamityGlobalNPC1Point2.ghostBoss].life < Main.npc[CalamityGlobalNPC1Point2.ghostBoss].lifeMax / 2) 
				{
					num772 = 12f;
				}
				if (Main.npc[CalamityGlobalNPC1Point2.ghostBoss].life < Main.npc[CalamityGlobalNPC1Point2.ghostBoss].lifeMax / 4) 
				{
					num772 = 14f;
				}
				if (Main.npc[CalamityGlobalNPC1Point2.ghostBoss].life < Main.npc[CalamityGlobalNPC1Point2.ghostBoss].lifeMax / 5) 
				{
					num772 = 18f;
				}
				if (Main.npc[CalamityGlobalNPC1Point2.ghostBoss].life < Main.npc[CalamityGlobalNPC1Point2.ghostBoss].lifeMax / 10) 
				{
					num772 = 23f;
				}
				if (expertMode) 
				{
					num772 += 1f;
				}
				if (expertMode && Main.npc[CalamityGlobalNPC1Point2.ghostBoss].life < Main.npc[CalamityGlobalNPC1Point2.ghostBoss].lifeMax / 2) 
				{
					num772 += 1f;
				}
				if (flag48) 
				{
					num772 *= 2f;
				}
				if (flag49) 
				{
					num772 *= 2f;
				}
				Vector2 vector95 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num773 = NPC.ai[0] * 16f - 8f - vector95.X;
				float num774 = NPC.ai[1] * 16f - 8f - vector95.Y;
				float num775 = (float)Math.Sqrt((double)(num773 * num773 + num774 * num774));
				if (num775 < 12f + num772) 
				{
					NPC.velocity.X = num773;
					NPC.velocity.Y = num774;
				} 
				else
				{
					num775 = num772 / num775;
					NPC.velocity.X = num773 * num775;
					NPC.velocity.Y = num774 * num775;
				}
				Vector2 vector96 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num776 = Main.npc[CalamityGlobalNPC1Point2.ghostBoss].Center.X - vector96.X;
				float num777 = Main.npc[CalamityGlobalNPC1Point2.ghostBoss].Center.Y - vector96.Y;
				NPC.rotation = (float)Math.Atan2((double)num777, (double)num776) - 1.57f;
				return;
			}
		}
		
		public override void FindFrame(int frameHeight)
		{
			if (NPC.velocity.X == 0f && NPC.velocity.Y == 0f)
			{
				if (NPC.frame.Y < 1)
				{
					NPC.frameCounter += 1.0;
					if (NPC.frameCounter > 4.0)
					{
						NPC.frameCounter = 0.0;
						NPC.frame.Y = NPC.frame.Y + frameHeight;
					}
				}
			}
			else if (NPC.frame.Y > 0)
			{
				NPC.frameCounter += 1.0;
				if (NPC.frameCounter > 4.0)
				{
					NPC.frameCounter = 0.0;
					NPC.frame.Y = NPC.frame.Y - frameHeight;
				}
			}
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (CalamityGlobalNPC1Point2.ghostBoss >= 0) 
			{
				Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y);
				float bossCenterX = Main.npc[CalamityGlobalNPC1Point2.ghostBoss].Center.X - center.X;
				float bossCenterY = Main.npc[CalamityGlobalNPC1Point2.ghostBoss].Center.Y - center.Y;
				float rotation2 = (float)Math.Atan2((double)bossCenterY, (double)bossCenterX) - 1.57f;
				bool draw = true;
				while (draw) 
				{
					int chainWidth = 20; //16 24
					int chainHeight = 52; //32 16
					float num10 = (float)Math.Sqrt((double)(bossCenterX * bossCenterX + bossCenterY * bossCenterY));
					if (num10 < (float)chainHeight) 
					{
						chainWidth = (int)num10 - chainHeight + chainWidth;
						draw = false;
					}
					num10 = (float)chainWidth / num10;
					bossCenterX *= num10;
					bossCenterY *= num10;
					center.X += bossCenterX;
					center.Y += bossCenterY;
					bossCenterX = Main.npc[CalamityGlobalNPC1Point2.ghostBoss].Center.X - center.X;
					bossCenterY = Main.npc[CalamityGlobalNPC1Point2.ghostBoss].Center.Y - center.Y;
					Microsoft.Xna.Framework.Color color2 = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16f));
					Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Polterghast/PolterghastChain").Value, new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y), 
						new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Polterghast/PolterghastChain").Value.Width, chainWidth)), color2, rotation2, 
						new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Polterghast/PolterghastChain").Value.Width * 0.5f, (float)ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Polterghast/PolterghastChain").Value.Height * 0.5f), 1f, SpriteEffects.None, 0f);
				}
			}
			return true;
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 0;
			return true;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180, true);
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}