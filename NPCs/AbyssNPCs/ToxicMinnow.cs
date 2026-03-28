using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.AbyssNPCs
{
	public class ToxicMinnow : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Toxic Minnow");
			Main.npcFrameCount[NPC.type] = 4;
		}

		public override void SetDefaults()
		{
			NPC.noGravity = true;
			NPC.damage = 20;
			NPC.width = 80;
			NPC.height = 40;
			NPC.defense = 20;
			NPC.lifeMax = 240;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.value = Item.buyPrice(0, 0, 5, 0);
			NPC.buffImmune[Mod.Find<ModBuff>("CrushDepth").Type] = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.15f;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("ToxicMinnowBanner").Type;
			SpawnModBiomes = new int[3] { ModContent.GetInstance<AbyssLayer1Biome>().Type, ModContent.GetInstance<AbyssLayer2Biome>().Type, ModContent.GetInstance<AbyssLayer3Biome>().Type };
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("Perhaps in a strange bid from evolution, these fish will process toxic chemicals from the plants they eat and store them as a self defense mechanism... however, it's only released on death, rendering it moot.")
			});
		}

		public override void AI()
		{
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			NPC.noGravity = true;
			if (NPC.direction == 0)
			{
				NPC.TargetClosest(true);
			}
			if (NPC.wet)
			{
				NPC.TargetClosest(false);
				if (NPC.collideX)
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
				if (NPC.velocity.X < -0.2f || NPC.velocity.X > 0.2f)
				{
					NPC.velocity.X = NPC.velocity.X * 0.95f;
				}
				if (NPC.ai[0] == -1f)
				{
					NPC.velocity.Y = NPC.velocity.Y - 0.01f;
					if ((double)NPC.velocity.Y < -0.3)
					{
						NPC.ai[0] = 1f;
					}
				}
				else
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.01f;
					if ((double)NPC.velocity.Y > 0.3)
					{
						NPC.ai[0] = -1f;
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
				NPC.velocity.Y = NPC.velocity.Y + 0.3f;
				if (NPC.velocity.Y > 3f)
				{
					NPC.velocity.Y = 3f;
				}
				NPC.ai[0] = 1f;
			}
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

		public override bool CheckDead()
		{
			SoundEngine.PlaySound(SoundID.NPCDeath14, NPC.position);
			NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
			NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
			NPC.width = (NPC.height = 40);
			NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
			NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
			if (Main.netMode != 1)
			{
				Vector2 valueBoom = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float spreadBoom = 15f * 0.0174f;
				double startAngleBoom = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spreadBoom / 2;
				double deltaAngleBoom = spreadBoom / 8f;
				double offsetAngleBoom;
				int iBoom;
				int damageBoom = 30;
				for (iBoom = 0; iBoom < 5; iBoom++)
				{
					int projectileType = Mod.Find<ModProjectile>("ToxicMinnowCloud").Type;
					offsetAngleBoom = (startAngleBoom + deltaAngleBoom * (iBoom + iBoom * iBoom) / 2f) + 32f * iBoom;
					int boom1 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), valueBoom.X, valueBoom.Y, (float)(Math.Sin(offsetAngleBoom) * 6f), (float)(Math.Cos(offsetAngleBoom) * 6f), projectileType, damageBoom, 0f, Main.myPlayer, 0f, 0f);
					int boom2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), valueBoom.X, valueBoom.Y, (float)(-Math.Sin(offsetAngleBoom) * 6f), (float)(-Math.Cos(offsetAngleBoom) * 6f), projectileType, damageBoom, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			NPC.netUpdate = true;
			return true;
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
			vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/ToxicMinnowGlow").Value.Width, (float)(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/ToxicMinnowGlow").Value.Height / Main.npcFrameCount[NPC.type])) * 1f / 2f;
			vector += vector11 * 1f + new Vector2(0f, 0f + 4f + NPC.gfxOffY);
			Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.LightGreen);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/ToxicMinnowGlow").Value, vector,
				new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, vector11, 1f, spriteEffects, 0f);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 120, true);
		}

		public override void FindFrame(int frameHeight)
		{
			if (!NPC.wet)
			{
				NPC.frameCounter = 0.0;
				return;
			}
			NPC.frameCounter += 0.15f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyssLayer1 && spawnInfo.Water)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.6f;
			}
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyssLayer2 && spawnInfo.Water)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.9f;
			}
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyssLayer3 && spawnInfo.Water)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.6f;
			}
			return 0f;
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule fakeCalorPlantDead = new LeadingConditionRule(new DownedCalDoppelorPlantera());
			
			npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), Mod.Find<ModItem>("HalibutCannon").Type, 1000000));
			fakeCalorPlantDead.OnSuccess(new CommonDrop(Mod.Find<ModItem>("DepthCells").Type, 2, 2, 4));
			fakeCalorPlantDead.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("DepthCells").Type, 2, 2, 4));
			npcLoot.Add(fakeCalorPlantDead);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 40, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 40, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}