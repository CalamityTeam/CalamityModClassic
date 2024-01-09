using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.GameContent.Generation;
using CalamityModClassic1Point2.Tiles;
using CalamityModClassic1Point2.NPCs.Calamitas;
using CalamityModClassic1Point2;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Calamitas;
using CalamityModClassic1Point2.Items.Placeables;
using CalamityModClassic1Point2.Items.Weapons.Calamitas;
using CalamityModClassic1Point2.Items;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.Calamitas
{
	[AutoloadBossHead]
	public class CalamitasRun3 : ModNPC
	{
		public float bossLife;
		public int halfLife = 0;
		public bool secondStage = false;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Calamitas");
			Main.npcFrameCount[NPC.type] = 3;
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 50;
			NPC.npcSlots = 7f;
			NPC.width = 120;
			NPC.height = 120;
			NPC.defense = 25;
			AnimationType = 125;
			NPC.lifeMax = CalamityWorld.revenge ? 21500 : 19000;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 15, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			Music = MusicLoader.GetMusicSlot(Mod, "CalamityModClassic1Point2/Sounds/Music/TerrariaBoss2");
			//bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("CalamitasBag").Type;
			if (CalamityWorld.downedProvidence)
			{
				NPC.damage = 160;
				NPC.defense = 150;
				NPC.lifeMax = 200000;
				NPC.value = Item.buyPrice(1, 15, 0, 0);
			}
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("Calamitas' doppel.")

            });
        }

        public override void AI()
		{
			bool revenge = CalamityWorld.revenge;
			bool expertMode = Main.expertMode;
			bool dayTime = Main.dayTime;
			bool provy = CalamityWorld.downedProvidence;
			Player player = Main.player[NPC.target];
			if (halfLife == 0 && (NPC.life <= NPC.lifeMax * 0.5f))
			{
				if (secondStage == false && Main.netMode != NetmodeID.MultiplayerClient) 
				{
					SoundEngine.PlaySound(SoundID.Item74, NPC.position);
					for (int I = 0; I < 5; I++) 
					{
						int FireEye = NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.Center.X + (Math.Sin(I * 72) * 150)), (int)(NPC.Center.Y + (Math.Cos(I * 72) * 150)), Mod.Find<ModNPC>("SoulSeeker").Type, NPC.whoAmI, 0, 0, 0, -1);
						NPC Eye = Main.npc[FireEye];
						Eye.ai[0] = I * 72;
						Eye.ai[3] = I * 72;
					}
					secondStage = true;
				}
				string key = "Impressive child, most impressive...";
				Color messageColor = Color.Orange;
				if (Main.netMode == NetmodeID.SinglePlayer)
				{
					Main.NewText((key), messageColor);
				}
				else if (Main.netMode == NetmodeID.Server)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
				}
				halfLife++;
			}
			if (bossLife == 0f && NPC.life > 0)
			{
				bossLife = (float)NPC.lifeMax;
			}
	       	if (NPC.life > 0)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.45);
					if ((float)(NPC.life + num660) < bossLife)
					{
						bossLife = (float)NPC.life;
						if (!expertMode)
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
								if (Main.netMode == NetmodeID.Server && num664 < 200)
								{
									NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
								}
							}
						}
						else if (expertMode)
						{
							int respawn = 1;
							for (int num662 = 0; num662 < respawn; num662++)
							{
								Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("RedSpawn").Type, 0, 0f, Main.myPlayer, 0f, 0f);
								Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("GraySpawn").Type, 0, 0f, Main.myPlayer, 0f, 0f);
								string key = "The brothers have been reborn!";
								Color messageColor = Color.Orange;
								if (Main.netMode == NetmodeID.SinglePlayer)
								{
									Main.NewText((key), messageColor);
								}
								else if (Main.netMode == NetmodeID.Server)
								{
									ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
								}
							}
						}
						return;
					}
				}
	       	}
			bool flag100 = false;
			int num568 = 0;
			if (expertMode)
			{
				for (int num569 = 0; num569 < 200; num569++)
				{
					if ((Main.npc[num569].active && Main.npc[num569].type == (Mod.Find<ModNPC>("CalamitasRun").Type)) || (Main.npc[num569].active && Main.npc[num569].type == Mod.Find<ModNPC>("CalamitasRun2").Type))
					{
						flag100 = true;
						num568++;
					}
				}
				NPC.defense += num568 * 50;
			}
			if (expertMode)
			{
				if (!flag100)
				{
					NPC.defense = 30;
				}
			}
			if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
			{
				NPC.TargetClosest(true);
			}
			float num801 = NPC.position.X + (float)(NPC.width / 2) - player.position.X - (float)(player.width / 2);
			float num802 = NPC.position.Y + (float)NPC.height - 59f - player.position.Y - (float)(player.height / 2);
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
			if (Main.rand.NextBool(5))
			{
				int num805 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + (float)NPC.height * 0.25f), NPC.width, (int)((float)NPC.height * 0.5f), DustID.LifeDrain, NPC.velocity.X, 2f, 0, default(Color), 1f);
				Dust expr_2F45E_cp_0 = Main.dust[num805];
				expr_2F45E_cp_0.velocity.X = expr_2F45E_cp_0.velocity.X * 0.5f;
				Dust expr_2F47E_cp_0 = Main.dust[num805];
				expr_2F47E_cp_0.velocity.Y = expr_2F47E_cp_0.velocity.Y * 0.1f;
			}
			if (!player.active || player.dead || (dayTime && !Main.eclipse))
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead || (dayTime && !Main.eclipse))
				{
					NPC.velocity = new Vector2(0f, -10f);
					if (NPC.timeLeft > 150)
					{
						NPC.timeLeft = 150;
					}
					return;
				}
			}
			else if (NPC.timeLeft > 1800)
			{
				NPC.timeLeft = 1800;
			}
			if (NPC.ai[1] == 0f)
			{
				float num823 = expertMode ? 12f : 10.5f;
				float num824 = expertMode ? 0.25f : 0.225f;
				Vector2 vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num825 = player.position.X + (float)(player.width / 2) - vector82.X;
				float num826 = player.position.Y + (float)(player.height / 2) - 300f - vector82.Y;
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
				num825 = player.position.X + (float)(player.width / 2) - vector82.X;
				num826 = player.position.Y + (float)(player.height / 2) - vector82.Y;
				NPC.rotation = (float)Math.Atan2((double)num826, (double)num825) - 1.57f;
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.localAI[1] += 1f;
					if (Main.player[(int)Player.FindClosest(NPC.position, NPC.width, NPC.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
					{
						NPC.localAI[1] += 1f;
					}
					if (revenge)
					{
						NPC.localAI[1] += 0.5f;
					}
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
					if (NPC.localAI[1] > 180f && Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
					{
						NPC.localAI[1] = 0f;
						float num828 = expertMode ? 14f : 8.5f;
						int num829 = expertMode ? 37 : 40;
						int num830 = Mod.Find<ModProjectile>("BrimstoneHellfireball").Type;
						num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
						num827 = num828 / num827;
						num825 *= num827;
						num826 *= num827;
						vector82.X += num825 * 15f;
						vector82.Y += num826 * 15f;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), vector82.X, vector82.Y, num825, num826, num830, num829 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
						return;
					}
				}
			}
			else
			{
				int num831 = 1;
				if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)player.width)
				{
					num831 = -1;
				}
				float num832 = expertMode ? 13f : 11f;
				float num833 = expertMode ? 0.35f : 0.3f;
				Vector2 vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num834 = player.position.X + (float)(player.width / 2) + (float)(num831 * 340) - vector83.X;
				float num835 = player.position.Y + (float)(player.height / 2) - vector83.Y;
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
				num834 = player.position.X + (float)(player.width / 2) - vector83.X;
				num835 = player.position.Y + (float)(player.height / 2) - vector83.Y;
				NPC.rotation = (float)Math.Atan2((double)num835, (double)num834) - 1.57f;
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.localAI[1] += 1f;
					if (Main.player[(int)Player.FindClosest(NPC.position, NPC.width, NPC.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
					{
						NPC.localAI[1] += 1f;
					}
					if (revenge)
					{
						NPC.localAI[1] += 0.5f;
					}
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
					if (expertMode)
					{
						NPC.localAI[1] += 1.5f;
					}
					if (NPC.localAI[1] > 60f && Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
					{
						NPC.localAI[1] = 0f;
						float num837 = 9f;
						int num838 = expertMode ? 23 : 28;
						int num839 = Mod.Find<ModProjectile>("BrimstoneLaser").Type;
						num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
						num836 = num837 / num836;
						num834 *= num836;
						num835 *= num836;
						vector83.X += num834 * 15f;
						vector83.Y += num835 * 15f;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), vector83.X, vector83.Y, num834, num835, num839, num838 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
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
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            //npcLoot.Add(new CommonDrop(ModContent.ItemType<TestItemKYS>(), 1));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<CalamitasTrophy>(), 10));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<CalamitasBag>(), 1));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CalamitasMask>(), 7));
            npcLoot.Add(ItemDropRule.ByCondition(new ProvidenceDowned(), ModContent.ItemType<Bloodstone>(), 1, 30, 40));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CalamityDust>(), 1, 9, 14));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BlightedLens>(), 1, 1, 2));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<EssenceofChaos>(), 1, 3, 5));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CalamitasInferno>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<TheEyeofCalamitas>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BlightedEyeStaff>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ChaosStone>(), 10));
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
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.LifeDrain, hit.HitDirection, -1f, 0, default(Color), 1f);
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
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.damage = (int)(NPC.damage * 0.8f);
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorld.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
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