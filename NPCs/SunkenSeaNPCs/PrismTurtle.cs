using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.SunkenSeaNPCs
{
	public class PrismTurtle : ModNPC
	{
		public bool hasBeenHit = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Prism-Back");
			Main.npcFrameCount[NPC.type] = 5;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("Slow swimmers that occasionally wander to the surface of their habitat to lay eggs.")
			});
		}

		public override void SetDefaults()
		{
			NPC.noGravity = true;
			NPC.damage = Main.hardMode ? 40 : 20; //normal damage
			NPC.width = 72;
			NPC.height = 58;
			NPC.defense = Main.hardMode ? 25 : 10;
			NPC.lifeMax = Main.hardMode ? 1000 : 350;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.value = Main.hardMode ? Item.buyPrice(0, 0, 50, 0) : Item.buyPrice(0, 0, 5, 0);
			NPC.HitSound = SoundID.NPCHit24;
			NPC.DeathSound = SoundID.NPCDeath27;
			NPC.knockBackResist = 0.15f;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("PrismTurtleBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<SunkenSea>().Type };
		}

		public override void AI()
		{
			if ((NPC.Center.Y + 10f) > Main.player[NPC.target].Center.Y)
			{
				if (CalamityWorldPreTrailer.death) //gotta do damage scaling directly
				{
					NPC.damage = Main.hardMode ? 240 : 120;
				}
				else if (CalamityWorldPreTrailer.revenge)
				{
					NPC.damage = Main.hardMode ? 168 : 84;
				}
				else if (Main.expertMode)
				{
					NPC.damage = Main.hardMode ? 160 : 80;
				}
				else
				{
					NPC.damage = Main.hardMode ? 80 : 40;
				}
			}
			else
			{
				if (CalamityWorldPreTrailer.death) //gotta do damage scaling directly
				{
					NPC.damage = Main.hardMode ? 120 : 60;
				}
				else if (CalamityWorldPreTrailer.revenge)
				{
					NPC.damage = Main.hardMode ? 84 : 42;
				}
				else if (Main.expertMode)
				{
					NPC.damage = Main.hardMode ? 80 : 40;
				}
				else
				{
					NPC.damage = Main.hardMode ? 40 : 20;
				}
			}
			Lighting.AddLight(NPC.Center, ((255 - NPC.alpha) * 0f) / 255f, ((255 - NPC.alpha) * 0.75f) / 255f, ((255 - NPC.alpha) * 0.75f) / 255f);
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			NPC.noGravity = true;
			if (NPC.justHit)
			{
				hasBeenHit = true;
			}
			NPC.chaseable = hasBeenHit;
			if (NPC.direction == 0)
			{
				NPC.TargetClosest(true);
			}
			if (NPC.wet)
			{
				if (NPC.collideX || NPC.velocity.X == 0f)
				{
					NPC.velocity.X = NPC.velocity.X * -1f;
					NPC.direction *= -1;
					NPC.netUpdate = true;
				}
				if (NPC.collideY)
				{
					NPC.netUpdate = true;
					if (NPC.velocity.Y > 0f)
					{
						NPC.velocity.Y = Math.Abs(NPC.velocity.Y) * -1f;
						NPC.directionY = -1;
						NPC.ai[0] = -1f;
					}
					else if (NPC.velocity.Y < 0f)
					{
						NPC.velocity.Y = Math.Abs(NPC.velocity.Y);
						NPC.directionY = 1;
						NPC.ai[0] = 1f;
					}
				}
				NPC.velocity.X = NPC.velocity.X + (float)NPC.direction * 0.1f;
				if (NPC.velocity.X < -2f || NPC.velocity.X > 2f)
				{
					NPC.velocity.X = NPC.velocity.X * 0.95f;
				}
				if (NPC.ai[0] == -1f)
				{
					NPC.velocity.Y = NPC.velocity.Y - 0.1f;
					if (NPC.velocity.Y < -1f)
					{
						NPC.velocity.Y = -1f;
						++NPC.ai[1];
						if (NPC.ai[1] >= 120)
						{
							NPC.ai[1] = 0;
							NPC.ai[0] = 1f;
						}
					}
				}
				else
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.1f;
					if (NPC.velocity.Y > 1f)
					{
						NPC.velocity.Y = 1f;
						++NPC.ai[1];
						if (NPC.ai[1] >= 120)
						{
							NPC.ai[1] = 0;
							NPC.ai[0] = -1f;
						}
					}
				}
				int num258 = (int)(NPC.position.X + (float)(NPC.width / 2)) / 16;
				int num259 = (int)(NPC.position.Y + (float)(NPC.height / 2)) / 16;
				if (Main.tile[num258, num259 - 1].LiquidAmount > 128)
				{
					if (Main.tile[num258, num259 + 1].HasTile)
					{
						NPC.ai[0] = -1f;
					}
					else if (Main.tile[num258, num259 + 2].HasTile)
					{
						NPC.ai[0] = -1f;
					}
				}
				if ((double)NPC.velocity.Y > 0.4 || (double)NPC.velocity.Y < -0.4)
				{
					NPC.velocity.Y = NPC.velocity.Y * 0.95f;
				}
			}
			else
			{
				if (NPC.velocity.Y == 0f)
				{
					NPC.velocity.X = NPC.velocity.X * 0.94f;
					if ((double)NPC.velocity.X > -0.2 && (double)NPC.velocity.X < 0.2)
					{
						NPC.velocity.X = 0f;
					}
				}
				NPC.velocity.Y = NPC.velocity.Y + 0.4f;
				if (NPC.velocity.Y > 12f)
				{
					NPC.velocity.Y = 12f;
				}
				NPC.ai[0] = 1f;
				NPC.rotation = NPC.velocity.Y * (float)NPC.direction * 0.1f;
				if ((double)NPC.rotation < -0.2)
				{
					NPC.rotation = -0.2f;
				}
				if ((double)NPC.rotation > 0.2)
				{
					NPC.rotation = 0.2f;
					return;
				}
			}
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += (NPC.wet ? 0.1f : 0f);
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}

		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (NPC.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y);
			Vector2 vector11 = new Vector2((float)(TextureAssets.Npc[NPC.type].Value.Width / 2), (float)(TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2));
			Vector2 vector = center - Main.screenPosition;
			vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/SunkenSeaNPCs/PrismTurtleGlow").Value.Width, (float)(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/SunkenSeaNPCs/PrismTurtleGlow").Value.Height / Main.npcFrameCount[NPC.type])) * 1f / 2f;
			vector += vector11 * 1f + new Vector2(0f, 0f + 4f + NPC.gfxOffY);
			Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.Blue);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/SunkenSeaNPCs/PrismTurtleGlow").Value, vector,
				new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, vector11, 1f, spriteEffects, 0f);
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			if (projectile.minion)
			{
				return hasBeenHit;
			}
			return null;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea && spawnInfo.Water && !spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().clamity)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.9f;
			}
			return 0f;
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.ByCondition(new DownedDS(), Mod.Find<ModItem>("PrismShard").Type, 1, 1, 4));
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 68, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("PrismTurtleGore1").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("PrismTurtleGore2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("PrismTurtleGore3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("PrismTurtleGore4").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("PrismTurtleGore5").Type, 1f);
				}
				for (int k = 0; k < 25; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 68, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}