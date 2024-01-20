using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;
using Terraria.GameContent.Generation;
using CalamityModClassic1Point1.Tiles;
using CalamityModClassic1Point1.NPCs.Calamitas;
using CalamityModClassic1Point1;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point1.Items;
using CalamityModClassic1Point1.Items.Calamitas;
using CalamityModClassic1Point1.Items.Placeables;
using CalamityModClassic1Point1.Items.Armor;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.UI;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.Calamitas
{
	[AutoloadBossHead]
	public class CalamitasRun3 : ModNPC
	{
		public float bossLife;
		public int halfLife = 0;
		
		public override void SetDefaults()
		{
			//NPC.name = "Calamitas");
			//Tooltip.SetDefault("Calamitas");
			NPC.damage = 65;
			NPC.npcSlots = 5f;
			NPC.width = 100;
			NPC.height = 110;
			NPC.defense = 30;
			AnimationType = 125;
			NPC.lifeMax = 20000;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(1, 5, 0, 0);
			Main.npcFrameCount[NPC.type] = 3;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.timeLeft = NPC.activeTime * 30;
			Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TerrariaBoss2");
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheTemple,
                new FlavorTextBestiaryInfoElement("The Forgotten Shade of Calamitas.")

            });
        }

        public override void AI()
        {
            if (halfLife == 0 && (NPC.life <= NPC.lifeMax * 0.5f))
			{
				Main.NewText("Impressive child, most impressive...", Color.Orange.R, Color.Orange.G, Color.Orange.B);
				halfLife++;
			}
			if (bossLife == 0f && NPC.life > 0)
			{
				bossLife = (float)NPC.lifeMax;
			}
	       	if (NPC.life > 0)
			{
				if (Main.netMode != 1)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.45);
					if ((float)(NPC.life + num660) < bossLife)
					{
						bossLife = (float)NPC.life;
						if (!Main.expertMode)
						{
							int num661 = Main.rand.Next(8, 13);
							for (int num662 = 0; num662 < num661; num662++)
							{
								int x = (int)(NPC.position.X + (float)Main.rand.Next(NPC.width - 32));
								int y = (int)(NPC.position.Y + (float)Main.rand.Next(NPC.height - 32));
								int num663 = Mod.Find<ModNPC>("LifeSeeker").Type;
								int num664 = NPC.NewNPC(NPC.GetSource_FromThis(), x, y, num663, 0, 0f, 0f, 0f, 0f, 255);
								Main.npc[num664].SetDefaults(num663);
								Main.npc[num664].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
								Main.npc[num664].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
								Main.npc[num664].ai[0] = (float)(-1000 * Main.rand.Next(3));
								Main.npc[num664].ai[1] = 0f;
								if (Main.netMode == 2 && num664 < 200)
								{
									NetMessage.SendData(23, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
								}
							}
						}
						else if (Main.expertMode)
						{
							int respawn = 1;
							for (int num662 = 0; num662 < respawn; num662++)
							{
								Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("RedSpawn").Type, 0, 0f, Main.myPlayer, 0f, 0f);
								Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("GraySpawn").Type, 0, 0f, Main.myPlayer, 0f, 0f);
								Main.NewText("The brothers have been reborn!", Color.Orange.R, Color.Orange.G, Color.Orange.B);
							}
						}
						return;
					}
				}
	       	}
			bool flag100 = false;
			int num568 = 0;
			if (Main.expertMode)
			{
				for (int num569 = 0; num569 < 200; num569++)
				{
					if ((Main.npc[num569].active && Main.npc[num569].type == (Mod.Find<ModNPC>("CalamitasRun").Type)) || (Main.npc[num569].active && Main.npc[num569].type == Mod.Find<ModNPC>("CalamitasRun2").Type))
					{
						flag100 = true;
						num568++;
					}
				}
				NPC.defense += num568 * 25;
			}
			if (Main.expertMode)
			{
				if (!flag100)
				{
					NPC.defense = 30;
				}
			}
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest(true);
			}
			bool dead2 = Main.player[NPC.target].dead;
			float num801 = NPC.position.X + (float)(NPC.width / 2) - Main.player[NPC.target].position.X - (float)(Main.player[NPC.target].width / 2);
			float num802 = NPC.position.Y + (float)NPC.height - 59f - Main.player[NPC.target].position.Y - (float)(Main.player[NPC.target].height / 2);
			float num803 = (float)Math.Atan2((double)num802, (double)num801) + 1.57f;
			if (num803 < 0f)
			{
				num803 += 6.283f;
			}
			else if ((double)num803 > 6.283)
			{
				num803 -= 6.283f;
			}
			float num804 = 0.1f;
			if (NPC.rotation < num803)
			{
				if ((double)(num803 - NPC.rotation) > 3.1415)
				{
					NPC.rotation -= num804;
				}
				else
				{
					NPC.rotation += num804;
				}
			}
			else if (NPC.rotation > num803)
			{
				if ((double)(NPC.rotation - num803) > 3.1415)
				{
					NPC.rotation += num804;
				}
				else
				{
					NPC.rotation -= num804;
				}
			}
			if (NPC.rotation > num803 - num804 && NPC.rotation < num803 + num804)
			{
				NPC.rotation = num803;
			}
			if (NPC.rotation < 0f)
			{
				NPC.rotation += 6.283f;
			}
			else if ((double)NPC.rotation > 6.283)
			{
				NPC.rotation -= 6.283f;
			}
			if (NPC.rotation > num803 - num804 && NPC.rotation < num803 + num804)
			{
				NPC.rotation = num803;
			}
			if (Main.rand.Next(5) == 0)
			{
				int num805 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + (float)NPC.height * 0.25f), NPC.width, (int)((float)NPC.height * 0.5f), 235, NPC.velocity.X, 2f, 0, default(Color), 1f);
				Dust expr_2F45E_cp_0 = Main.dust[num805];
				expr_2F45E_cp_0.velocity.X = expr_2F45E_cp_0.velocity.X * 0.5f;
				Dust expr_2F47E_cp_0 = Main.dust[num805];
				expr_2F47E_cp_0.velocity.Y = expr_2F47E_cp_0.velocity.Y * 0.1f;
			}
			if (Main.netMode != 1 && !dead2 && NPC.timeLeft < 10)
			{
				for (int num806 = 0; num806 < 200; num806++)
				{
					if (num806 != NPC.whoAmI && Main.npc[num806].active && Main.npc[num806].timeLeft - 1 > NPC.timeLeft)
					{
						NPC.timeLeft = Main.npc[num806].timeLeft - 1;
					}
				}
			}
			if (dead2)
			{
				NPC.TargetClosest(false);
				NPC.velocity.Y = NPC.velocity.Y + 2f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 2f;
				}
				if ((double)NPC.position.Y > Main.rockLayer * 16.0)
				{
					for (int num957 = 0; num957 < 200; num957++)
					{
						if (Main.npc[num957].aiStyle == NPC.aiStyle)
						{
							Main.npc[num957].active = false;
						}
					}
				}
			}
			else
			{
				if (NPC.ai[1] == 0f)
				{
					float num823 = 10.5f;
					float num824 = 0.225f;
					if (Main.expertMode)
					{
						num823 = 12f;
						num824 = 0.25f;
					}
					if (Main.dayTime)
					{
						num823 = 14f;
						num824 = 0.3f;
					}
					Vector2 vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num825 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector82.X;
					float num826 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - 300f - vector82.Y;
					float num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
					num827 = num823 / num827;
					num825 *= num827;
					num826 *= num827;
					if (NPC.velocity.X < num825)
					{
						NPC.velocity.X = NPC.velocity.X + num824;
						if (NPC.velocity.X < 0f && num825 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num824;
						}
					}
					else if (NPC.velocity.X > num825)
					{
						NPC.velocity.X = NPC.velocity.X - num824;
						if (NPC.velocity.X > 0f && num825 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num824;
						}
					}
					if (NPC.velocity.Y < num826)
					{
						NPC.velocity.Y = NPC.velocity.Y + num824;
						if (NPC.velocity.Y < 0f && num826 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num824;
						}
					}
					else if (NPC.velocity.Y > num826)
					{
						NPC.velocity.Y = NPC.velocity.Y - num824;
						if (NPC.velocity.Y > 0f && num826 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num824;
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 300f)
					{
						NPC.ai[1] = 1f;
						NPC.ai[2] = 0f;
						NPC.ai[3] = 0f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
					}
					vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					num825 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector82.X;
					num826 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector82.Y;
					NPC.rotation = (float)Math.Atan2((double)num826, (double)num825) - 1.57f;
					if (Main.netMode != 1)
					{
						NPC.localAI[1] += 1f;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
						{
							NPC.localAI[1] += 1f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
						{
							NPC.localAI[1] += 1f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
						{
							NPC.localAI[1] += 1f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
						{
							NPC.localAI[1] += 2f;
						}
						if (CalamityGlobalNPC1Point1.bossBuff && CalamityGlobalNPC1Point1.superBossBuff)
						{
							NPC.localAI[1] += 4f;
						}
						if (NPC.localAI[1] > 180f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							NPC.localAI[1] = 0f;
							float num828 = 8.5f;
							int num829 = 70;
							int num830 = Mod.Find<ModProjectile>("BrimstoneHellfireball").Type;
							if (Main.expertMode)
							{
								num828 = 14f;
								num829 = 40;
							}
							if (Main.dayTime)
							{
								num828 = 15f;
								num829 = 80;
							}
							if (NPC.downedMoonlord)
							{
								num828 = 17f;
								num829 = 80;
								if (Main.expertMode)
								{
									num828 = 18f;
									num829 = 50;
								}
							}
							num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
							num827 = num828 / num827;
							num825 *= num827;
							num826 *= num827;
							vector82.X += num825 * 15f;
							vector82.Y += num826 * 15f;
							Projectile.NewProjectile(NPC.GetSource_FromThis(), vector82.X, vector82.Y, num825, num826, num830, num829, 0f, Main.myPlayer, 0f, 0f);
							return;
						}
					}
				}
				else
				{
					int num831 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width)
					{
						num831 = -1;
					}
					float num832 = 11f;
					float num833 = 0.3f;
					if (Main.expertMode)
					{
						num832 = 13f;
						num833 = 0.35f;
					}
					if (Main.dayTime)
					{
						num832 = 15f;
						num833 = 0.45f;
					}
					Vector2 vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num834 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + (float)(num831 * 340) - vector83.X;
					float num835 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector83.Y;
					float num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
					num836 = num832 / num836;
					num834 *= num836;
					num835 *= num836;
					if (NPC.velocity.X < num834)
					{
						NPC.velocity.X = NPC.velocity.X + num833;
						if (NPC.velocity.X < 0f && num834 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num833;
						}
					}
					else if (NPC.velocity.X > num834)
					{
						NPC.velocity.X = NPC.velocity.X - num833;
						if (NPC.velocity.X > 0f && num834 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num833;
						}
					}
					if (NPC.velocity.Y < num835)
					{
						NPC.velocity.Y = NPC.velocity.Y + num833;
						if (NPC.velocity.Y < 0f && num835 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num833;
						}
					}
					else if (NPC.velocity.Y > num835)
					{
						NPC.velocity.Y = NPC.velocity.Y - num833;
						if (NPC.velocity.Y > 0f && num835 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num833;
						}
					}
					vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					num834 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector83.X;
					num835 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector83.Y;
					NPC.rotation = (float)Math.Atan2((double)num835, (double)num834) - 1.57f;
					if (Main.netMode != 1)
					{
						NPC.localAI[1] += 1f;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
						{
							NPC.localAI[1] += 0.5f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
						{
							NPC.localAI[1] += 0.75f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
						{
							NPC.localAI[1] += 1f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
						{
							NPC.localAI[1] += 1.5f;
						}
						if (Main.expertMode)
						{
							NPC.localAI[1] += 1.5f;
						}
						if (CalamityGlobalNPC1Point1.bossBuff && CalamityGlobalNPC1Point1.superBossBuff)
						{
							NPC.localAI[1] += 3f;
						}
						if (NPC.localAI[1] > 60f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							NPC.localAI[1] = 0f;
							float num837 = 9f;
							int num838 = 31;
							int num839 = Mod.Find<ModProjectile>("BrimstoneLaser").Type;
							if (Main.expertMode)
							{
								num838 = 17;
							}
							if (Main.dayTime)
							{
								num838 = 33;
							}
							if (NPC.downedMoonlord)
							{
								num838 = 35;
								if (Main.expertMode)
								{
									num838 = 21;
								}
							}
							num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
							num836 = num837 / num836;
							num834 *= num836;
							num835 *= num836;
							vector83.X += num834 * 15f;
							vector83.Y += num835 * 15f;
							Projectile.NewProjectile(NPC.GetSource_FromThis(), vector83.X, vector83.Y, num834, num835, num839, num838, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 180f)
					{
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.ai[3] = 0f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
						return;
					}
				}
			}
		}
		
		public override bool CheckActive()
		{
			return false;
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			npcLoot.Add(new CommonDrop(ModContent.ItemType<TestItemKys>(), 1));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<CalamitasTrophy>(), 10));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<CalamitasBag>(), 1));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CalamitasMask>(), 7));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CalamityDust>(), 1, 9, 14));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BlightedLens>(), 1));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<EssenceofChaos>(), 1, 3, 5));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CalamitasInferno>(), 7));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<TheEyeofCalamitas>(), 15));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BlightedEyeStaff>(), 16));
        }
		
		public override bool CheckDead()
		{
			if (!CalamityWorld1Point1.stopChaotic)
			{
				Main.NewText("A crawling chaos has sprouted in the underworld ash...", Color.Yellow.R, Color.Fuchsia.G, Color.Yellow.B);
			}
			return true;
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "A Calamitas Doppelganger";
			potionType = ItemID.GreaterHealingPotion;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 100;
				NPC.height = 100;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.damage = (int)(NPC.damage * 0.65f);
			NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * balance);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300, true);
			}
			else
			{
				target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 350, true);
			}
		}
	}
}