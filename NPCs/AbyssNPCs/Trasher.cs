using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.AbyssNPCs
{
	public class Trasher : ModNPC
	{
		public bool hasBeenHit = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Trasher");
			Main.npcFrameCount[NPC.type] = 8;
		}

		public override void SetDefaults()
		{
			NPC.noGravity = true;
			NPC.damage = 50;
			NPC.width = 150;
			NPC.height = 40;
			NPC.defense = 22;
			NPC.lifeMax = 200;
			NPC.aiStyle = -1;
			AIType = -1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.value = Item.buyPrice(0, 0, 3, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath5;
			NPC.knockBackResist = 0f;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("TrasherBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<Sulphur>().Type };
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("Despite their ferocious appearance and great jaw, these crocodiles prefer to scavenge for food and leftovers.")
			});
		}

		public override void AI()
		{
			NPC.spriteDirection = ((NPC.direction > 0) ? -1 : 1);
			NPC.chaseable = hasBeenHit;
			if (NPC.justHit)
			{
				hasBeenHit = true;
			}
			if (NPC.wet)
			{
				if (NPC.direction == 0)
				{
					NPC.TargetClosest(true);
				}
				NPC.noTileCollide = false;
				bool flag14 = hasBeenHit;
				NPC.TargetClosest(false);
				if (Main.player[NPC.target].wet && !Main.player[NPC.target].dead &&
					Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) &&
					(Main.player[NPC.target].Center - NPC.Center).Length() < 200f)
				{
					flag14 = true;
				}
				if (Main.player[NPC.target].dead && flag14)
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
					NPC.velocity.X = NPC.velocity.X + (float)NPC.direction * 0.3f;
					NPC.velocity.Y = NPC.velocity.Y + (float)NPC.directionY * 0.1f;
					if (NPC.velocity.X > 10f)
					{
						NPC.velocity.X = 10f;
					}
					if (NPC.velocity.X < -10f)
					{
						NPC.velocity.X = -10f;
					}
					if (NPC.velocity.Y > 5f)
					{
						NPC.velocity.Y = 5f;
					}
					if (NPC.velocity.Y < -5f)
					{
						NPC.velocity.Y = -5f;
					}
				}
				else
				{
					NPC.velocity.X = NPC.velocity.X + (float)NPC.direction * 0.1f;
					if (NPC.velocity.X < -1.5f || NPC.velocity.X > 1.5f)
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
				if (Collision.WetCollision(NPC.position, NPC.width, NPC.height))
				{
					NPC.noTileCollide = false;
					NPC.netUpdate = true;
					return;
				}
				NPC.noTileCollide = true;
				float num823 = 1f;
				NPC.TargetClosest(true);
				bool flag51 = false;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
				{
					num823 = 1.5f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
				{
					num823 = 2.5f;
				}
				if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < 20f)
				{
					flag51 = true;
				}
				if (flag51)
				{
					NPC.velocity.X = NPC.velocity.X * 0.9f;
					if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
					{
						NPC.velocity.X = 0f;
					}
				}
				else
				{
					if (NPC.direction > 0)
					{
						NPC.velocity.X = (NPC.velocity.X * 20f + num823) / 21f;
					}
					if (NPC.direction < 0)
					{
						NPC.velocity.X = (NPC.velocity.X * 20f - num823) / 21f;
					}
				}
				int num854 = 80;
				int num855 = 20;
				Vector2 position2 = new Vector2(NPC.Center.X - (float)(num854 / 2), NPC.position.Y + (float)NPC.height - (float)num855);
				bool flag52 = false;
				if (NPC.position.X < Main.player[NPC.target].position.X && NPC.position.X + (float)NPC.width > Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width && NPC.position.Y + (float)NPC.height < Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height - 16f)
				{
					flag52 = true;
				}
				if (flag52)
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.5f;
				}
				else if (Collision.SolidCollision(position2, num854, num855))
				{
					if (NPC.velocity.Y > 0f)
					{
						NPC.velocity.Y = 0f;
					}
					if ((double)NPC.velocity.Y > -0.2)
					{
						NPC.velocity.Y = NPC.velocity.Y - 0.025f;
					}
					else
					{
						NPC.velocity.Y = NPC.velocity.Y - 0.2f;
					}
					if (NPC.velocity.Y < -4f)
					{
						NPC.velocity.Y = -4f;
					}
				}
				else
				{
					if (NPC.velocity.Y < 0f)
					{
						NPC.velocity.Y = 0f;
					}
					if ((double)NPC.velocity.Y < 0.1)
					{
						NPC.velocity.Y = NPC.velocity.Y + 0.025f;
					}
					else
					{
						NPC.velocity.Y = NPC.velocity.Y + 0.5f;
					}
				}
				if (NPC.velocity.Y > 10f)
				{
					NPC.velocity.Y = 10f;
				}
			}
			NPC.rotation = NPC.velocity.Y * (float)NPC.direction * 0.05f;
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
			NPC.frameCounter += (hasBeenHit ? 1.25 : 1.0);
			if (NPC.frameCounter < 6.0)
			{
				NPC.frame.Y = 0;
			}
			else if (NPC.frameCounter < 12.0)
			{
				NPC.frame.Y = frameHeight;
			}
			else if (NPC.frameCounter < 18.0)
			{
				NPC.frame.Y = frameHeight * 2;
			}
			else if (NPC.frameCounter < 24.0)
			{
				NPC.frame.Y = frameHeight * 3;
			}
			else
			{
				NPC.frameCounter = 0.0;
				NPC.frame.Y = 0;
			}
			if (!NPC.wet)
			{
				NPC.frame.Y = NPC.frame.Y + frameHeight * 4;
			}
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Bleeding, 180, true);
			target.AddBuff(BuffID.Venom, 180, true);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe)
			{
				return 0f;
			}
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSulphur || (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSulphur && spawnInfo.Water))
			{
				return 0.1f;
			}
			return 0f;
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(ItemID.DivingHelmet, 25));
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("TrashmanTrashcan").Type, 25));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(),ItemID.Gatligator, 10));
		}

		public override void OnKill()
		{
			if (!NPC.savedAngler && !NPC.AnyNPCs(NPCID.Angler) && !NPC.AnyNPCs(NPCID.SleepingAngler) && Main.netMode != 1)
			{
				NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.Angler, 0, 0f, 0f, 0f, 0f, 255);
				NPC.savedAngler = true;
			}
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
					for (int k = 0; k < 25; k++)
					{
						Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color),
							1f);
					}

					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Trasher").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Trasher2").Type, 1f);
				}
			}
		}
	}
}