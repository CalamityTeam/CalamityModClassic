using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.Weapons.SunkenSea;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.SunkenSeaNPCs
{
	public class GiantClam : ModNPC
	{
		private int hitAmount = 0;
		private int attack = -1; //-1 doing nothing, 0 = shell hiding, 1 = telestomp, 2 = pearl burst, 3 = pearl rain
		private bool attackAnim = false;
		private bool hasBeenHit = false;
		private bool statChange = false;
		private bool hide = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Giant Clam");
			Main.npcFrameCount[NPC.type] = 12;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("An oversized clam large enough to trap a person within it. It might make a good shield, who knows?")
			});
		}

		public override void SetDefaults()
		{
			//npc.damage = Main.hardMode ? 100 : 50;
			NPC.npcSlots = 5f;
			NPC.damage = 50;
			NPC.width = 226;
			NPC.height = 136;
			//npc.defense = Main.hardMode ? 35 : 10;
			NPC.defense = 9999;
			NPC.lifeMax = Main.hardMode ? 7500 : 1250;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.value = Main.hardMode ? Item.buyPrice(0, 10, 0, 0) : Item.buyPrice(0, 1, 0, 0);
			NPC.HitSound = SoundID.NPCHit4;
			NPC.knockBackResist = 0f;
			NPC.rarity = 2;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("GiantClamBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<SunkenSea>().Type };
		}

		public override void AI()
		{
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];
			if (NPC.justHit && hitAmount < 5)
			{
				++hitAmount;
				hasBeenHit = true;
			}
			NPC.chaseable = hasBeenHit;
			if (hitAmount == 5)
			{
				if (Main.netMode != 2)
				{
					if (!Main.player[NPC.target].dead && Main.player[NPC.target].active)
					{
						player.AddBuff(Mod.Find<ModBuff>("Clamity").Type, 2); //CLAM INVASION
					}
				}

				if (!hide)
					Lighting.AddLight(NPC.Center, 0f, ((255 - NPC.alpha) * 2.5f) / 255f, ((255 - NPC.alpha) * 2.5f) / 255f);

				if (!statChange)
				{
					NPC.defense = 10;
					NPC.damage = Main.expertMode ? 100 : 50;
					if (Main.hardMode)
					{
						NPC.defense = 35;
						NPC.damage = Main.expertMode ? 200 : 100;
					}
					statChange = true;
				}

				if (NPC.ai[0] < 240f)
				{
					NPC.ai[0] += 1f;
					hide = false;
				}
				else
				{
					if (attack == -1)
					{
						attack = Main.rand.Next(2);
						if (attack == 0)
						{
							attack = Main.rand.Next(2); //rarer chance of doing the hiding clam
						}
					}
					else if (attack == 0)
					{
						hide = true;
						NPC.defense = 9999;
						NPC.ai[1] += 1f;
						if (NPC.ai[1] >= 90f)
						{
							NPC.ai[0] = 0f;
							NPC.ai[1] = 0f;
							hide = false;
							attack = -1;
							NPC.defense = Main.hardMode ? 35 : 10;
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)(NPC.Center.X + 5), (int)NPC.Center.Y, Mod.Find<ModNPC>("Clam").Type, 0, 0f, 0f, 0f, 0f, 255);
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)(NPC.Center.X), (int)NPC.Center.Y, Mod.Find<ModNPC>("Clam").Type, 0, 0f, 0f, 0f, 0f, 255);
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)(NPC.Center.X - 5), (int)NPC.Center.Y, Mod.Find<ModNPC>("Clam").Type, 0, 0f, 0f, 0f, 0f, 255);
						}
					}
					else if (attack == 1)
					{
						if (NPC.ai[2] == 0f)
						{
							if (Main.netMode != 1)
							{
								NPC.TargetClosest(true);
								NPC.ai[2] = 1f;
								NPC.netUpdate = true;
							}
						}
						else if (NPC.ai[2] == 1f)
						{
							NPC.damage = 0;
							NPC.chaseable = false;
							NPC.dontTakeDamage = true;
							NPC.noGravity = true;
							NPC.noTileCollide = true;
							NPC.alpha += Main.hardMode ? 8 : 5;
							if (NPC.alpha >= 255)
							{
								NPC.alpha = 255;
								NPC.position.X = player.Center.X - (float)(NPC.width / 2);
								NPC.position.Y = player.Center.Y - (float)(NPC.height / 2) + player.gfxOffY - 200f;
								NPC.position.X = NPC.position.X - 15f;
								NPC.position.Y = NPC.position.Y - 100f;
								NPC.ai[2] = 2f;
								NPC.netUpdate = true;
							}
						}
						else if (NPC.ai[2] == 2f)
						{
							if (Main.rand.Next(2) == 0)
							{
								int num5 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 226, 0f, 0f, 200, default(Color), 1.5f);
								Main.dust[num5].noGravity = true;
								Main.dust[num5].velocity *= 0.75f;
								Main.dust[num5].fadeIn = 1.3f;
								Vector2 vector = new Vector2((float)Main.rand.Next(-200, 201), (float)Main.rand.Next(-200, 201));
								vector.Normalize();
								vector *= (float)Main.rand.Next(100, 200) * 0.04f;
								Main.dust[num5].velocity = vector;
								vector.Normalize();
								vector *= 34f;
								Main.dust[num5].position = NPC.Center - vector;
							}
							NPC.alpha -= Main.hardMode ? 7 : 4;
							if (NPC.alpha <= 0)
							{
								NPC.damage = Main.expertMode ? 100 : 50;
								if (Main.hardMode)
								{
									NPC.damage = Main.expertMode ? 200 : 100;
								}
								NPC.chaseable = true;
								NPC.dontTakeDamage = false;
								NPC.alpha = 0;
								NPC.ai[2] = 3f;
								NPC.netUpdate = true;
							}
						}
						else if (NPC.ai[2] == 3f)
						{
							NPC.velocity.Y += 0.8f;
							attackAnim = true;
							if ((NPC.Center.Y) > (player.Center.Y - (float)(NPC.height / 2) + player.gfxOffY - 15f))
							{
								NPC.noTileCollide = false;
								NPC.ai[2] = 4f;
								NPC.netUpdate = true;
							}
						}
						else if (NPC.ai[2] == 4f)
						{
							if (NPC.velocity.Y == 0f)
							{
								NPC.ai[2] = 0f;
								NPC.ai[0] = 0f;
								NPC.netUpdate = true;
								NPC.noGravity = false;
								attack = -1;
								SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/ClamImpact"), NPC.position);
								for (int stompDustArea = (int)NPC.position.X - 30; stompDustArea < (int)NPC.position.X + NPC.width + 60; stompDustArea += 30)
								{
									for (int stompDustAmount = 0; stompDustAmount < 5; stompDustAmount++)
									{
										int stompDust = Dust.NewDust(new Vector2(NPC.position.X - 30f, NPC.position.Y + (float)NPC.height), NPC.width + 30, 4, 33, 0f, 0f, 100, default(Color), 1.5f);
										Main.dust[stompDust].velocity *= 0.2f;
									}
									int stompGore = Gore.NewGore(NPC.GetSource_FromThis(null), new Vector2((float)(stompDustArea - 30), NPC.position.Y + (float)NPC.height - 12f), default(Vector2), Main.rand.Next(61, 64), 1f);
									Main.gore[stompGore].velocity *= 0.4f;
								}
							}
							NPC.velocity.Y += 0.8f;
						}
					}
				}

				if (NPC.ai[3] < 180f && Main.hardMode)
				{
					NPC.ai[3] += 1f;
				}
				else if (Main.hardMode)
				{
					if (attack == -1)
					{
						attack = Main.rand.Next(2, 4);
					}
					else if (attack == 2)
					{
						SoundEngine.PlaySound(SoundID.Item67, NPC.position);
						Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float spread = 45f * 0.0174f;
						double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
						double deltaAngle = spread / 8f;
						double offsetAngle;
						int projectileShot = Mod.Find<ModProjectile>("PearlBurst").Type;
						int damage = Main.expertMode ? 50 : 75;
						float speed = 5f;
						Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
						float num6 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector.X + (float)Main.rand.Next(-20, 21);
						float num7 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector.Y + (float)Main.rand.Next(-20, 21);
						float num8 = (float)Math.Sqrt((double)(num6 * num6 + num7 * num7));
						num8 = speed / num8;
						num6 *= num8;
						num7 *= num8;
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, num6, num7, projectileShot, damage, 0f, Main.myPlayer, 0f, 0f);
						for (int i = 0; i < 4; i++)
						{
							offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)(Math.Sin(offsetAngle) * 3f), (float)(Math.Cos(offsetAngle) * 3f), projectileShot, damage, 0f, Main.myPlayer, 0f, 0f);
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)(-Math.Sin(offsetAngle) * 3f), (float)(-Math.Cos(offsetAngle) * 3f), projectileShot, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						attack = -1;
						NPC.ai[3] = 0f;
					}
					else if (attack == 3)
					{
						if (Main.netMode != 1)
						{
							SoundEngine.PlaySound(SoundID.Item68, NPC.position);
							int damage = Main.expertMode ? 30 : 50;
							float shotSpacing = 750f;
							for (int i = 0; i < 11; i++)
							{
								Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.Center.X + shotSpacing, player.Center.Y - 750f, 0f, 8f, Mod.Find<ModProjectile>("PearlRain").Type, damage, 0f, Main.myPlayer, 0f, 0f);
								shotSpacing -= 150f;
							}
						}
						attack = -1;
						NPC.ai[3] = 0f;
					}
				}
			}
		}

		public override bool CheckActive()
		{
			return Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 5600f;
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
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > (attackAnim ? 2.0 : 5.0))
			{
				NPC.frameCounter = 0.0;
				NPC.frame.Y = NPC.frame.Y + frameHeight;
			}
			if (hitAmount < 5 || hide)
			{
				NPC.frame.Y = frameHeight * 11;
			}
			else if (attackAnim)
			{
				if (NPC.frame.Y < frameHeight * 3)
				{
					NPC.frame.Y = frameHeight * 3;
				}
				if (NPC.frame.Y > frameHeight * 10)
				{
					hide = true;
					attackAnim = false;
				}
			}
			else
			{
				if (NPC.frame.Y > frameHeight * 3)
				{
					NPC.frame.Y = 0;
				}
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea && spawnInfo.Water && !NPC.AnyNPCs(Mod.Find<ModNPC>("GiantClam").Type))
            {
				return SpawnCondition.CaveJellyfish.Chance * 0.12f;
			}
			return 0f;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 37, hit.HitDirection, -1f, 0, default(Color), 1f);
			}

			if (NPC.life <= 0)
			{
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 37, hit.HitDirection, -1f, 0, default(Color), 1f);
				}

				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("GiantClam1").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("GiantClam2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("GiantClam3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("GiantClam4").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("GiantClam5").Type, 1f);
				}
			}
		}

		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y);
			Vector2 vector11 = new Vector2((float)(TextureAssets.Npc[NPC.type].Value.Width / 2), (float)(TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2));
			Vector2 vector = center - Main.screenPosition;
			vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/SunkenSeaNPCs/GiantClamGlow").Value.Width, (float)(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/SunkenSeaNPCs/GiantClamGlow").Value.Height / Main.npcFrameCount[NPC.type])) * 1f / 2f;
			vector += vector11 * 1f + new Vector2(0f, 0f + 4f + NPC.gfxOffY);
			Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.LightBlue);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/SunkenSeaNPCs/GiantClamGlow").Value, vector,
				new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, vector11, 1f, spriteEffects, 0f);
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			
			LeadingConditionRule isHardmode = new LeadingConditionRule(new Conditions.IsExpert());
			
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("Navystone").Type, 1, 25, 36));
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("GiantPearl").Type, 3));
			npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), Mod.Find<ModItem>("AmidiasPendant").Type, 2));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("MolluskHusk").Type, 1, 6, 12));
			isHardmode.OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				ModContent.ItemType<Poseidon>(),
				ModContent.ItemType<ClamCrusher>(),
				ModContent.ItemType<ClamorRifle>(),
				ModContent.ItemType<ShellfishStaff>(),
			}));
			npcLoot.Add(isHardmode);
		}

		public override void OnKill()
		{
			int seahoe = NPC.FindFirstNPC(Mod.Find<ModNPC>("SEAHOE").Type);
			if (seahoe == -1 && Main.netMode != 1 && CalamityWorldPreTrailer.downedDesertScourge)
			{
				NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("SEAHOE").Type, 0, 0f, 0f, 0f, 0f, 255);
			}
		}
	}
}