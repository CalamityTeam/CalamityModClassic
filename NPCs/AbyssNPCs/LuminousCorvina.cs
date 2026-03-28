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
	public class LuminousCorvina : ModNPC
	{
		public bool hasBeenHit = false;
		public int screamTimer = 0;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Luminous Corvina");
			Main.npcFrameCount[NPC.type] = 8;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("This creature's survival methods are unusual, yet clever: Once a potential threat is near it, it will release a shrill pulse, alerting all other creatures of a potential meal's location... While it remains unscathed.")
			});
		}

		public override void SetDefaults()
		{
			NPC.noGravity = true;
			NPC.damage = 10;
			NPC.width = 74;
			NPC.height = 56;
			NPC.defense = 20;
			NPC.lifeMax = 800;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.buffImmune[Mod.Find<ModBuff>("CrushDepth").Type] = true;
			NPC.value = Item.buyPrice(0, 0, 15, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.85f;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("LuminousCorvinaBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<AbyssLayer2Biome>().Type };
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
				if ((Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position,
					Main.player[NPC.target].width, Main.player[NPC.target].height) && ((NPC.Center.X - 15f < Main.player[NPC.target].Center.X &&
					NPC.direction == 1) || (NPC.Center.X + 15f > Main.player[NPC.target].Center.X && NPC.direction == -1))) ||
					(Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position,
					Main.player[NPC.target].width, Main.player[NPC.target].height) && flag14))
				{
					++screamTimer;

					if (screamTimer >= 300)
					{
						if (screamTimer == 300)
						{
							SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/CorvinaScream"), NPC.position);
							if (Main.netMode != 2)
							{
								if (!Main.player[NPC.target].dead && Main.player[NPC.target].active)
								{
									Main.player[NPC.target].AddBuff(Mod.Find<ModBuff>("FishAlert").Type, 360, true);
								}
							}
						}
						if (screamTimer >= 360)
						{
							screamTimer = 0;
						}
						return;
					}
				}
				if ((!Main.player[NPC.target].wet || Main.player[NPC.target].dead) && flag14)
				{
					flag14 = false;
				}
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
				if (NPC.velocity.Y > 10f)
				{
					NPC.velocity.Y = 10f;
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
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 6.0)
			{
				NPC.frameCounter = 0.0;
				NPC.frame.Y = NPC.frame.Y + frameHeight;
			}
			if (screamTimer <= 300)
			{
				if (NPC.frame.Y > frameHeight * 5)
				{
					NPC.frame.Y = 0;
				}
			}
			else
			{
				if (NPC.frame.Y < frameHeight * 6)
				{
					NPC.frame.Y = frameHeight * 6;
				}
				if (NPC.frame.Y > frameHeight * 7)
				{
					NPC.frame.Y = frameHeight * 6;
				}
			}

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
			vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/LuminousCorvinaGlow").Value.Width, (float)(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/LuminousCorvinaGlow").Value.Height / Main.npcFrameCount[NPC.type])) * 1f / 2f;
			vector += vector11 * 1f + new Vector2(0f, 0f + 4f + NPC.gfxOffY);
			Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.LightBlue);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/LuminousCorvinaGlow").Value, vector,
				new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, vector11, 1f, spriteEffects, 0f);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 180, true);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyssLayer2 && spawnInfo.Water)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.6f;
			}
			return 0f;
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule fakeCalorPlantDead = new LeadingConditionRule(new DownedCalDoppelorPlantera());
			
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("Voidstone").Type, 1, 8, 16));
			npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), Mod.Find<ModItem>("HalibutCannon").Type, 1000000));
			fakeCalorPlantDead.OnSuccess(new CommonDrop(Mod.Find<ModItem>("Lumenite").Type, 2));
			fakeCalorPlantDead.OnSuccess(new CommonDrop(Mod.Find<ModItem>("DepthCells").Type, 2, 1, 3));
			fakeCalorPlantDead.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("DepthCells").Type, 2));
			npcLoot.Add(fakeCalorPlantDead);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 139, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 25; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 139, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}