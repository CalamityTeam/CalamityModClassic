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
	public class Catfish : ModNPC
	{
		public bool hasBeenHit = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Catfish");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("These fish use their whiskers to find their way and catch prey.")
			});
		}

		public override void SetDefaults()
		{
			NPC.noGravity = true;
			NPC.damage = 30;
			NPC.width = 98;
			NPC.height = 40;
			NPC.defense = 12;
			NPC.lifeMax = 120;
			NPC.aiStyle = -1;
			AIType = -1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.value = Item.buyPrice(0, 0, 1, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath40;
			NPC.knockBackResist = 0.8f;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("CatfishBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<Sulphur>().Type };
		}

		public override void AI()
		{
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			NPC.noGravity = true;
			NPC.chaseable = hasBeenHit;
			if (NPC.direction == 0)
			{
				NPC.TargetClosest(true);
			}
			if (NPC.justHit)
			{
				hasBeenHit = true;
			}
			if (NPC.wet)
			{
				bool flag14 = hasBeenHit;
				NPC.TargetClosest(false);
				if (Main.player[NPC.target].wet && !Main.player[NPC.target].dead &&
					(Main.player[NPC.target].Center - NPC.Center).Length() < 200f &&
					Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
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
					NPC.velocity.X = NPC.velocity.X + (float)NPC.direction * 0.25f;
					NPC.velocity.Y = NPC.velocity.Y + (float)NPC.directionY * 0.15f;
					if (NPC.velocity.X > 8f)
					{
						NPC.velocity.X = 8f;
					}
					if (NPC.velocity.X < -8f)
					{
						NPC.velocity.X = -8f;
					}
					if (NPC.velocity.Y > 6f)
					{
						NPC.velocity.Y = 6f;
					}
					if (NPC.velocity.Y < -6f)
					{
						NPC.velocity.Y = -6f;
					}
				}
				else
				{
					NPC.velocity.X = NPC.velocity.X + (float)NPC.direction * 0.1f;
					if (NPC.velocity.X < -2.5f || NPC.velocity.X > 2.5f)
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
			if (!NPC.wet)
			{
				NPC.frameCounter = 0.0;
				return;
			}
			NPC.frameCounter += (hasBeenHit ? 0.15f : 0.075f);
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
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
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSulphur && spawnInfo.Water)
			{
				return 0.2f;
			}
			return 0f;
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(ItemID.DivingHelmet, 25));
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