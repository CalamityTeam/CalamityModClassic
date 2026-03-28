using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
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
	public class EutrophicRay : ModNPC
	{
		public bool hasBeenHit = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eutrophic Ray");
			Main.npcFrameCount[NPC.type] = 5;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("A stingray that spends most of its time as a docile creature, but that will release its energy reserves as short bursts of speed when threatened.")
			});
		}

		public override void SetDefaults()
		{
			NPC.damage = 20;
			NPC.width = 116;
			NPC.height = 36;
			NPC.defense = Main.hardMode ? 15 : 5;
			NPC.lifeMax = Main.hardMode ? 500 : 150;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.value = Main.hardMode ? Item.buyPrice(0, 0, 50, 0) : Item.buyPrice(0, 0, 5, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath55;
			NPC.knockBackResist = 0f;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("EutrophicRayBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<SunkenSea>().Type };
		}

		public override void AI()
		{
			NPC.TargetClosest(true);
			if (NPC.velocity.X > 0.25f)
			{
				NPC.spriteDirection = 1;
			}
			else if (NPC.velocity.X < 0.25f)
			{
				NPC.spriteDirection = -1;
			}
			if (NPC.justHit && !hasBeenHit)
			{
				hasBeenHit = true;
				NPC.damage = Main.expertMode ? 40 : 20;
				if (Main.hardMode)
				{
					NPC.damage = Main.expertMode ? 100 : 50;
				}
				NPC.noTileCollide = true;
				NPC.noGravity = true;
				if ((NPC.Center.X) < Main.player[NPC.target].Center.X)
				{
					NPC.ai[0] = 1f;
				}
				else
				{
					NPC.ai[0] = 2f;
				}
			}
			NPC.chaseable = hasBeenHit;
			if (hasBeenHit)
			{
				float AccelerationY = Main.hardMode ? 0.4f : 0.2f;
				float MaxSpeedY = Main.hardMode ? 4f : 2.5f;
				float Rotation = 0;
				if ((NPC.Center.Y + 0.4f) > Main.player[NPC.target].Center.Y)
				{
					NPC.velocity.Y -= AccelerationY;
					if (NPC.velocity.Y < -MaxSpeedY)
					{
						NPC.velocity.Y = -MaxSpeedY;
					}
				}
				else if ((NPC.Center.Y - 0.4f) < Main.player[NPC.target].Center.Y)
				{
					NPC.velocity.Y += AccelerationY;
					if (NPC.velocity.Y > MaxSpeedY)
					{
						NPC.velocity.Y = MaxSpeedY;
					}
				}
				float AccelerationX = Main.hardMode ? 0.4f : 0.25f;
				float MaxSpeedX = Main.hardMode ? 6f : 4f;

				if (NPC.ai[0] == 1f)
				{
					Rotation = -0.05f;
					NPC.velocity.X -= AccelerationX;
					if (NPC.velocity.X < -MaxSpeedX)
					{
						NPC.velocity.X = -MaxSpeedX;
					}

					if ((NPC.Center.X + 300f) < Main.player[NPC.target].Center.X)
					{
						NPC.ai[0] = 2f;
					}
				}
				else if (NPC.ai[0] == 2f)
				{
					Rotation = 0.05f;
					NPC.velocity.X += AccelerationX;
					if (NPC.velocity.X > MaxSpeedX)
					{
						NPC.velocity.X = MaxSpeedX;
					}

					if ((NPC.Center.X - 300f) > Main.player[NPC.target].Center.X)
					{
						NPC.ai[0] = 1f;
					}
				}

				NPC.rotation = NPC.velocity.Y * Rotation;
				if ((double)NPC.rotation < -0.1)
				{
					NPC.rotation = -0.1f;
				}
				if ((double)NPC.rotation > 0.1)
				{
					NPC.rotation = 0.1f;
					return;
				}
			}
			else
			{
				NPC.damage = 0;
			}
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += (hasBeenHit ? 0.15f : 0f);
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
			vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/SunkenSeaNPCs/EutrophicRayGlow").Value.Width, (float)(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/SunkenSeaNPCs/EutrophicRayGlow").Value.Height / Main.npcFrameCount[NPC.type])) * 1f / 2f;
			vector += vector11 * 1f + new Vector2(0f, 0f + 4f + NPC.gfxOffY);
			Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.LightBlue);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/SunkenSeaNPCs/EutrophicRayGlow").Value, vector,
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
				return SpawnCondition.CaveJellyfish.Chance * 0.6f;
			}
			return 0f;
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("EutrophicShank").Type, 3));
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 68, hit.HitDirection, -1f, 0, default(Color), 1f);
			}

			if (NPC.life <= 0)
			{
				for (int k = 0; k < 25; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 68, hit.HitDirection, -1f, 0, default(Color), 1f);
				}

				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("RayGore1").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("RayGore2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("RayGore3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("RayGore4").Type, 1f);
				}
			}
		}
	}
}