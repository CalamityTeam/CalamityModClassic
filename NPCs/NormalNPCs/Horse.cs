using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class Horse : ModNPC
	{
		int chargetimer = 0;
		int basespeed = 1;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Earth Elemental");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
				new FlavorTextBestiaryInfoElement("A man-made elemental run with clockwork mechanisms, records say its previous design resembled a horse.")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 3f;
			NPC.damage = 50;
			NPC.width = 230;
			NPC.height = 230;
			NPC.defense = 30;
			NPC.lifeMax = 3800;
			NPC.aiStyle = -1;
			AIType = -1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 1, 50, 0);
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.rarity = 2;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("EarthElementalBanner").Type;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || !Main.hardMode || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyss ||
				spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea)
			{
				return 0f;
			}
			return SpawnCondition.Cavern.Chance * 0.005f;
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter++;
			if (NPC.frameCounter >= 8)
			{
				NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
				NPC.frameCounter = 0;
			}
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("AridArtifact").Type, 3));
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("SlagMagnum").Type, 4));
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("Aftershock").Type, 4));
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("EarthenPike").Type, 4));
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 31, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				SoundEngine.PlaySound(SoundID.Item14, NPC.position);
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 160;
				NPC.height = 160;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 31, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 6, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
				for (int num625 = 0; num625 < 3; num625++)
				{
					float scaleFactor10 = 0.33f;
					if (num625 == 1)
					{
						scaleFactor10 = 0.66f;
					}
					if (num625 == 2)
					{
						scaleFactor10 = 1f;
					}
					int num626 = Gore.NewGore(NPC.GetSource_FromThis(null), new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num626].velocity *= scaleFactor10;
					Gore expr_13AB6_cp_0 = Main.gore[num626];
					expr_13AB6_cp_0.velocity.X = expr_13AB6_cp_0.velocity.X + 1f;
					Gore expr_13AD6_cp_0 = Main.gore[num626];
					expr_13AD6_cp_0.velocity.Y = expr_13AD6_cp_0.velocity.Y + 1f;
					num626 = Gore.NewGore(NPC.GetSource_FromThis(null), new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num626].velocity *= scaleFactor10;
					Gore expr_13B79_cp_0 = Main.gore[num626];
					expr_13B79_cp_0.velocity.X = expr_13B79_cp_0.velocity.X - 1f;
					Gore expr_13B99_cp_0 = Main.gore[num626];
					expr_13B99_cp_0.velocity.Y = expr_13B99_cp_0.velocity.Y + 1f;
					num626 = Gore.NewGore(NPC.GetSource_FromThis(null), new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num626].velocity *= scaleFactor10;
					Gore expr_13C3C_cp_0 = Main.gore[num626];
					expr_13C3C_cp_0.velocity.X = expr_13C3C_cp_0.velocity.X + 1f;
					Gore expr_13C5C_cp_0 = Main.gore[num626];
					expr_13C5C_cp_0.velocity.Y = expr_13C5C_cp_0.velocity.Y - 1f;
					num626 = Gore.NewGore(NPC.GetSource_FromThis(null), new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num626].velocity *= scaleFactor10;
					Gore expr_13CFF_cp_0 = Main.gore[num626];
					expr_13CFF_cp_0.velocity.X = expr_13CFF_cp_0.velocity.X - 1f;
					Gore expr_13D1F_cp_0 = Main.gore[num626];
					expr_13D1F_cp_0.velocity.Y = expr_13D1F_cp_0.velocity.Y - 1f;
				}
			}
		}

		public override bool PreAI()
		{
			if (Main.netMode != 1)
			{
				NPC.localAI[0] += 1f;
				if (NPC.localAI[0] >= 300f)
				{
					NPC.localAI[0] = 0f;
					SoundEngine.PlaySound(SoundID.NPCHit41, new Vector2((NPC.position.X + (float)(NPC.width / 2)), (NPC.position.Y + (float)(NPC.height / 2))));
					NPC.TargetClosest(true);
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						float num179 = 4f;
						Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
						float num181 = Math.Abs(num180) * 0.1f;
						float num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y - num181;
						float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
						NPC.netUpdate = true;
						num183 = num179 / num183;
						num180 *= num183;
						num182 *= num183;
						int num184 = 30;
						int num185 = Mod.Find<ModProjectile>("EarthRockSmall").Type;
						value9.X += num180;
						value9.Y += num182;
						for (int num186 = 0; num186 < 4; num186++)
						{
							num185 = (Main.rand.Next(4) == 0 ? Mod.Find<ModProjectile>("EarthRockBig").Type : Mod.Find<ModProjectile>("EarthRockSmall").Type);
							num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
							num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y;
							num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							num183 = num179 / num183;
							num180 += (float)Main.rand.Next(-40, 41);
							num182 += (float)Main.rand.Next(-40, 41);
							num180 *= num183;
							num182 *= num183;
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
						}
					}
				}
			}
			if ((double)Math.Abs(NPC.velocity.X) > 0.2)
			{
				NPC.spriteDirection = NPC.direction;
			}
			bool expertMode = Main.expertMode;
			NPC.TargetClosest(true);
			Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
			direction.Normalize();
			chargetimer += expertMode ? 2 : 1;
			if (chargetimer >= Math.Sqrt(NPC.life) * 14.0)
			{
				if (Main.rand.Next(25) == 1)
				{
					direction.X = direction.X * 6f;
					direction.Y = direction.Y * 6f;
					NPC.velocity = direction;
					chargetimer = 0;
				}
			}
			if (Math.Sqrt((NPC.velocity.X * NPC.velocity.X) + (NPC.velocity.Y * NPC.velocity.Y)) > basespeed)
			{
				NPC.velocity *= 0.985f;
			}
			if (Math.Sqrt((NPC.velocity.X * NPC.velocity.X) + (NPC.velocity.Y * NPC.velocity.Y)) <= basespeed * 1.15)
			{
				NPC.velocity = direction * basespeed;
			}
			return false;

		}
	}
}