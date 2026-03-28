using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using CalamityModClassicPreTrailer.Items;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class Cnidrion : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cnidrion");
			Main.npcFrameCount[NPC.type] = 10;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert,
				new FlavorTextBestiaryInfoElement("Enigmatic creatures from the desert that despite it, store massive amounts of water within their bodies... perhaps there's something more to them?")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 3f;
			NPC.aiStyle = -1;
			NPC.damage = 20;
			NPC.width = 160; //324
			NPC.height = 80; //216
			NPC.defense = 10;
			NPC.lifeMax = 400;
			NPC.knockBackResist = 0.05f;
			AIType = -1;
			NPC.value = Item.buyPrice(0, 0, 25, 0);
			NPC.HitSound = SoundID.NPCHit12;
			NPC.DeathSound = SoundID.NPCDeath18;
			NPC.rarity = 2;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("CnidrionBanner").Type;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea)
			{
				return 0f;
			}
			return SpawnCondition.DesertCave.Chance * 0.0175f;
		}
		
		public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.1f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override void AI()
		{
			Player player = Main.player[NPC.target];
			bool expertMode = Main.expertMode;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			float num823 = 1f;
			NPC.TargetClosest(true);
			bool flag51 = false;
			if ((double)NPC.life < (double)NPC.lifeMax * 0.33) 
			{
				num823 = 2f;
			}
			if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
			{
				num823 = 4f;
			}
			if (NPC.ai[0] == 0f)
			{
				NPC.ai[1] += 1f;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.33) 
				{
					NPC.ai[1] += 1f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
				{
					NPC.ai[1] += 1f;
				}
				if (NPC.ai[1] >= 300f && Main.netMode != 1) 
				{
					NPC.ai[1] = 0f;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
					{
						NPC.ai[0] = (float)Main.rand.Next(3, 5);
					} 
					else
					{
						NPC.ai[0] = (float)Main.rand.Next(1, 3);
					}
					NPC.netUpdate = true;
				}
			}
			else if (NPC.ai[0] == 1f)
			{
				flag51 = true;
				NPC.ai[1] += 1f;
				if (NPC.ai[1] % 15f == 0f) 
				{
					Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + 20f);
					vector18.X += (float)(10 * NPC.direction);
					float num829 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector18.X;
					float num830 = Main.player[NPC.target].position.Y - vector18.Y;
					float num831 = (float)Math.Sqrt((double)(num829 * num829 + num830 * num830));
					float num832 = 6f;
					num831 = num832 / num831;
					num829 *= num831;
					num830 *= num831;
					num829 *= 1f + (float)Main.rand.Next(-50, 51) * 0.01f;
					num830 *= 1f + (float)Main.rand.Next(-50, 51) * 0.01f;
					int num833 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector18.X, vector18.Y, num829, num830, Mod.Find<ModProjectile>("HorsWaterBlast").Type, (expertMode ? 6 : 9), 0f, Main.myPlayer, 0f, 0f);
				}
				if (NPC.ai[1] >= 120f) 
				{
					NPC.ai[1] = 0f;
					NPC.ai[0] = 0f;
				}
			}
			else if (NPC.ai[0] == 2f)
			{
				flag51 = true;
				NPC.ai[1] += 1f;
				if (NPC.ai[1] > 60f && NPC.ai[1] < 240f && NPC.ai[1] % 8f == 0f) 
				{
					Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + 20f);
					vector18.X += (float)(10 * NPC.direction);
					float num829 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector18.X;
					float num830 = Main.player[NPC.target].position.Y - vector18.Y;
					float num831 = (float)Math.Sqrt((double)(num829 * num829 + num830 * num830));
					float num832 = 8f;
					num831 = num832 / num831;
					num829 *= num831;
					num830 *= num831;
					num829 *= 1f + (float)Main.rand.Next(-30, 31) * 0.01f;
					num830 *= 1f + (float)Main.rand.Next(-30, 31) * 0.01f;
					int num833 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector18.X, vector18.Y, num829, num830, Mod.Find<ModProjectile>("HorsWaterBlast").Type, (expertMode ? 7 : 10), 0f, Main.myPlayer, 0f, 0f);
				}
				if (NPC.ai[1] >= 300f) 
				{
					NPC.ai[1] = 0f;
					NPC.ai[0] = 0f;
				}
			} 
			else if (NPC.ai[0] == 3f)
			{
				num823 = 4f;
				NPC.ai[1] += 1f;
				if (NPC.ai[1] % 30f == 0f) 
				{
					Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + 20f);
					vector18.X += (float)(10 * NPC.direction);
					float num844 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector18.X;
					float num845 = Main.player[NPC.target].position.Y - vector18.Y;
					float num846 = (float)Math.Sqrt((double)(num844 * num844 + num845 * num845));
					float num847 = 10f;
					num846 = num847 / num846;
					num844 *= num846;
					num845 *= num846;
					num844 *= 1f + (float)Main.rand.Next(-20, 21) * 0.001f;
					num845 *= 1f + (float)Main.rand.Next(-20, 21) * 0.001f;
					int num848 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector18.X, vector18.Y, num844, num845, Mod.Find<ModProjectile>("HorsWaterBlast").Type, (expertMode ? 9 : 12), 0f, Main.myPlayer, 0f, 0f);
				}
				if (NPC.ai[1] >= 120f) 
				{
					NPC.ai[1] = 0f;
					NPC.ai[0] = 0f;
				}
			} 
			else if (NPC.ai[0] == 4f)
			{
				num823 = 4f;
				NPC.ai[1] += 1f;
				if (NPC.ai[1] % 10f == 0f) 
				{
					Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + 20f);
					vector18.X += (float)(10 * NPC.direction);
					float num829 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector18.X;
					float num830 = Main.player[NPC.target].position.Y - vector18.Y;
					float num831 = (float)Math.Sqrt((double)(num829 * num829 + num830 * num830));
					float num832 = 11f;
					num831 = num832 / num831;
					num829 *= num831;
					num830 *= num831;
					num829 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
					num830 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
					int num833 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector18.X, vector18.Y, num829, num830, Mod.Find<ModProjectile>("HorsWaterBlast").Type, (expertMode ? 11 : 15), 0f, Main.myPlayer, 0f, 0f);
				}
				if (NPC.ai[1] >= 240f) 
				{
					NPC.ai[1] = 0f;
					NPC.ai[0] = 0f;
				}
			}
			if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < 50f) 
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
				return;
			}
			float num116 = (float)Main.rand.Next(30, 900);
			num116 *= (float)NPC.life / (float)NPC.lifeMax;
			num116 += 30f;
			if (Main.netMode != 1 && NPC.ai[2] >= num116 && NPC.velocity.Y == 0f && !player.dead && ((NPC.direction > 0 && NPC.Center.X < player.Center.X) || (NPC.direction < 0 && NPC.Center.X > player.Center.X)) && Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
			{
				float num117 = 13f;
				Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + 20f);
				vector18.X += (float)(10 * NPC.direction);
				float num118 = player.position.X + (float)player.width * 0.5f - vector18.X;
				float num119 = player.position.Y + (float)player.height * 0.5f - vector18.Y;
				num118 += (float)Main.rand.Next(-40, 41);
				num119 += (float)Main.rand.Next(-40, 41);
				float num120 = (float)Math.Sqrt((double)(num118 * num118 + num119 * num119));
				NPC.netUpdate = true;
				num120 = num117 / num120;
				num118 *= num120;
				num119 *= num120;
				int num121 = expertMode ? 8 : 12;
				int num122 = Mod.Find<ModProjectile>("HorsWaterBlast").Type;
				vector18.X += num118 * 3f;
				vector18.Y += num119 * 3f;
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector18.X, vector18.Y, num118, num119, num122, num121, 0f, Main.myPlayer, 0f, 0f);
				NPC.ai[2] = 0f;
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
				for (int k = 0; k < 40; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 2f);
				}
			}
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(ItemID.Coral, 1, 1, 4));
			npcLoot.Add(new CommonDrop(ItemID.Starfish, 1, 1, 4));
			npcLoot.Add(new CommonDrop(ItemID.Seashell, 1, 1, 4));
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("VictoryShard").Type, 1, 1, 4));
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("TheTransformer").Type, 100));
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("AmidiasSpark").Type, 4));
		}
	}
}