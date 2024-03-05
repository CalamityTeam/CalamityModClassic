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
using CalamityModClassic1Point2.Items;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Placeables;
using CalamityModClassic1Point2.Items.Weapons.Calamitas;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.Calamitas
{
	[AutoloadBossHead]
	public class CalamitasRun : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cataclysm");
			Main.npcFrameCount[NPC.type] = 3;
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 78;
			NPC.npcSlots = 5f;
			NPC.width = 120; //324
			NPC.height = 120; //216
			NPC.defense = 10;
			AnimationType = 126;
			NPC.alpha = 50;
			NPC.lifeMax = CalamityWorld1Point2.revenge ? 6000 : 4000;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.boss = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.timeLeft = NPC.activeTime * 60;
			Music = MusicLoader.GetMusicSlot("CalamityModClassic1Point2/Sounds/Music/TerrariaBoss2");
			if (CalamityWorld1Point2.downedProvidence)
			{
				NPC.damage = 170;
				NPC.defense = 80;
				NPC.lifeMax = 60000;
			}
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("A clone, or replica, or something like that of the Supreme Brute, Cataclysm.")

            });
        }

        public override void AI()
		{
			bool revenge = CalamityWorld1Point2.revenge;
			bool expertMode = Main.expertMode;
			bool dayTime = Main.dayTime;
			bool provy = CalamityWorld1Point2.downedProvidence;
			Player player = Main.player[NPC.target];
			if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
			{
				NPC.TargetClosest(true);
			}
			float num840 = NPC.position.X + (float)(NPC.width / 2) - player.position.X - (float)(player.width / 2);
			float num841 = NPC.position.Y + (float)NPC.height - 59f - player.position.Y - (float)(player.height / 2);
			float num842 = (float)Math.Atan2((double)num841, (double)num840) + 1.57f;
			if (num842 < 0f)
			{
				num842 += 6.283f;
			}
			else if ((double)num842 > 6.283)
			{
				num842 -= 6.283f;
			}
			float num843 = 0.15f;
			if (NPC.rotation < num842)
			{
				if ((double)(num842 - NPC.rotation) > 3.1415)
				{
					NPC.rotation -= num843;
				}
				else
				{
					NPC.rotation += num843;
				}
			}
			else if (NPC.rotation > num842)
			{
				if ((double)(NPC.rotation - num842) > 3.1415)
				{
					NPC.rotation += num843;
				}
				else
				{
					NPC.rotation -= num843;
				}
			}
			if (NPC.rotation > num842 - num843 && NPC.rotation < num842 + num843)
			{
				NPC.rotation = num842;
			}
			if (NPC.rotation < 0f)
			{
				NPC.rotation += 6.283f;
			}
			else if ((double)NPC.rotation > 6.283)
			{
				NPC.rotation -= 6.283f;
			}
			if (NPC.rotation > num842 - num843 && NPC.rotation < num842 + num843)
			{
				NPC.rotation = num842;
			}
			if (Main.rand.NextBool(5))
			{
				int num844 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + (float)NPC.height * 0.25f), NPC.width, (int)((float)NPC.height * 0.5f), DustID.LifeDrain, NPC.velocity.X, 2f, 0, default(Color), 1f);
				Dust expr_3133D_cp_0 = Main.dust[num844];
				expr_3133D_cp_0.velocity.X = expr_3133D_cp_0.velocity.X * 0.5f;
				Dust expr_3135D_cp_0 = Main.dust[num844];
				expr_3135D_cp_0.velocity.Y = expr_3135D_cp_0.velocity.Y * 0.1f;
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
				float num861 = 4f;
				float num862 = 0.1f;
				int num863 = 1;
				if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)player.width)
				{
					num863 = -1;
				}
				Vector2 vector86 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num864 = player.position.X + (float)(player.width / 2) + (float)(num863 * 180) - vector86.X;
				float num865 = player.position.Y + (float)(player.height / 2) - vector86.Y;
				float num866 = (float)Math.Sqrt((double)(num864 * num864 + num865 * num865));
				if (expertMode)
				{
					if (num866 > 300f)
					{
						num861 += 0.5f;
					}
					if (num866 > 400f)
					{
						num861 += 0.5f;
					}
					if (num866 > 500f)
					{
						num861 += 0.55f;
					}
					if (num866 > 600f)
					{
						num861 += 0.55f;
					}
					if (num866 > 700f)
					{
						num861 += 0.6f;
					}
					if (num866 > 800f)
					{
						num861 += 0.6f;
					}
				}
				num866 = num861 / num866;
				num864 *= num866;
				num865 *= num866;
				if (NPC.velocity.X < num864)
				{
					NPC.velocity.X = NPC.velocity.X + num862;
					if (NPC.velocity.X < 0f && num864 > 0f)
					{
						NPC.velocity.X = NPC.velocity.X + num862;
					}
				}
				else if (NPC.velocity.X > num864)
				{
					NPC.velocity.X = NPC.velocity.X - num862;
					if (NPC.velocity.X > 0f && num864 < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - num862;
					}
				}
				if (NPC.velocity.Y < num865)
				{
					NPC.velocity.Y = NPC.velocity.Y + num862;
					if (NPC.velocity.Y < 0f && num865 > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + num862;
					}
				}
				else if (NPC.velocity.Y > num865)
				{
					NPC.velocity.Y = NPC.velocity.Y - num862;
					if (NPC.velocity.Y > 0f && num865 < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y - num862;
					}
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= 400f)
				{
					NPC.ai[1] = 1f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.target = 255;
					NPC.netUpdate = true;
				}
				if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
				{
					NPC.localAI[2] += 1f;
					if (NPC.localAI[2] > 22f)
					{
						NPC.localAI[2] = 0f;
						SoundEngine.PlaySound(SoundID.Item34, NPC.position);
					}
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						NPC.localAI[1] += 1f;
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
							NPC.localAI[1] += 1f;
						}
						if (NPC.localAI[1] > 8f)
						{
							NPC.localAI[1] = 0f;
							float num867 = 6f;
							int num868 = expertMode ? 23 : 26;
							int num869 = Mod.Find<ModProjectile>("BrimstoneFire").Type;
							vector86 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							num864 = player.position.X + (float)(player.width / 2) - vector86.X;
							num865 = player.position.Y + (float)(player.height / 2) - vector86.Y;
							num866 = (float)Math.Sqrt((double)(num864 * num864 + num865 * num865));
							num866 = num867 / num866;
							num864 *= num866;
							num865 *= num866;
							num865 += (float)Main.rand.Next(-40, 41) * 0.01f;
							num864 += (float)Main.rand.Next(-40, 41) * 0.01f;
							num865 += NPC.velocity.Y * 0.5f;
							num864 += NPC.velocity.X * 0.5f;
							vector86.X -= num864 * 1f;
							vector86.Y -= num865 * 1f;
							Projectile.NewProjectile(NPC.GetSource_FromThis(), vector86.X, vector86.Y, num864, num865, num869, num868 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
							return;
						}
					}
				}
			}
			else
			{
				if (NPC.ai[1] == 1f)
				{
					SoundEngine.PlaySound(SoundID.Roar, NPC.position);
					NPC.rotation = num842;
					float num870 = 12f;
					if (expertMode)
					{
						num870 += 2.5f;
					}
					if (revenge)
					{
						num870 += 1f;
					}
					Vector2 vector87 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num871 = player.position.X + (float)(player.width / 2) - vector87.X;
					float num872 = player.position.Y + (float)(player.height / 2) - vector87.Y;
					float num873 = (float)Math.Sqrt((double)(num871 * num871 + num872 * num872));
					num873 = num870 / num873;
					NPC.velocity.X = num871 * num873;
					NPC.velocity.Y = num872 * num873;
					NPC.ai[1] = 2f;
					return;
				}
				if (NPC.ai[1] == 2f)
				{
					NPC.ai[2] += 1f;
					if (expertMode)
					{
						NPC.ai[2] += 0.5f;
					}
					if (revenge)
					{
						NPC.ai[2] += 0.5f;
					}
					if (NPC.ai[2] >= 50f)
					{
						NPC.velocity.X = NPC.velocity.X * 0.93f;
						NPC.velocity.Y = NPC.velocity.Y * 0.93f;
						if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
						{
							NPC.velocity.X = 0f;
						}
						if ((double)NPC.velocity.Y > -0.1 && (double)NPC.velocity.Y < 0.1)
						{
							NPC.velocity.Y = 0f;
						}
					}
					else
					{
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
					}
					if (NPC.ai[2] >= 80f)
					{
						NPC.ai[3] += 1f;
						NPC.ai[2] = 0f;
						NPC.target = 255;
						NPC.rotation = num842;
						if (NPC.ai[3] >= 6f)
						{
							NPC.ai[1] = 0f;
							NPC.ai[3] = 0f;
							return;
						}
						NPC.ai[1] = 1f;
						return;
					}
				}
			}
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<CataclysmTrophy>(), 10));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BrimstoneFlamesprayer>(), 4, 10));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BrimstoneFlameblaster>(), 4, 10));
        }
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "A Cataclysm Shade";
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
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorld1Point2.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
			if (Main.expertMode)
			{
				target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 100, true);
			}
			else
			{
				target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 80, true);
			}
		}
	}
}