using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.CeaselessVoid
{
	[AutoloadBossHead]
	public class CeaselessVoid : ModNPC
	{
		private float bossLife;
		private float beamPortal = 0f;
		private float shootBoost = 0;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ceaseless Void");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
				new FlavorTextBestiaryInfoElement("Created after a failed experiment with the Devourer of Gods' dimension hopping ability, this being desires nothing more than to consume all the life force it can.")
			});
		}

		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.npcSlots = 36f;
			NPC.width = 100; //324
			NPC.height = 100; //216
			NPC.defense = 0;
			NPC.lifeMax = 200;
			if (Main.expertMode)
			{
				NPC.lifeMax = 400;
			}
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/ScourgeofTheUniverse");
			if (CalamityWorldPreTrailer.DoGSecondStageCountdown <= 0)
			{
				NPC.value = Item.buyPrice(0, 35, 0, 0);
				Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/Void");
			}
			NPC.aiStyle = -1; //new
			AIType = -1; //new
			NPC.knockBackResist = 0f;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.boss = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
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
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			CalamityGlobalNPC.voidBoss = NPC.whoAmI;
			Vector2 vector = NPC.Center;
			NPC.TargetClosest(true);
			if (NPC.CountNPCS(Mod.Find<ModNPC>("DarkEnergy").Type) > 0 ||
				NPC.CountNPCS(Mod.Find<ModNPC>("DarkEnergy2").Type) > 0 ||
				NPC.CountNPCS(Mod.Find<ModNPC>("DarkEnergy3").Type) > 0)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = false;
			}
			if (!player.active || player.dead)
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead)
				{
					NPC.velocity = new Vector2(0f, -10f);
					CalamityWorldPreTrailer.DoGSecondStageCountdown = 0;
					if (Main.netMode == 2)
					{
						var netMessage = Mod.GetPacket();
						netMessage.Write((byte)CalamityModClassicPreTrailerMessageType.DoGCountdownSync);
						netMessage.Write(CalamityWorldPreTrailer.DoGSecondStageCountdown);
						netMessage.Send();
					}
					if (NPC.timeLeft > 150)
					{
						NPC.timeLeft = 150;
					}
					return;
				}
			}
			else if (NPC.timeLeft < 2400)
			{
				NPC.timeLeft = 2400;
			}
			if (Main.netMode != 1)
			{
				beamPortal += expertMode ? 2f : 1f;
				beamPortal += shootBoost;
				if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
				{
					beamPortal += 4f;
				}
				if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
				{
					beamPortal += 2f;
				}
				if (beamPortal >= 1200f)
				{
					beamPortal = 0f;
					NPC.TargetClosest(true);
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
					{
						float num941 = 3f; //speed
						Vector2 vector104 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
						float num942 = player.position.X + (float)player.width * 0.5f - vector104.X + (float)Main.rand.Next(-20, 21);
						float num943 = player.position.Y + (float)player.height * 0.5f - vector104.Y + (float)Main.rand.Next(-20, 21);
						float num944 = (float)Math.Sqrt((double)(num942 * num942 + num943 * num943));
						num944 = num941 / num944;
						num942 *= num944;
						num943 *= num944;
						num942 += (float)Main.rand.Next(-10, 11) * 0.05f;
						num943 += (float)Main.rand.Next(-10, 11) * 0.05f;
						int num945 = expertMode ? 42 : 58;
						int num946 = Mod.Find<ModProjectile>("DoGBeamPortal").Type;
						vector104.X += num942 * 5f;
						vector104.Y += num943 * 5f;
						int num947 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector104.X, vector104.Y, num942, num943, num946, num945, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[num947].timeLeft = 300;
						NPC.netUpdate = true;
					}
					if (NPC.life <= (int)((double)NPC.lifeMax * 0.5) && revenge)
					{
						float spread = 45f * 0.0174f;
						double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
						double deltaAngle = spread / 8f;
						double offsetAngle;
						int damage = expertMode ? 42 : 58;
						int i;
						float passedVar = 1f;
						for (i = 0; i < 4; i++)
						{
							offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, (float)(Math.Sin(offsetAngle) * 3f), (float)(Math.Cos(offsetAngle) * 3f), Mod.Find<ModProjectile>("DarkEnergyBall").Type, damage, 0f, Main.myPlayer, passedVar, 0f);
							passedVar += 1f;
						}
					}
				}
			}
			float num823 = 10f;
			float num824 = 0.2f;
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
			if (bossLife == 0f && NPC.life > 0)
			{
				bossLife = (float)NPC.lifeMax;
			}
			if (NPC.life > 0)
			{
				if (Main.netMode != 1)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.26);
					if ((float)(NPC.life + num660) < bossLife)
					{
						bossLife = (float)NPC.life;
						shootBoost += 1f;
						int glob = revenge ? 8 : 4;
						if (bossLife <= 0.5f)
						{
							glob = revenge ? 16 : 8;
						}
						for (int num662 = 0; num662 < glob; num662++)
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("DarkEnergySpawn").Type, 0, 0f, Main.myPlayer, 0f, 0f);
						}
						return;
					}
				}
			}
		}

		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			modifiers.SetMaxDamage(1);
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.ByCondition(new NotInDoGSentinelPhase(), Mod.Find<ModItem>("DarkPlasma").Type, 1, 2, 4));
			npcLoot.Add(ItemDropRule.ByCondition(new NotInDoGSentinelPhase(), Mod.Find<ModItem>("CeaselessVoidTrophy").Type, 10));
			npcLoot.Add(ItemDropRule.ByCondition(new NotInDoGSentinelPhase(), Mod.Find<ModItem>("MirrorBlade").Type, 3));
			npcLoot.Add(ItemDropRule.ByCondition(new NotInDoGSentinelPhase(), Mod.Find<ModItem>("TheEvolution").Type, 40));
			npcLoot.Add(ItemDropRule.ByCondition(new NotInDoGSentinelPhase(), Mod.Find<ModItem>("ArcanumoftheVoid").Type, 5));
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 173, hit.HitDirection, -1f, 0, default(Color), 1f);
			}

			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
					NPC.width = 100;
					NPC.height = 100;
					NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
					for (int num621 = 0; num621 < 40; num621++)
					{
						int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height,
							173, 0f, 0f, 100, default(Color), 2f);
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
							173, 0f, 0f, 100, default(Color), 3f);
						Main.dust[num624].noGravity = true;
						Main.dust[num624].velocity *= 5f;
						num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173,
							0f, 0f, 100, default(Color), 2f);
						Main.dust[num624].velocity *= 2f;
					}

					float randomSpread = (float)(Main.rand.Next(-200, 200) / 100);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("CeaselessVoid").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("CeaselessVoid2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("CeaselessVoid3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("CeaselessVoid4").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("CeaselessVoid5").Type, 1f);
				}
			}
		}
	}
}