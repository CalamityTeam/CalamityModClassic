using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassicPreTrailer.NPCs.BrimstoneWaifu
{
	public class Brimling : ModNPC
	{
		private bool boostDR = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brimling");
			Main.npcFrameCount[NPC.type] = 8;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
				new FlavorTextBestiaryInfoElement("Extensions of the Brimstone Elementals, they possess no mind, and simply serve as additional weapons for the elemental.")
			});
		}

		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 0;
			NPC.width = 60; //324
			NPC.height = 60; //216
			NPC.defense = 0;
			NPC.lifeMax = 4000;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 6000;
			}
			NPC.knockBackResist = 0f;
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
			NPC.noGravity = true;
			NPC.noTileCollide = false;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit23;
			NPC.DeathSound = SoundID.NPCDeath39;
			if (CalamityWorldPreTrailer.downedProvidence)
			{
				NPC.lifeMax = 40000;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = 100000;
			}
			SpawnModBiomes = new int[] { ModContent.GetInstance<Crag>().Type };
		}

		public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 1f, 0f, 0f);
			bool goIntoShell = (double)NPC.life <= (double)NPC.lifeMax * 0.2;
			bool provy = (CalamityWorldPreTrailer.downedProvidence && !CalamityWorldPreTrailer.bossRushActive);
			if (goIntoShell || Main.npc[CalamityGlobalNPC.brimstoneElemental].ai[0] == 4f)
			{
				boostDR = true;
				NPC.chaseable = false;
			}
			else
			{
				boostDR = false;
				NPC.chaseable = true;
			}
			NPC.noTileCollide = true;
			float num1446 = goIntoShell ? 1f : 6f;
			int num1447 = 480;
			float num244;
			if (NPC.localAI[1] == 1f)
			{
				NPC.localAI[1] = 0f;
				if (Main.rand.Next(4) == 0)
				{
					NPC.ai[0] = (float)num1447;
				}
			}
			NPC.TargetClosest(true);
			NPC.rotation = Math.Abs(NPC.velocity.X) * (float)NPC.direction * 0.1f;
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			Vector2 value53 = NPC.Center + new Vector2((float)(NPC.direction * 20), 6f);
			Vector2 vector251 = Main.player[NPC.target].Center - value53;
			bool flag104 = Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1);
			NPC.localAI[0] += 1f;
			if (Main.netMode != 1 && NPC.localAI[0] >= 300f && Main.npc[CalamityGlobalNPC.brimstoneElemental].ai[0] != 4f)
			{
				NPC.localAI[0] = 0f;
				if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					float speed = 5f;
					Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
					float num6 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector.X + (float)Main.rand.Next(-10, 11);
					float num7 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector.Y + (float)Main.rand.Next(-10, 11);
					float num8 = (float)Math.Sqrt((double)(num6 * num6 + num7 * num7));
					num8 = speed / num8;
					num6 *= num8;
					num7 *= num8;
					int damage = 32;
					if (Main.expertMode)
					{
						damage = 24;
					}
					Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, num6, num7, Mod.Find<ModProjectile>("BrimstoneHellfireball").Type, damage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (vector251.Length() > 400f || !flag104)
			{
				Vector2 value54 = vector251;
				if (value54.Length() > num1446)
				{
					value54.Normalize();
					value54 *= num1446;
				}
				int num1448 = 30;
				NPC.velocity = (NPC.velocity * (float)(num1448 - 1) + value54) / (float)num1448;
			}
			else
			{
				NPC.velocity *= 0.98f;
			}
			if (NPC.ai[2] != 0f && NPC.ai[3] != 0f)
			{
				SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
				int num;
				for (int num1449 = 0; num1449 < 20; num1449 = num + 1)
				{
					int num1450 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, 0f, 0f, 100, Color.Transparent, 1f);
					Dust dust = Main.dust[num1450];
					dust.velocity *= 3f;
					Main.dust[num1450].noGravity = true;
					Main.dust[num1450].scale = 2.5f;
					num = num1449;
				}
				NPC.Center = new Vector2(NPC.ai[2] * 16f, NPC.ai[3] * 16f);
				NPC.velocity = Vector2.Zero;
				NPC.ai[2] = 0f;
				NPC.ai[3] = 0f;
				SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
				for (int num1451 = 0; num1451 < 20; num1451 = num + 1)
				{
					int num1452 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, 0f, 0f, 100, Color.Transparent, 1f);
					Dust dust = Main.dust[num1452];
					dust.velocity *= 3f;
					Main.dust[num1452].noGravity = true;
					Main.dust[num1452].scale = 2.5f;
					num = num1451;
				}
			}
			float[] var_9_48E3C_cp_0 = NPC.ai;
			int var_9_48E3C_cp_1 = 0;
			num244 = var_9_48E3C_cp_0[var_9_48E3C_cp_1];
			var_9_48E3C_cp_0[var_9_48E3C_cp_1] = num244 + 1f;
			if (NPC.ai[0] >= (float)num1447 && Main.netMode != 1)
			{
				if (NPC.localAI[0] > 260f)
				{
					NPC.localAI[0] -= 60f;
				}
				NPC.ai[0] = 0f;
				Point point12 = NPC.Center.ToTileCoordinates();
				Point point13 = Main.player[NPC.target].Center.ToTileCoordinates();
				int num1453 = 20;
				int num1454 = 3;
				int num1455 = 10;
				int num1456 = 1;
				int num1457 = 0;
				bool flag106 = false;
				if (vector251.Length() > 2000f)
				{
					flag106 = true;
				}
				while (!flag106 && num1457 < 100)
				{
					num1457++;
					int num1458 = Main.rand.Next(point13.X - num1453, point13.X + num1453 + 1);
					int num1459 = Main.rand.Next(point13.Y - num1453, point13.Y + num1453 + 1);
					if ((num1459 < point13.Y - num1455 || num1459 > point13.Y + num1455 || num1458 < point13.X - num1455 || num1458 > point13.X + num1455) && (num1459 < point12.Y - num1454 || num1459 > point12.Y + num1454 || num1458 < point12.X - num1454 || num1458 > point12.X + num1454) && !Main.tile[num1458, num1459].HasUnactuatedTile)
					{
						bool flag107 = true;
						if (flag107 && (Main.tile[num1458, num1459].LiquidType == LiquidID.Lava))
						{
							flag107 = false;
						}
						if (flag107 && Collision.SolidTiles(num1458 - num1456, num1458 + num1456, num1459 - num1456, num1459 + num1456))
						{
							flag107 = false;
						}
						if (flag107)
						{
							NPC.ai[2] = (float)num1458;
							NPC.ai[3] = (float)num1459;
							break;
						}
					}
				}
				NPC.netUpdate = true;
			}
		}

		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			double multiplier = 0.8;
			if (boostDR)
			{
				multiplier = 0.2;
			}
			modifiers.FinalDamage *= (float)multiplier;
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 1.0;
			if (!boostDR)
			{
				if (NPC.frameCounter > 12.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y >= frameHeight * 4)
				{
					NPC.frame.Y = 0;
				}
			}
			else
			{
				if (NPC.frameCounter > 12.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y < frameHeight * 4)
				{
					NPC.frame.Y = frameHeight * 4;
				}
				if (NPC.frame.Y >= frameHeight * 8)
				{
					NPC.frame.Y = frameHeight * 4;
				}
			}
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}