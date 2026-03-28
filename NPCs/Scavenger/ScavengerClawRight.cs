using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.Scavenger
{
	public class ScavengerClawRight : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ravager");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}

		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 88;
			NPC.width = 80; //324
			NPC.height = 40; //216
			NPC.defense = 50;
			NPC.lifeMax = 11120;
			NPC.knockBackResist = 0f;
			AIType = -1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[BuffID.Daybreak] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("DemonFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Nightwither").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("WhisperingDeath").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("SilvaStun").Type] = false;
			NPC.noGravity = true;
			NPC.canGhostHeal = false;
			NPC.alpha = 255;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.HitSound = SoundID.NPCHit41;
			NPC.DeathSound = SoundID.NPCDeath14;
			if (CalamityWorldPreTrailer.downedProvidence)
			{
				NPC.damage = 200;
				NPC.defense = 120;
				NPC.lifeMax = 100000;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 300000 : 260000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
		}

		public override void AI()
		{
			if (!Main.npc[CalamityGlobalNPC.scavenger].active)
			{
				NPC.active = false;
				NPC.netUpdate = true;
				return;
			}
			if (NPC.timeLeft < 3000)
			{
				NPC.timeLeft = 3000;
			}
			if (NPC.alpha > 0)
			{
				NPC.alpha -= 10;
				if (NPC.alpha < 0)
				{
					NPC.alpha = 0;
				}
				NPC.ai[1] = 0f;
			}
			if (NPC.ai[0] == 0f)
			{
				NPC.noTileCollide = true;
				float num659 = 14f;
				if (NPC.life < NPC.lifeMax / 2)
				{
					num659 += 1f;
				}
				if (NPC.life < NPC.lifeMax / 3)
				{
					num659 += 1f;
				}
				if (NPC.life < NPC.lifeMax / 5)
				{
					num659 += 1f;
				}
				Vector2 vector79 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num660 = Main.npc[CalamityGlobalNPC.scavenger].Center.X - vector79.X;
				float num661 = Main.npc[CalamityGlobalNPC.scavenger].Center.Y - vector79.Y;
				num661 += 50f;
				num660 += 120f;
				float num662 = (float)Math.Sqrt((double)(num660 * num660 + num661 * num661));
				if (num662 < 12f + num659)
				{
					NPC.rotation = 0f;
					NPC.velocity.X = num660;
					NPC.velocity.Y = num661;
					NPC.ai[1] += 1f;
					if (NPC.life < NPC.lifeMax / 2)
					{
						NPC.ai[1] += 1f;
					}
					if (NPC.life < NPC.lifeMax / 3)
					{
						NPC.ai[1] += 1f;
					}
					if (NPC.life < NPC.lifeMax / 5)
					{
						NPC.ai[1] += 10f;
					}
					if (NPC.ai[1] >= 60f)
					{
						NPC.TargetClosest(true);
						if ((NPC.Center.X - 100f < Main.player[NPC.target].Center.X))
						{
							NPC.ai[1] = 0f;
							NPC.ai[0] = 1f;
							return;
						}
						NPC.ai[1] = 0f;
						return;
					}
				}
				else
				{
					num662 = num659 / num662;
					NPC.velocity.X = num660 * num662;
					NPC.velocity.Y = num661 * num662;
					NPC.rotation = (float)Math.Atan2((double)(-(double)NPC.velocity.Y), (double)(-(double)NPC.velocity.X));
				}
			}
			else if (NPC.ai[0] == 1f)
			{
				NPC.noTileCollide = true;
				NPC.collideX = false;
				NPC.collideY = false;
				float num663 = 12f;
				if (NPC.life < NPC.lifeMax / 2)
				{
					num663 += 4f;
				}
				if (NPC.life < NPC.lifeMax / 3)
				{
					num663 += 4f;
				}
				if (NPC.life < NPC.lifeMax / 5)
				{
					num663 += 10f;
				}
				Vector2 vector80 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num664 = Main.player[NPC.target].Center.X - vector80.X;
				float num665 = Main.player[NPC.target].Center.Y - vector80.Y;
				float num666 = (float)Math.Sqrt((double)(num664 * num664 + num665 * num665));
				num666 = num663 / num666;
				NPC.velocity.X = num664 * num666;
				NPC.velocity.Y = num665 * num666;
				NPC.ai[0] = 2f;
				NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
			}
			else if (NPC.ai[0] == 2f)
			{
				if (Math.Abs(NPC.velocity.X) > Math.Abs(NPC.velocity.Y))
				{
					if (NPC.velocity.X > 0f && NPC.Center.X > Main.player[NPC.target].Center.X)
					{
						NPC.noTileCollide = false;
					}
					if (NPC.velocity.X < 0f && NPC.Center.X < Main.player[NPC.target].Center.X)
					{
						NPC.noTileCollide = false;
					}
				}
				else
				{
					if (NPC.velocity.Y > 0f && NPC.Center.Y > Main.player[NPC.target].Center.Y)
					{
						NPC.noTileCollide = false;
					}
					if (NPC.velocity.Y < 0f && NPC.Center.Y < Main.player[NPC.target].Center.Y)
					{
						NPC.noTileCollide = false;
					}
				}
				Vector2 vector81 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num667 = Main.npc[CalamityGlobalNPC.scavenger].Center.X - vector81.X;
				float num668 = Main.npc[CalamityGlobalNPC.scavenger].Center.Y - vector81.Y;
				num667 += Main.npc[CalamityGlobalNPC.scavenger].velocity.X;
				num668 += Main.npc[CalamityGlobalNPC.scavenger].velocity.Y;
				num668 += 40f;
				num667 += 110f;
				float num669 = (float)Math.Sqrt((double)(num667 * num667 + num668 * num668));
				if ((num669 > 700f || NPC.collideX || NPC.collideY) | NPC.justHit)
				{
					NPC.noTileCollide = true;
					NPC.ai[0] = 0f;
					return;
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				NPC.noTileCollide = true;
				float num671 = 12f;
				float num672 = 0.4f;
				Vector2 vector82 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num673 = Main.player[NPC.target].Center.X - vector82.X;
				float num674 = Main.player[NPC.target].Center.Y - vector82.Y;
				float num675 = (float)Math.Sqrt((double)(num673 * num673 + num674 * num674));
				num675 = num671 / num675;
				num673 *= num675;
				num674 *= num675;
				if (NPC.velocity.X < num673)
				{
					NPC.velocity.X = NPC.velocity.X + num672;
					if (NPC.velocity.X < 0f && num673 > 0f)
					{
						NPC.velocity.X = NPC.velocity.X + num672 * 2f;
					}
				}
				else if (NPC.velocity.X > num673)
				{
					NPC.velocity.X = NPC.velocity.X - num672;
					if (NPC.velocity.X > 0f && num673 < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - num672 * 2f;
					}
				}
				if (NPC.velocity.Y < num674)
				{
					NPC.velocity.Y = NPC.velocity.Y + num672;
					if (NPC.velocity.Y < 0f && num674 > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + num672 * 2f;
					}
				}
				else if (NPC.velocity.Y > num674)
				{
					NPC.velocity.Y = NPC.velocity.Y - num672;
					if (NPC.velocity.Y > 0f && num674 < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y - num672 * 2f;
					}
				}
				NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y);
			float drawPositionX = Main.npc[CalamityGlobalNPC.scavenger].Center.X - center.X;
			float drawPositionY = Main.npc[CalamityGlobalNPC.scavenger].Center.Y - center.Y;
			drawPositionY += 30f;
			drawPositionX += 70f;
			float rotation = (float)Math.Atan2((double)drawPositionY, (double)drawPositionX) - 1.57f;
			bool draw = true;
			while (draw)
			{
				float totalDrawDistance = (float)Math.Sqrt((double)(drawPositionX * drawPositionX + drawPositionY * drawPositionY));
				if (totalDrawDistance < 16f)
				{
					draw = false;
				}
				else
				{
					totalDrawDistance = 16f / totalDrawDistance;
					drawPositionX *= totalDrawDistance;
					drawPositionY *= totalDrawDistance;
					center.X += drawPositionX;
					center.Y += drawPositionY;
					drawPositionX = Main.npc[CalamityGlobalNPC.scavenger].Center.X - center.X;
					drawPositionY = Main.npc[CalamityGlobalNPC.scavenger].Center.Y - center.Y;
					drawPositionY += 30f;
					drawPositionX += 70f;
					Microsoft.Xna.Framework.Color color = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16f));
					Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerChain").Value, new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
						new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerChain").Value.Width, ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerChain").Value.Height)), color, rotation,
						new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerChain").Value.Width * 0.5f, (float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerChain").Value.Height * 0.5f), 1f, SpriteEffects.None, 0f);
				}
			}
			return true;
		}

		public override bool PreKill()
		{
			return false;
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life > 0)
			{
				int num285 = 0;
				while ((double)num285 < hit.Damage / (double)NPC.lifeMax * 100.0)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, (float)hit.HitDirection, -1f, 0, default(Color), 1f);
					num285++;
				}
			}
			else
			{
				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("ScavengerClawRight").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("ScavengerClawRight2").Type, 1f);
				}
			}
		}
	}
}