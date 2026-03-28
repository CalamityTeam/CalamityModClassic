using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
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
	public class MorayEel : ModNPC
	{
		public bool hasBeenHit = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Moray Eel");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("These eels prefer to remain hidden within underwater structures, only ocassionally roaming open water.")
			});
		}

		public override void SetDefaults()
		{
			NPC.noGravity = true;
			NPC.damage = 45;
			NPC.width = 136;
			NPC.height = 40;
			NPC.defense = 8;
			NPC.lifeMax = 180;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.buffImmune[Mod.Find<ModBuff>("CrushDepth").Type] = true;
			NPC.value = Item.buyPrice(0, 0, 1, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.75f;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("MorayEelBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<AbyssLayer1Biome>().Type };
		}

		public override void AI()
		{
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			NPC.noGravity = true;
			if (NPC.direction == 0)
			{
				NPC.TargetClosest(true);
			}
			if (NPC.justHit)
			{
				hasBeenHit = true;
			}
			NPC.chaseable = hasBeenHit;
			if (NPC.wet)
			{
				bool flag14 = hasBeenHit;
				NPC.TargetClosest(false);
				if (Main.player[NPC.target].wet && !Main.player[NPC.target].dead &&
					Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) &&
					(Main.player[NPC.target].Center - NPC.Center).Length() < ((Main.player[NPC.target].GetModPlayer<CalamityPlayerPreTrailer>().anechoicPlating ||
					Main.player[NPC.target].GetModPlayer<CalamityPlayerPreTrailer>().anechoicCoating) ? 250f : 300f) *
					(Main.player[NPC.target].GetModPlayer<CalamityPlayerPreTrailer>().fishAlert ? 3f : 1f))
				{
					flag14 = true;
				}
				if ((!Main.player[NPC.target].wet || Main.player[NPC.target].dead) && flag14)
				{
					flag14 = false;
				}
				if (!flag14)
				{
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
				}
				if (flag14)
				{
					NPC.TargetClosest(true);
					NPC.velocity.X = NPC.velocity.X + (float)NPC.direction * 0.15f;
					NPC.velocity.Y = NPC.velocity.Y + (float)NPC.directionY * 0.1f;
					if (NPC.velocity.X > 6f)
					{
						NPC.velocity.X = 6f;
					}
					if (NPC.velocity.X < -6f)
					{
						NPC.velocity.X = -6f;
					}
					if (NPC.velocity.Y > 4f)
					{
						NPC.velocity.Y = 4f;
					}
					if (NPC.velocity.Y < -4f)
					{
						NPC.velocity.Y = -4f;
					}
				}
				else
				{
					NPC.velocity.X = NPC.velocity.X + (float)NPC.direction * 0.1f;
					if (NPC.velocity.X < -2f || NPC.velocity.X > 2f)
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
			vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/MorayEelGlow").Value.Width, (float)(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/MorayEelGlow").Value.Height / Main.npcFrameCount[NPC.type])) * 1f / 2f;
			vector += vector11 * 1f + new Vector2(0f, 0f + 4f + NPC.gfxOffY);
			Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.Gold);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/MorayEelGlow").Value, vector,
				new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, vector11, 1f, spriteEffects, 0f);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Bleeding, 180, true);
			target.AddBuff(BuffID.Venom, 90, true);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSulphur)
			{
				return 0f;
			}
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyssLayer1 && spawnInfo.Water)
			{
				return SpawnCondition.CaveJellyfish.Chance * 1.2f;
			}
			return SpawnCondition.OceanMonster.Chance * 0.1f;
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(ItemID.Flipper, 25));
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 25; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}