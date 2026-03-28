using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.AstrumDeus
{
	public class AstrumDeusProbe3 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Astrum Deus Probe");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
	
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.npcSlots = 2f;
			NPC.damage = 0;
			NPC.width = 30; //324
			NPC.height = 30; //216
			NPC.defense = 30;
			NPC.lifeMax = 900;
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = 120000;
			}
			NPC.knockBackResist = 0.95f;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.buffImmune[24] = true;
		}

		public override void AI()
		{
			bool revenge = CalamityWorldPreTrailer.revenge;
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
			}
			float num = revenge ? 6f : 5f;
			float num2 = revenge ? 0.07f : 0.065f;
			if (CalamityWorldPreTrailer.death)
			{
				num = 8f;
				num2 = 0.08f;
			}
			Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float num4 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
			float num5 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
			num4 = (float)((int)(num4 / 8f) * 8);
			num5 = (float)((int)(num5 / 8f) * 8);
			vector.X = (float)((int)(vector.X / 8f) * 8);
			vector.Y = (float)((int)(vector.Y / 8f) * 8);
			num4 -= vector.X;
			num5 -= vector.Y;
			float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
			float num7 = num6;
			bool flag = false;
			if (num6 > 600f)
			{
				flag = true;
			}
			if (num6 == 0f)
			{
				num4 = NPC.velocity.X;
				num5 = NPC.velocity.Y;
			}
			else
			{
				num6 = num / num6;
				num4 *= num6;
				num5 *= num6;
			}
			if (num7 > 100f)
			{
				NPC.ai[0] += 1f;
				if (NPC.ai[0] > 0f)
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.023f;
				}
				else
				{
					NPC.velocity.Y = NPC.velocity.Y - 0.023f;
				}
				if (NPC.ai[0] < -100f || NPC.ai[0] > 100f)
				{
					NPC.velocity.X = NPC.velocity.X + 0.023f;
				}
				else
				{
					NPC.velocity.X = NPC.velocity.X - 0.023f;
				}
				if (NPC.ai[0] > 200f)
				{
					NPC.ai[0] = -200f;
				}
			}
			if (Main.player[NPC.target].dead)
			{
				num4 = (float)NPC.direction * num / 2f;
				num5 = -num / 2f;
			}
			if (NPC.velocity.X < num4)
			{
				NPC.velocity.X = NPC.velocity.X + num2;
			}
			else if (NPC.velocity.X > num4)
			{
				NPC.velocity.X = NPC.velocity.X - num2;
			}
			if (NPC.velocity.Y < num5)
			{
				NPC.velocity.Y = NPC.velocity.Y + num2;
			}
			else if (NPC.velocity.Y > num5)
			{
				NPC.velocity.Y = NPC.velocity.Y - num2;
			}
			NPC.localAI[0] += 1f;
			if (Main.netMode != 1 && NPC.localAI[0] >= 180f)
			{
				NPC.localAI[0] = 0f;
				if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					int num8 = 37;
					if (Main.expertMode)
					{
						num8 = 29;
					}
					int num9 = 84;
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector.X, vector.Y, num4, num5, num9, num8, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			int num10 = (int)NPC.position.X + NPC.width / 2;
			int num11 = (int)NPC.position.Y + NPC.height / 2;
			num10 /= 16;
			num11 /= 16;
			if (!WorldGen.SolidTile(num10, num11))
			{
				Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.3f, 0f, 0.25f);
			}
			if (num4 > 0f)
			{
				NPC.spriteDirection = 1;
				NPC.rotation = (float)Math.Atan2((double)num5, (double)num4);
			}
			if (num4 < 0f)
			{
				NPC.spriteDirection = -1;
				NPC.rotation = (float)Math.Atan2((double)num5, (double)num4) + 3.14f;
			}
			float num12 = 0.7f;
			if (NPC.collideX)
			{
				NPC.netUpdate = true;
				NPC.velocity.X = NPC.oldVelocity.X * -num12;
				if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
				{
					NPC.velocity.X = 2f;
				}
				if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
				{
					NPC.velocity.X = -2f;
				}
			}
			if (NPC.collideY)
			{
				NPC.netUpdate = true;
				NPC.velocity.Y = NPC.oldVelocity.Y * -num12;
				if (NPC.velocity.Y > 0f && (double)NPC.velocity.Y < 1.5)
				{
					NPC.velocity.Y = 2f;
				}
				if (NPC.velocity.Y < 0f && (double)NPC.velocity.Y > -1.5)
				{
					NPC.velocity.Y = -2f;
				}
			}
			if (flag)
			{
				if ((NPC.velocity.X > 0f && num4 > 0f) || (NPC.velocity.X < 0f && num4 < 0f))
				{
					if (Math.Abs(NPC.velocity.X) < 12f)
					{
						NPC.velocity.X = NPC.velocity.X * 1.05f;
					}
				}
				else
				{
					NPC.velocity.X = NPC.velocity.X * 0.9f;
				}
			}
			if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
			{
				NPC.netUpdate = true;
			}
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.soundDelay == 0)
			{
				NPC.soundDelay = 15;
				switch (Main.rand.Next(3))
				{
					case 0:
						SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCHit/AstralEnemyHit"), NPC.Center);
						break;
					case 1:
						SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCHit/AstralEnemyHit2"), NPC.Center);
						break;
					case 2:
						SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCHit/AstralEnemyHit3"), NPC.Center);
						break;
				}
			}

			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
					NPC.width = 30;
					NPC.height = 30;
					NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
					for (int num621 = 0; num621 < 5; num621++)
					{
						int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height,
							ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 2f);
						Main.dust[num622].velocity *= 3f;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num622].scale = 0.5f;
							Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}

					for (int num623 = 0; num623 < 10; num623++)
					{
						int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height,
							ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 3f);
						Main.dust[num624].noGravity = true;
						Main.dust[num624].velocity *= 5f;
						num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height,
							ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 2f);
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

						int num626 = Gore.NewGore(NPC.GetSource_FromThis(null),
							new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f,
								NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2),
							Main.rand.Next(61, 64), 1f);
						Main.gore[num626].velocity *= scaleFactor10;
						Gore expr_13AB6_cp_0 = Main.gore[num626];
						expr_13AB6_cp_0.velocity.X = expr_13AB6_cp_0.velocity.X + 1f;
						Gore expr_13AD6_cp_0 = Main.gore[num626];
						expr_13AD6_cp_0.velocity.Y = expr_13AD6_cp_0.velocity.Y + 1f;
						num626 = Gore.NewGore(NPC.GetSource_FromThis(null),
							new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f,
								NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2),
							Main.rand.Next(61, 64), 1f);
						Main.gore[num626].velocity *= scaleFactor10;
						Gore expr_13B79_cp_0 = Main.gore[num626];
						expr_13B79_cp_0.velocity.X = expr_13B79_cp_0.velocity.X - 1f;
						Gore expr_13B99_cp_0 = Main.gore[num626];
						expr_13B99_cp_0.velocity.Y = expr_13B99_cp_0.velocity.Y + 1f;
						num626 = Gore.NewGore(NPC.GetSource_FromThis(null),
							new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f,
								NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2),
							Main.rand.Next(61, 64), 1f);
						Main.gore[num626].velocity *= scaleFactor10;
						Gore expr_13C3C_cp_0 = Main.gore[num626];
						expr_13C3C_cp_0.velocity.X = expr_13C3C_cp_0.velocity.X + 1f;
						Gore expr_13C5C_cp_0 = Main.gore[num626];
						expr_13C5C_cp_0.velocity.Y = expr_13C5C_cp_0.velocity.Y - 1f;
						num626 = Gore.NewGore(NPC.GetSource_FromThis(null),
							new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f,
								NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2),
							Main.rand.Next(61, 64), 1f);
						Main.gore[num626].velocity *= scaleFactor10;
						Gore expr_13CFF_cp_0 = Main.gore[num626];
						expr_13CFF_cp_0.velocity.X = expr_13CFF_cp_0.velocity.X - 1f;
						Gore expr_13D1F_cp_0 = Main.gore[num626];
						expr_13D1F_cp_0.velocity.Y = expr_13D1F_cp_0.velocity.Y - 1f;
					}
				}
			}
		}

		public override bool PreKill()
		{
			return false;
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 90, true);
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("MarkedforDeath").Type, 120);
			}
		}
	}
}