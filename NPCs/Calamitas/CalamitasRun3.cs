using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Generation;
using CalamityModClassicPreTrailer.Tiles;
using CalamityModClassicPreTrailer.NPCs.Calamitas;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.Calamitas;
using CalamityModClassicPreTrailer.Items.Placeables;
using CalamityModClassicPreTrailer.Items.Weapons.Calamitas;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.WorldBuilding;
using Conditions = Terraria.GameContent.ItemDropRules.Conditions;

namespace CalamityModClassicPreTrailer.NPCs.Calamitas
{
	[AutoloadBossHead]
	public class CalamitasRun3 : ModNPC
	{
		private float bossLife;
		private bool halfLife = false;
		private bool secondStage = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Calamitas");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				new FlavorTextBestiaryInfoElement("A failed clone of the Brimstone Witch contained within a metallic vessel, she wanders the night with seemingly little purpose.")
			});
		}

		public override void SetDefaults()
		{
			NPC.damage = 80;
			NPC.npcSlots = 14f;
			NPC.width = 120;
			NPC.height = 120;
			NPC.defense = 25;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 36750 : 27500;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 60250;
			}
			NPC.aiStyle = -1; //new
			AIType = -1; //new
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 15, 0, 0);
			NPCID.Sets.TrailCacheLength[NPC.type] = 8;
			NPCID.Sets.TrailingMode[NPC.type] = 1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("MarkedforDeath").Type] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[BuffID.Daybreak] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("DemonFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Nightwither").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("WhisperingDeath").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("SilvaStun").Type] = false;
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/Calamitas");
			if (CalamityWorldPreTrailer.downedProvidence)
			{
				NPC.damage = 160;
				NPC.defense = 150;
				NPC.lifeMax = 120000;
				NPC.value = Item.buyPrice(0, 35, 0, 0);
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 3300000 : 3000000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
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
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			bool dayTime = Main.dayTime;
			bool provy = (CalamityWorldPreTrailer.downedProvidence && !CalamityWorldPreTrailer.bossRushActive);
			Player player = Main.player[NPC.target];
			if (!halfLife && NPC.life <= NPC.lifeMax / 2)
			{
				if (!secondStage && Main.netMode != 1)
				{
					SoundEngine.PlaySound(SoundID.Item74, NPC.position);
					for (int I = 0; I < 5; I++)
					{
						int FireEye = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)(NPC.Center.X + (Math.Sin(I * 72) * 150)), (int)(NPC.Center.Y + (Math.Cos(I * 72) * 150)), Mod.Find<ModNPC>("SoulSeeker").Type, NPC.whoAmI, 0, 0, 0, -1);
						NPC Eye = Main.npc[FireEye];
						Eye.ai[0] = I * 72;
						Eye.ai[3] = I * 72;
					}
					secondStage = true;
				}
				string key = "Impressive child, most impressive...";
				Color messageColor = Color.Orange;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
				else if (Main.netMode == 2)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
				halfLife = true;
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
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("RedSpawn").Type, 0, 0f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("GraySpawn").Type, 0, 0f, Main.myPlayer, 0f, 0f);
						if (CalamityWorldPreTrailer.death)
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("RedSpawn").Type, 0, 0f, Main.myPlayer, 0f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("GraySpawn").Type, 0, 0f, Main.myPlayer, 0f, 0f);
						}
						string key = "The brothers have been reborn!";
						Color messageColor = Color.Orange;
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
						return;
					}
				}
			}
			bool flag100 = false;
			int num568 = 0;
			if (expertMode)
			{
				if (NPC.AnyNPCs(Mod.Find<ModNPC>("CalamitasRun").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("CalamitasRun2").Type))
				{
					flag100 = true;
					num568 += 255;
				}
				NPC.defense += num568 * 50;
				if (!flag100)
				{
					NPC.defense = provy ? 150 : 25;
				}
			}
			NPC.chaseable = !flag100;
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
			else if (NPC.timeLeft < 1800)
			{
				NPC.timeLeft = 1800;
			}
			if (NPC.ai[1] == 0f)
			{
				float num823 = expertMode ? 10f : 8.5f;
				float num824 = expertMode ? 0.18f : 0.155f;
				Vector2 vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num825 = player.position.X + (float)(player.width / 2) - vector82.X;
				float num826 = player.position.Y + (float)(player.height / 2) - 360f - vector82.Y;
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
				if (NPC.ai[2] >= 200f)
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
				if (Main.netMode != 1)
				{
					NPC.localAI[1] += 1f;
					if (revenge)
					{
						NPC.localAI[1] += 0.5f;
					}
					if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
					{
						NPC.localAI[1] += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5 || CalamityWorldPreTrailer.bossRushActive)
					{
						NPC.localAI[1] += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
					{
						NPC.localAI[1] += 1f;
					}
					if (NPC.localAI[1] > 180f && Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
					{
						NPC.localAI[1] = 0f;
						float num828 = expertMode ? 14f : 12.5f;
						if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
						{
							num828 += 5f;
						}
						int num829 = expertMode ? 34 : 42;
						int num830 = Mod.Find<ModProjectile>("BrimstoneHellfireball").Type;
						num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
						num827 = num828 / num827;
						num825 *= num827;
						num826 *= num827;
						vector82.X += num825 * 15f;
						vector82.Y += num826 * 15f;
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector82.X, vector82.Y, num825, num826, num830, num829 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
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
				float num832 = expertMode ? 10f : 8.5f;
				float num833 = expertMode ? 0.255f : 0.205f;
				Vector2 vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num834 = player.position.X + (float)(player.width / 2) + (float)(num831 * 360) - vector83.X;
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
				if (Main.netMode != 1)
				{
					NPC.localAI[1] += 1f;
					if (revenge)
					{
						NPC.localAI[1] += 0.5f;
					}
					if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
					{
						NPC.localAI[1] += 0.5f;
					}
					if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
					{
						NPC.localAI[1] += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5 || CalamityWorldPreTrailer.bossRushActive)
					{
						NPC.localAI[1] += 0.5f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
					{
						NPC.localAI[1] += 0.5f;
					}
					if (expertMode)
					{
						NPC.localAI[1] += 1.5f;
					}
					if (NPC.localAI[1] > 60f && Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
					{
						NPC.localAI[1] = 0f;
						float num837 = 11f;
						int num838 = expertMode ? 24 : 30;
						int num839 = Mod.Find<ModProjectile>("BrimstoneLaser").Type;
						num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
						num836 = num837 / num836;
						num834 *= num836;
						num835 *= num836;
						vector83.X += num834 * 15f;
						vector83.Y += num835 * 15f;
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector83.X, vector83.Y, num834, num835, num839, num838 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
					}
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= 120f)
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

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (!NPC.active || NPC.IsABestiaryIconDummy)
			{
				return true;
			}
			SpriteEffects spriteEffects = SpriteEffects.None;
			Microsoft.Xna.Framework.Color color24 = NPC.GetAlpha(drawColor);
			Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
			Texture2D texture2D3 = TextureAssets.Npc[NPC.type].Value;
			int num156 = TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type];
			int y3 = num156 * (int)NPC.frameCounter;
			Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(0, y3, texture2D3.Width, num156);
			Vector2 origin2 = rectangle.Size() / 2f;
			int num157 = 8;
			int num158 = 2;
			int num159 = 1;
			float num160 = 0f;
			int num161 = num159;
			while (((num158 > 0 && num161 < num157) || (num158 < 0 && num161 > num157)) && Lighting.NotRetro)
			{
				Microsoft.Xna.Framework.Color color26 = NPC.GetAlpha(color25);
				{
					goto IL_6899;
				}
			IL_6881:
				num161 += num158;
				continue;
			IL_6899:
				float num164 = (float)(num157 - num161);
				if (num158 < 0)
				{
					num164 = (float)(num159 - num161);
				}
				color26 *= num164 / ((float)NPCID.Sets.TrailCacheLength[NPC.type] * 1.5f);
				Vector2 value4 = (NPC.oldPos[num161]);
				float num165 = NPC.rotation;
				Main.spriteBatch.Draw(texture2D3, value4 + NPC.Size / 2f - Main.screenPosition + new Vector2(0, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26, num165 + NPC.rotation * num160 * (float)(num161 - 1) * -(float)spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin2, NPC.scale, spriteEffects, 0f);
				goto IL_6881;
			}
			var something = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(texture2D3, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), NPC.frame, color24, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, something, 0);
			return false;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
			npcLoot.Add(new CommonDrop(ModContent.ItemType<CalamitasTrophy>(), 10));
			npcLoot.Add(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<CalamitasBag>(),
				1,
				5, 5));
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<CalamitasBag>()));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<EssenceofChaos>(), 1, 4, 9));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<CalamityDust>(), 1, 9, 15));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<BlightedLens>(), 1, 1, 3));
			notExpert.OnSuccess(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<Bloodstone>(), 1, 30, 41)); 
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<ChaosStone>(), 10));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<CalamitasInferno>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<CalamitasMask>(), 7));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<TheEyeofCalamitas>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<BlightedEyeStaff>(), 4));
			npcLoot.Add(notExpert);
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
				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Calamitas").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Calamitas2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Calamitas3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Calamitas4").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Calamitas5").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Calamitas6").Type, 1f);
					NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
					NPC.width = 100;
					NPC.height = 100;
					NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
					for (int num621 = 0; num621 < 40; num621++)
					{
						int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height,
							235, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num622].velocity *= 3f;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num622].scale = 0.5f;
							Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}

					for (int num623 = 0; num623 < 70; num623++)
					{
						int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height,
							235, 0f, 0f, 100, default(Color), 3f);
						Main.dust[num624].noGravity = true;
						Main.dust[num624].velocity *= 5f;
						num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235,
							0f, 0f, 100, default(Color), 2f);
						Main.dust[num624].velocity *= 2f;
					}
				}
			}
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.damage = (int)(NPC.damage * 0.8f);
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180, true);
			}
			target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300, true);
		}
	}
}