using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Perforator;
using CalamityModClassicPreTrailer.Items.Placeables;
using CalamityModClassicPreTrailer.Items.Weapons.Perforators;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.Perforator
{
	[AutoloadBossHead]
	public class PerforatorHive : ModNPC
	{
		private bool small = false;
		private bool medium = false;
		private bool large = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Perforator Hive");
			Main.npcFrameCount[NPC.type] = 10;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
				new FlavorTextBestiaryInfoElement("Birthed in the crimson, though unknown if it's a servant of it, as its worms tear through the crimson flesh regularly.")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 18f;
			NPC.damage = 35;
			NPC.width = 110; //324
			NPC.height = 100; //216
			NPC.defense = 0;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 5400 : 3750;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 7600;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 3000000 : 2700000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.aiStyle = -1; //new
			AIType = -1; //new
			NPC.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("TemporalSadness").Type] = true;
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 6, 0, 0);
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit13;
			NPC.DeathSound = SoundID.NPCDeath19;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/BloodCoagulant");
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
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 1.5f, 0f, 0f);
			Player player = Main.player[NPC.target];
			bool isCrimson = player.ZoneCrimson || CalamityWorldPreTrailer.bossRushActive;
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			if (Vector2.Distance(player.Center, NPC.Center) > 5600f)
			{
				if (NPC.timeLeft > 10)
				{
					NPC.timeLeft = 10;
				}
			}
			if (!player.active || player.dead)
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead)
				{
					NPC.velocity = new Vector2(0f, 10f);
					if (NPC.timeLeft > 150)
					{
						NPC.timeLeft = 150;
					}
					return;
				}
			}
			else if (NPC.timeLeft < 1800)
			{
				NPC.timeLeft = 1800;
			}
			NPC.TargetClosest(true);
			bool wormAlive = false;
			if (NPC.AnyNPCs(Mod.Find<ModNPC>("PerforatorHeadLarge").Type))
			{
				wormAlive = true;
			}
			if (wormAlive)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = isCrimson ? false : true;
			}
			if (Main.netMode != 1)
			{
				int shoot = revenge ? 6 : 4;
				if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
				{
					shoot += 3;
				}
				NPC.localAI[0] += (float)Main.rand.Next(shoot);
				if (NPC.localAI[0] >= (float)Main.rand.Next(300, 900))
				{
					NPC.localAI[0] = 0f;
					SoundEngine.PlaySound(SoundID.NPCHit20, NPC.position);
					for (int num621 = 0; num621 < 8; num621++)
					{
						int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 170, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num622].velocity *= 3f;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num622].scale = 0.5f;
							Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}
					for (int num623 = 0; num623 < 16; num623++)
					{
						int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 5, 0f, 0f, 100, default(Color), 3f);
						Main.dust[num624].noGravity = true;
						Main.dust[num624].velocity *= 5f;
						num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num624].velocity *= 2f;
					}
					NPC.TargetClosest(true);
					float num179 = 8f;
					Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
					float num181 = Math.Abs(num180) * 0.1f;
					float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
					float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
					NPC.netUpdate = true;
					num183 = num179 / num183;
					num180 *= num183;
					num182 *= num183;
					int num184 = expertMode ? 14 : 18;
					int num185 = (Main.rand.Next(2) == 0 ? Mod.Find<ModProjectile>("IchorShot").Type : Mod.Find<ModProjectile>("BloodGeyser").Type);
					value9.X += num180;
					value9.Y += num182;
					for (int num186 = 0; num186 < 20; num186++)
					{
						num180 = player.position.X + (float)player.width * 0.5f - value9.X;
						num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
						num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
						num183 = num179 / num183;
						num180 += (float)Main.rand.Next(-180, 181);
						num180 *= num183;
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, num180, -5f, num185, num184, 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			NPC.rotation = NPC.velocity.X * 0.04f;
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			if (NPC.position.Y > player.position.Y - 160f) //200
			{
				if (NPC.velocity.Y > 0f)
				{
					NPC.velocity.Y = NPC.velocity.Y * 0.98f;
				}
				NPC.velocity.Y = NPC.velocity.Y - 0.1f;
				if (NPC.velocity.Y > 2f)
				{
					NPC.velocity.Y = 2f;
				}
			}
			else if (NPC.position.Y < player.position.Y - 400f) //500
			{
				if (NPC.velocity.Y < 0f)
				{
					NPC.velocity.Y = NPC.velocity.Y * 0.98f;
				}
				NPC.velocity.Y = NPC.velocity.Y + 0.1f;
				if (NPC.velocity.Y < -2f)
				{
					NPC.velocity.Y = -2f;
				}
			}
			if (NPC.position.X + (float)(NPC.width / 2) > player.position.X + (float)(player.width / 2) + 80f)
			{
				if (NPC.velocity.X > 0f)
				{
					NPC.velocity.X = NPC.velocity.X * 0.98f;
				}
				NPC.velocity.X = NPC.velocity.X - 0.1f;
				if (NPC.velocity.X > 8f)
				{
					NPC.velocity.X = 8f;
				}
			}
			if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)(player.width / 2) - 80f)
			{
				if (NPC.velocity.X < 0f)
				{
					NPC.velocity.X = NPC.velocity.X * 0.98f;
				}
				NPC.velocity.X = NPC.velocity.X + 0.1f;
				if (NPC.velocity.X < -8f)
				{
					NPC.velocity.X = -8f;
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
					int num660 = (int)((double)NPC.lifeMax * 0.3);
					if ((float)(NPC.life + num660) < NPC.ai[3])
					{
						NPC.ai[3] = (float)NPC.life;
						int wormType = Mod.Find<ModNPC>("PerforatorHeadSmall").Type;
						if (!small)
						{
							small = true;
						}
						else if (!medium)
						{
							medium = true;
							wormType = Mod.Find<ModNPC>("PerforatorHeadMedium").Type;
						}
						else if (!large)
						{
							large = true;
							wormType = Mod.Find<ModNPC>("PerforatorHeadLarge").Type;
						}
						NPC.SpawnOnPlayer(NPC.FindClosestPlayer(), wormType);
						return;
					}
				}
			}
		}

		public override bool CheckDead()
		{
			if (NPC.AnyNPCs(Mod.Find<ModNPC>("PerforatorHeadLarge").Type))
			{
				return false;
			}
			return true;
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.HealingPotion;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
			npcLoot.Add(new CommonDrop(ModContent.ItemType<PerforatorTrophy>(), 10));
			npcLoot.Add(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<PerforatorBag>(),
				1,
				5, 5));
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<PerforatorBag>()));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<PerforatorMask>(), 7));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<Aorta>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<SausageMaker>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<BloodyRupture>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<BloodBath>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<VeinBurster>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<Eviscerator>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<ToothBall>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<BloodClotStaff>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<BloodSample>(), 1, 7, 15));
			notExpert.OnSuccess(new CommonDrop(ItemID.Vertebrae, 1, 3, 10));
			notExpert.OnSuccess(new CommonDrop(ItemID.CrimtaneBar, 1, 2, 6));
			notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.Ichor, 1, 10, 21));
			npcLoot.Add(notExpert);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < hit.Damage / NPC.lifeMax * 100.0; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Hive").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Hive2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Hive3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Hive4").Type, 1f);
				}
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 100;
				NPC.height = 100;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 5, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
	}
}