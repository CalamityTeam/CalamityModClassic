using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2;
using CalamityModClassic1Point2.NPCs.TheDevourerofGods;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Projectiles
{
	public class CalamityGlobalProjectile1Point2 : GlobalProjectile
	{
		public static float counter = 0;
		
		public override void AI(Projectile projectile)
		{
			if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().eQuiver && projectile.CountsAsClass(DamageClass.Ranged))
			{
				if (Main.rand.Next(150) > 148)
				{
					float spread = 180f * 0.0174f;
					double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread/2;
					double deltaAngle = spread/8f;
					double offsetAngle;
					int i;
					for (i = 0; i < 1; i++)
					{
					   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
					   	if (projectile.owner == Main.myPlayer)
					   	{
						   	int projectile1 = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, (float)( Math.Sin(offsetAngle) * 8f ), (float)( Math.Cos(offsetAngle) * 8f ), projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
						    int projectile2 = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * 8f ), (float)( -Math.Cos(offsetAngle) * 8f ), projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
						    Main.projectile[projectile1].DamageType = DamageClass.Default/* tModPorter Suggestion: Remove. See Item.DamageType */;
						    Main.projectile[projectile2].DamageType = DamageClass.Default/* tModPorter Suggestion: Remove. See Item.DamageType */;
					   	}
					}
				}
			}
			if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().nanotech && projectile.CountsAsClass(DamageClass.Throwing))
			{
				counter += 1f;
				if (counter >= 30f)
				{
					counter = 0f;
					if (projectile.owner == Main.myPlayer)
					{
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("Nanotech").Type, 300, 0f, projectile.owner, 0f, 0f);
					}
				}
			}
		}
		
		public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
		{
			if (projectile.owner == Main.myPlayer && Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().beeResist)
			{
				if (!projectile.friendly && projectile.hostile && projectile.damage > 0 && 
					(projectile.type == ProjectileID.Stinger || projectile.type == ProjectileID.HornetStinger || projectile.type == Mod.Find<ModProjectile>("PlagueStingerGoliath").Type ||
					projectile.type == Mod.Find<ModProjectile>("HiveBombGoliath").Type || projectile.type == Mod.Find<ModProjectile>("HiveBombExplosionHostile").Type || projectile.type == Mod.Find<ModProjectile>("PlagueStingerGoliathV2").Type ||
					projectile.type == Mod.Find<ModProjectile>("PlagueExplosion").Type))
				{
					modifiers.FinalDamage *= 0.5f;
				}
			}
		}
		
		public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
		{
			if (projectile.owner == Main.myPlayer && Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().projRef && projectile.active && !projectile.friendly && projectile.hostile && info.Damage > 0 && Main.rand.NextBool(4))
			{
				target.statLife += info.Damage;
    			target.HealEffect(info.Damage);
				projectile.hostile = false;
				projectile.friendly = true;
				projectile.velocity.X = -projectile.velocity.X;
				projectile.velocity.Y = -projectile.velocity.Y;
			}
			if (projectile.owner == Main.myPlayer && Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().daedalusReflect && projectile.active && !projectile.friendly && projectile.hostile && info.Damage > 0 && Main.rand.NextBool(2))
			{
				int healAmt = info.Damage / 5;
				target.statLife += healAmt;
    			target.HealEffect(healAmt);
				projectile.hostile = false;
				projectile.friendly = true;
				projectile.velocity.X = -projectile.velocity.X;
				projectile.velocity.Y = -projectile.velocity.Y;
			}
		}
		
		public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
		{
			if (projectile.owner == Main.myPlayer && Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().uberBees && (projectile.type == 566 || projectile.type == 181 || projectile.type == 189))
			{
				modifiers.FinalDamage.Base = modifiers.FinalDamage.Base + Main.rand.Next(70, 101);
				projectile.penetrate = 1;
			}
		}
		
		public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().astralStarRain && hit.Crit)
            {
                if (projectile.owner == Main.myPlayer)
                {
                    for (int n = 0; n < 3; n++)
                    {
                        float x = target.position.X + (float)Main.rand.Next(-400, 400);
                        float y = target.position.Y - (float)Main.rand.Next(500, 800);
                        Vector2 vector = new Vector2(x, y);
                        float num13 = target.position.X + (float)(target.width / 2) - vector.X;
                        float num14 = target.position.Y + (float)(target.height / 2) - vector.Y;
                        num13 += (float)Main.rand.Next(-100, 101);
                        int num15 = 25;
                        int projectileType = Main.rand.Next(3);
                        if (projectileType == 0)
                        {
                            projectileType = Mod.Find<ModProjectile>("AstralStar").Type;
                        }
                        else if (projectileType == 1)
                        {
                            projectileType = 92;
                        }
                        else
                        {
                            projectileType = 12;
                        }
                        float num16 = (float)Math.Sqrt((double)(num13 * num13 + num14 * num14));
                        num16 = (float)num15 / num16;
                        num13 *= num16;
                        num14 *= num16;
                        int num17 = Projectile.NewProjectile(projectile.GetSource_FromThis(), x, y, num13, num14, projectileType, 85, 5f, projectile.owner, 0f, 0f);
                        Main.projectile[num17].DamageType = DamageClass.Default/* tModPorter Suggestion: Remove. See Item.DamageType */;
                    }
                }
            }
            if (projectile.owner == Main.myPlayer)
			{
				if (target.type == NPCID.TargetDummy)
				{
					return;
				}
				if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().frostFlare)
				{
					target.AddBuff(BuffID.Frostburn, 360);
				}
				if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().uberBees && (projectile.type == 566 || projectile.type == 181 || projectile.type == 189))
				{
					target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 360);
				}
				if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().alchFlask && (projectile.CountsAsClass(DamageClass.Magic) || projectile.CountsAsClass(DamageClass.Throwing) || projectile.CountsAsClass(DamageClass.Melee) || projectile.minion || projectile.CountsAsClass(DamageClass.Ranged)))
				{
					target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 120);
					int plague = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("PlagueSeeker").Type, 22, 0f, projectile.owner, 0f, 0f);
					Main.projectile[plague].DamageType = DamageClass.Default/* tModPorter Suggestion: Remove. See Item.DamageType */;
				}
				if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().reaverBlast && (projectile.CountsAsClass(DamageClass.Magic) || projectile.CountsAsClass(DamageClass.Throwing) || projectile.CountsAsClass(DamageClass.Melee) || projectile.minion || projectile.CountsAsClass(DamageClass.Ranged)))
				{
					Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ReaverBlast").Type, 17, 0f, projectile.owner, 0f, 0f);
				}
				if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().auricSet)
				{
					float num11 = 0.1f;
					num11 -= (float)projectile.numHits * 0.025f;
					if (num11 <= 0f)
					{
						return;
					}
					float num12 = (float)projectile.damage * num11;
					if ((int)num12 <= 0)
					{
						return;
					}
					if (Main.player[Main.myPlayer].lifeSteal <= 0f)
					{
						return;
					}
					Main.player[Main.myPlayer].lifeSteal -= num12;
					float num13 = 0f;
					int num14 = projectile.owner;
					for (int i = 0; i < 255; i++)
					{
						if (Main.player[i].active && !Main.player[i].dead && ((!Main.player[projectile.owner].hostile && !Main.player[i].hostile) || Main.player[projectile.owner].team == Main.player[i].team))
						{
							float num15 = Math.Abs(Main.player[i].position.X + (float)(Main.player[i].width / 2) - projectile.position.X + (float)(projectile.width / 2)) + Math.Abs(Main.player[i].position.Y + (float)(Main.player[i].height / 2) - projectile.position.Y + (float)(projectile.height / 2));
							if (num15 < 1200f && (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife) > num13)
							{
								num13 = (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife);
								num14 = i;
							}
						}
					}
					Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("AuricOrb").Type, 0, 0f, projectile.owner, (float)num14, num12);
				}
				if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().silvaSet)
				{
					float num11 = 0.05f;
					num11 -= (float)projectile.numHits * 0.025f;
					if (num11 <= 0f)
					{
						return;
					}
					float num12 = (float)projectile.damage * num11;
					if ((int)num12 <= 0)
					{
						return;
					}
					if (Main.player[Main.myPlayer].lifeSteal <= 0f)
					{
						return;
					}
					Main.player[Main.myPlayer].lifeSteal -= num12;
					float num13 = 0f;
					int num14 = projectile.owner;
					for (int i = 0; i < 255; i++)
					{
						if (Main.player[i].active && !Main.player[i].dead && ((!Main.player[projectile.owner].hostile && !Main.player[i].hostile) || Main.player[projectile.owner].team == Main.player[i].team))
						{
							float num15 = Math.Abs(Main.player[i].position.X + (float)(Main.player[i].width / 2) - projectile.position.X + (float)(projectile.width / 2)) + Math.Abs(Main.player[i].position.Y + (float)(Main.player[i].height / 2) - projectile.position.Y + (float)(projectile.height / 2));
							if (num15 < 1200f && (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife) > num13)
							{
								num13 = (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife);
								num14 = i;
							}
						}
					}
					Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("SilvaOrb").Type, 0, 0f, projectile.owner, (float)num14, num12);
				}
				if (projectile.CountsAsClass(DamageClass.Magic))
				{
					if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().reaverBurst)
					{
						int num251 = Main.rand.Next(2, 5);
						for (int num252 = 0; num252 < num251; num252++)
						{
							Vector2 value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
							while (value15.X == 0f && value15.Y == 0f)
							{
								value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
							}
							value15.Normalize();
							value15 *= (float)Main.rand.Next(70, 101) * 0.1f;
							Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.oldPosition.X + (float)(projectile.width / 2), projectile.oldPosition.Y + (float)(projectile.height / 2), value15.X, value15.Y, 569 + Main.rand.Next(3), (int)((double)projectile.damage * 0.5f), 0f, projectile.owner, 0f, 0f);
						}
					}
					else if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().ataxiaMage)
					{
						int num = projectile.damage / 2;
						Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().ataxiaDmg += (float)num;
						int[] array = new int[200];
						int num3 = 0;
						int num4 = 0;
						for (int i = 0; i < 200; i++)
						{
							if (Main.npc[i].CanBeChasedBy(projectile, false))
							{
								float num5 = Math.Abs(Main.npc[i].position.X + (float)(Main.npc[i].width / 2) - projectile.position.X + (float)(projectile.width / 2)) + Math.Abs(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2) - projectile.position.Y + (float)(projectile.height / 2));
								if (num5 < 800f)
								{
									if (Collision.CanHit(projectile.position, 1, 1, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height) && num5 > 50f)
									{
										array[num4] = i;
										num4++;
									}
									else if (num4 == 0)
									{
										array[num3] = i;
										num3++;
									}
								}
							}
						}
						if (num3 == 0 && num4 == 0)
						{
							return;
						}
						int num6;
						if (num4 > 0)
						{
							num6 = array[Main.rand.Next(num4)];
						}
						else
						{
							num6 = array[Main.rand.Next(num3)];
						}
						float num7 = 20f;
						float num8 = (float)Main.rand.Next(-100, 101);
						float num9 = (float)Main.rand.Next(-100, 101);
						float num10 = (float)Math.Sqrt((double)(num8 * num8 + num9 * num9));
						num10 = num7 / num10;
						num8 *= num10;
						num9 *= num10;
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, num8, num9, Mod.Find<ModProjectile>("AtaxiaOrb").Type, num, 0f, projectile.owner, (float)num6, 0f);
						float num11 = 0.04f; //0.2
						num11 -= (float)projectile.numHits * 0.02f; //0.05
						if (num11 <= 0f)
						{
							return;
						}
						float num12 = (float)projectile.damage * num11;
						if ((int)num12 <= 0)
						{
							return;
						}
						if (Main.player[Main.myPlayer].lifeSteal <= 0f)
						{
							return;
						}
						Main.player[Main.myPlayer].lifeSteal -= num12;
						float num13 = 0f;
						int num14 = projectile.owner;
						for (int i = 0; i < 255; i++)
						{
							if (Main.player[i].active && !Main.player[i].dead && ((!Main.player[projectile.owner].hostile && !Main.player[i].hostile) || Main.player[projectile.owner].team == Main.player[i].team))
							{
								float num15 = Math.Abs(Main.player[i].position.X + (float)(Main.player[i].width / 2) - projectile.position.X + (float)(projectile.width / 2)) + Math.Abs(Main.player[i].position.Y + (float)(Main.player[i].height / 2) - projectile.position.Y + (float)(projectile.height / 2));
								if (num15 < 1200f && (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife) > num13)
								{
									num13 = (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife);
									num14 = i;
								}
							}
						}
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("AtaxiaHealOrb").Type, 0, 0f, projectile.owner, (float)num14, num12);
					}
					else if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().xerocSet)
					{
						int num = projectile.damage / 2;
						Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().xerocDmg += (float)num;
						int[] array = new int[200];
						int num3 = 0;
						int num4 = 0;
						for (int i = 0; i < 200; i++)
						{
							if (Main.npc[i].CanBeChasedBy(projectile, false))
							{
								float num5 = Math.Abs(Main.npc[i].position.X + (float)(Main.npc[i].width / 2) - projectile.position.X + (float)(projectile.width / 2)) + Math.Abs(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2) - projectile.position.Y + (float)(projectile.height / 2));
								if (num5 < 800f)
								{
									if (Collision.CanHit(projectile.position, 1, 1, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height) && num5 > 50f)
									{
										array[num4] = i;
										num4++;
									}
									else if (num4 == 0)
									{
										array[num3] = i;
										num3++;
									}
								}
							}
						}
						if (num3 == 0 && num4 == 0)
						{
							return;
						}
						int num6;
						if (num4 > 0)
						{
							num6 = array[Main.rand.Next(num4)];
						}
						else
						{
							num6 = array[Main.rand.Next(num3)];
						}
						float num7 = 30f;
						float num8 = (float)Main.rand.Next(-100, 101);
						float num9 = (float)Main.rand.Next(-100, 101);
						float num10 = (float)Math.Sqrt((double)(num8 * num8 + num9 * num9));
						num10 = num7 / num10;
						num8 *= num10;
						num9 *= num10;
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, num8, num9, Mod.Find<ModProjectile>("XerocOrb").Type, num, 0f, projectile.owner, (float)num6, 0f);
						float num11 = 0.02f;
						num11 -= (float)projectile.numHits * 0.01f;
						if (num11 <= 0f)
						{
							return;
						}
						float num12 = (float)projectile.damage * num11;
						if ((int)num12 <= 0)
						{
							return;
						}
						if (Main.player[Main.myPlayer].lifeSteal <= 0f)
						{
							return;
						}
						Main.player[Main.myPlayer].lifeSteal -= num12;
						float num13 = 0f;
						int num14 = projectile.owner;
						for (int i = 0; i < 255; i++)
						{
							if (Main.player[i].active && !Main.player[i].dead && ((!Main.player[projectile.owner].hostile && !Main.player[i].hostile) || Main.player[projectile.owner].team == Main.player[i].team))
							{
								float num15 = Math.Abs(Main.player[i].position.X + (float)(Main.player[i].width / 2) - projectile.position.X + (float)(projectile.width / 2)) + Math.Abs(Main.player[i].position.Y + (float)(Main.player[i].height / 2) - projectile.position.Y + (float)(projectile.height / 2));
								if (num15 < 1200f && (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife) > num13)
								{
									num13 = (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife);
									num14 = i;
								}
							}
						}
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("XerocHealOrb").Type, 0, 0f, projectile.owner, (float)num14, num12);
					}
				}
				else if (projectile.CountsAsClass(DamageClass.Melee))
				{
					if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().ataxiaGeyser)
					{
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ChaosGeyser").Type, 30, 0f, projectile.owner, 0f, 0f);
					}
					else if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().xerocSet)
					{
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("XerocBlast").Type, 60, 0f, projectile.owner, 0f, 0f);
					}
				}
				else if (projectile.CountsAsClass(DamageClass.Ranged))
				{
					if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().xerocSet)
					{
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("XerocFire").Type, 60, 0f, projectile.owner, 0f, 0f);
					}
				}
				else if (projectile.CountsAsClass(DamageClass.Throwing))
				{
					if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().daedalusSplit)
					{
						int num251 = Main.rand.Next(2, 5);
						for (int num252 = 0; num252 < num251; num252++)
						{
							Vector2 value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
							while (value15.X == 0f && value15.Y == 0f)
							{
								value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
							}
							value15.Normalize();
							value15 *= (float)Main.rand.Next(70, 101) * 0.1f;
							Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.oldPosition.X + (float)(projectile.width / 2), projectile.oldPosition.Y + (float)(projectile.height / 2), value15.X, value15.Y, 90, (int)((double)projectile.damage * 0.5), 0.25f, projectile.owner, 0f, 0f);
						}
					}
					else if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().xerocSet)
					{
						int num = projectile.damage;
						Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().xerocDmg += (float)num;
						int[] array = new int[200];
						int num3 = 0;
						int num4 = 0;
						for (int i = 0; i < 200; i++)
						{
							if (Main.npc[i].CanBeChasedBy(projectile, false))
							{
								float num5 = Math.Abs(Main.npc[i].position.X + (float)(Main.npc[i].width / 2) - projectile.position.X + (float)(projectile.width / 2)) + Math.Abs(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2) - projectile.position.Y + (float)(projectile.height / 2));
								if (num5 < 800f)
								{
									if (Collision.CanHit(projectile.position, 1, 1, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height) && num5 > 50f)
									{
										array[num4] = i;
										num4++;
									}
									else if (num4 == 0)
									{
										array[num3] = i;
										num3++;
									}
								}
							}
						}
						if (num3 == 0 && num4 == 0)
						{
							return;
						}
						int num6;
						if (num4 > 0)
						{
							num6 = array[Main.rand.Next(num4)];
						}
						else
						{
							num6 = array[Main.rand.Next(num3)];
						}
						float num7 = Main.rand.Next(15, 30);
						float num8 = (float)Main.rand.Next(-100, 101);
						float num9 = (float)Main.rand.Next(-100, 101);
						float num10 = (float)Math.Sqrt((double)(num8 * num8 + num9 * num9));
						num10 = num7 / num10;
						num8 *= num10;
						num9 *= num10;
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, num8, num9, Mod.Find<ModProjectile>("XerocStar").Type, num, 0f, projectile.owner, (float)num6, 0f);
					}
				}
				else if (projectile.minion)
				{
					if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().tearMinions)
					{
						target.AddBuff(Mod.Find<ModBuff>("TemporalSadness").Type, 60);
					}
					if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().shadowMinions)
					{
						target.AddBuff(BuffID.ShadowFlame, 300);
					}
					if (Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().xerocSet)
					{
						int num = projectile.damage / 2;
						Main.player[(int)Player.FindClosest(projectile.position, projectile.width, projectile.height)].GetModPlayer<CalamityPlayer1Point2>().xerocDmg += (float)num;
						int[] array = new int[200];
						int num3 = 0;
						int num4 = 0;
						for (int i = 0; i < 200; i++)
						{
							if (Main.npc[i].CanBeChasedBy(projectile, false))
							{
								float num5 = Math.Abs(Main.npc[i].position.X + (float)(Main.npc[i].width / 2) - projectile.position.X + (float)(projectile.width / 2)) + Math.Abs(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2) - projectile.position.Y + (float)(projectile.height / 2));
								if (num5 < 800f)
								{
									if (Collision.CanHit(projectile.position, 1, 1, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height) && num5 > 50f)
									{
										array[num4] = i;
										num4++;
									}
									else if (num4 == 0)
									{
										array[num3] = i;
										num3++;
									}
								}
							}
						}
						if (num3 == 0 && num4 == 0)
						{
							return;
						}
						int num6;
						if (num4 > 0)
						{
							num6 = array[Main.rand.Next(num4)];
						}
						else
						{
							num6 = array[Main.rand.Next(num3)];
						}
						float num7 = 15f;
						float num8 = (float)Main.rand.Next(-100, 101);
						float num9 = (float)Main.rand.Next(-100, 101);
						float num10 = (float)Math.Sqrt((double)(num8 * num8 + num9 * num9));
						num10 = num7 / num10;
						num8 *= num10;
						num9 *= num10;
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, num8, num9, Mod.Find<ModProjectile>("XerocBubble").Type, num, 0f, projectile.owner, (float)num6, 0f);
					}
				}
			}
		}
	}
}
