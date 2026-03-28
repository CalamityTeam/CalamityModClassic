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
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.AbyssNPCs
{
	public class BoxJellyfish : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Box Jellyfish");
			Main.npcFrameCount[NPC.type] = 5;
		}

		public override void SetDefaults()
		{
			NPC.noGravity = true;
			NPC.damage = 22;
			NPC.width = 30;
			NPC.height = 30;
			NPC.defense = 5;
			NPC.lifeMax = 45;
			NPC.alpha = 20;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.buffImmune[Mod.Find<ModBuff>("CrushDepth").Type] = true;
			NPC.value = Item.buyPrice(0, 0, 0, 80);
			NPC.HitSound = SoundID.NPCHit25;
			NPC.DeathSound = SoundID.NPCDeath28;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("BoxJellyfishBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<AbyssLayer1Biome>().Type };
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("These jellyfish unique for their cube-like shape act like ambush predators, waiting for something edible to approach them to then hastily swim towards prey in an effort to consume it.")
			});
		}

		public override void AI()
		{
			if (NPC.direction == 0)
			{
				NPC.TargetClosest(true);
			}
			if (!NPC.wet)
			{
				NPC.rotation += NPC.velocity.X * 0.1f;
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
			bool flag16 = false;
			NPC.TargetClosest(false);
			if (Main.player[NPC.target].wet && !Main.player[NPC.target].dead)
			{
				flag16 = true;
			}
			if (flag16)
			{
				NPC.localAI[2] = 1f;
				NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
				NPC.velocity *= 0.975f;
				float num263 = 0.8f;
				if (NPC.velocity.X > -num263 && NPC.velocity.X < num263 && NPC.velocity.Y > -num263 && NPC.velocity.Y < num263)
				{
					NPC.TargetClosest(true);
					float num264 = 8f;
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
				NPC.rotation = NPC.velocity.X * 0.4f;
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

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.15f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
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

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Venom, 120, true);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 15; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}