using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.AbyssNPCs
{
	public class GiantSquid : ModNPC
	{
		public bool hasBeenHit = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Giant Squid");
			Main.npcFrameCount[NPC.type] = 5;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("splatoon inkling real")
			});
		}

		public override void SetDefaults()
		{
			NPC.noGravity = true;
			NPC.damage = 80;
			NPC.width = 50;
			NPC.height = 220;
			NPC.defense = 25;
			NPC.lifeMax = 400;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.buffImmune[Mod.Find<ModBuff>("CrushDepth").Type] = true;
			NPC.value = Item.buyPrice(0, 0, 15, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("GiantSquidBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<AbyssLayer2Biome>().Type, ModContent.GetInstance<AbyssLayer3Biome>().Type };
		}

		public override void AI()
		{
			if (NPC.direction == 0)
			{
				NPC.TargetClosest(true);
			}
			if (!NPC.wet)
			{
				if (NPC.velocity.Y == 0f)
				{
					NPC.velocity.X = NPC.velocity.X * 0.98f;
					if ((double)NPC.velocity.X > -0.01 && (double)NPC.velocity.X < 0.01)
					{
						NPC.velocity.X = 0f;
					}
				}
				NPC.velocity.Y = NPC.velocity.Y + 0.2f;
				if (NPC.velocity.Y > 10f)
				{
					NPC.velocity.Y = 10f;
				}
				NPC.ai[0] = 1f;
				return;
			}
			if (NPC.collideX)
			{
				NPC.velocity.X = NPC.velocity.X * -1f;
				NPC.direction *= -1;
			}
			if (NPC.collideY)
			{
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
			NPC.TargetClosest(false);
			if ((Main.player[NPC.target].wet && !Main.player[NPC.target].dead &&
				Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) &&
				(Main.player[NPC.target].Center - NPC.Center).Length() < ((Main.player[NPC.target].GetModPlayer<CalamityPlayerPreTrailer>().anechoicPlating ||
				Main.player[NPC.target].GetModPlayer<CalamityPlayerPreTrailer>().anechoicCoating) ? 300f : 500f) *
				(Main.player[NPC.target].GetModPlayer<CalamityPlayerPreTrailer>().fishAlert ? 3f : 1f)) ||
				NPC.justHit)
			{
				hasBeenHit = true;
			}
			NPC.chaseable = hasBeenHit;
			NPC.rotation = NPC.velocity.X * 0.02f;
			if (hasBeenHit)
			{
				NPC.localAI[2] = 1f;
				NPC.velocity *= 0.975f;
				float num263 = 1.6f;
				if (NPC.velocity.X > -num263 && NPC.velocity.X < num263 && NPC.velocity.Y > -num263 && NPC.velocity.Y < num263)
				{
					NPC.TargetClosest(true);
					float num264 = 16f;
					Vector2 vector31 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num265 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector31.X;
					float num266 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector31.Y;
					float num267 = (float)Math.Sqrt((double)(num265 * num265 + num266 * num266));
					num267 = num264 / num267;
					num265 *= num267;
					num266 *= num267;
					NPC.velocity.X = num265;
					NPC.velocity.Y = num266;
					return;
				}
			}
			else
			{
				NPC.localAI[2] = 0f;
				NPC.velocity.X = NPC.velocity.X + (float)NPC.direction * 0.02f;
				if (NPC.velocity.X < -1f || NPC.velocity.X > 1f)
				{
					NPC.velocity.X = NPC.velocity.X * 0.95f;
				}
				if (NPC.ai[0] == -1f)
				{
					NPC.velocity.Y = NPC.velocity.Y - 0.01f;
					if (NPC.velocity.Y < -1f)
					{
						NPC.ai[0] = 1f;
					}
				}
				else
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.01f;
					if (NPC.velocity.Y > 1f)
					{
						NPC.ai[0] = -1f;
					}
				}
				int num268 = (int)(NPC.position.X + (float)(NPC.width / 2)) / 16;
				int num269 = (int)(NPC.position.Y + (float)(NPC.height / 2)) / 16;
				if (Main.tile[num268, num269 - 1].LiquidAmount > 128)
				{
					if (Main.tile[num268, num269 + 1].HasTile)
					{
						NPC.ai[0] = -1f;
					}
					else if (Main.tile[num268, num269 + 2].HasTile)
					{
						NPC.ai[0] = -1f;
					}
				}
				else
				{
					NPC.ai[0] = 1f;
				}
				if ((double)NPC.velocity.Y > 1.2 || (double)NPC.velocity.Y < -1.2)
				{
					NPC.velocity.Y = NPC.velocity.Y * 0.99f;
					return;
				}
			}
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			if (projectile.minion)
			{
				return hasBeenHit;
			}
			return null;
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += (hasBeenHit ? 0.15f : 0.075f);
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
			vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/GiantSquidGlow").Value.Width, (float)(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/GiantSquidGlow").Value.Height / Main.npcFrameCount[NPC.type])) * 1f / 2f;
			vector += vector11 * 1f + new Vector2(0f, 0f + 4f + NPC.gfxOffY);
			Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.Cyan);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/GiantSquidGlow").Value, vector,
				new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, vector11, 1f, spriteEffects, 0f);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyssLayer2 && spawnInfo.Water)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.6f;
			}
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyssLayer3 && spawnInfo.Water)
			{
				return SpawnCondition.CaveJellyfish.Chance * 1.2f;
			}
			return 0f;
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 180, true);
			target.AddBuff(BuffID.Darkness, 180, true);
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("MarkedforDeath").Type, 120);
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120, true);
			}
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule fakeCalorPlantDead = new LeadingConditionRule(new DownedCalDoppelorPlantera());
			
			npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), Mod.Find<ModItem>("HalibutCannon").Type, 1000000));
			fakeCalorPlantDead.OnSuccess(new CommonDrop(Mod.Find<ModItem>("Lumenite").Type, 1));
			fakeCalorPlantDead.OnSuccess(new CommonDrop(Mod.Find<ModItem>("DepthCells").Type, 2, 2, 5));
			fakeCalorPlantDead.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("DepthCells").Type, 2, 1, 3));
			npcLoot.Add(fakeCalorPlantDead);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}

			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					for (int k = 0; k < 30; k++)
					{
						Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color),
							1f);
					}

					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("GiantSquid").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("GiantSquid2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("GiantSquid3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("GiantSquid4").Type, 1f);
				}
			}
		}
	}
}