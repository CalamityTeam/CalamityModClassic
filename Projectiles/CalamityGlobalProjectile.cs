using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1;
using CalamityModClassic1Point1.NPCs.TheDevourerofGods;
using CalamityModClassic1Point1.Items.Armor;

namespace CalamityModClassic1Point1.Projectiles
{
	public class CalamityGlobalProjectile : GlobalProjectile
	{
		public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (CalamityPlayer.alchFlask && (projectile.CountsAsClass(DamageClass.Magic) || projectile.CountsAsClass(DamageClass.Throwing) || projectile.CountsAsClass(DamageClass.Melee) || projectile.minion || projectile.CountsAsClass(DamageClass.Ranged)))
			{
				target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 180);
				float damageAlch = (float)projectile.damage * 0.5f;
				int ownerAlch = projectile.owner;
				int plague = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("PlagueSeeker").Type, 20, 0f, projectile.owner, (float)ownerAlch, damageAlch);
				Main.projectile[plague].DamageType = DamageClass.Generic;
			}
			if (projectile.CountsAsClass(DamageClass.Magic))
			{
				if (CalamityPlayer.ataxiaHurt)
				{
					int num = projectile.damage / 2;
					CalamityPlayer.ataxiaDmg += (float)num;
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
				}
				if (CalamityPlayer.ataxiaHeal)
				{
					float num11 = 0.1f;
					num11 -= (float)projectile.numHits * 0.05f;
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
				if (CalamityPlayer.xerocHurt)
				{
					int num = projectile.damage / 2;
					CalamityPlayer.xerocDmg += (float)num;
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
				}
				if (CalamityPlayer.xerocHeal)
				{
					float num11 = 0.1f;
					num11 -= (float)projectile.numHits * 0.05f;
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
				if (CalamityPlayer.xerocBlast)
				{
					float num16 = (float)projectile.damage * 0.5f;
					int num17 = projectile.owner;
					Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("XerocBlast").Type, 60, 0f, projectile.owner, (float)num17, num16);
				}
				if (CalamityPlayer.reaverBlast)
				{
					float num18 = (float)projectile.damage * 0.25f;
					int num19 = projectile.owner;
					Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ReaverBlast").Type, 30, 0f, projectile.owner, (float)num19, num18);
				}
			}
			else if (projectile.CountsAsClass(DamageClass.Ranged))
			{
				if (CalamityPlayer.xerocSpike)
				{
					float num16 = (float)projectile.damage * 0.35f;
					int num17 = projectile.owner;
					Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("XerocFire").Type, 60, 0f, projectile.owner, (float)num17, num16);
				}
			}
			else if (projectile.CountsAsClass(DamageClass.Throwing))
			{
				if (CalamityPlayer.xerocTear)
				{
					int num = projectile.damage;
					CalamityPlayer.xerocDmg += (float)num;
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
				if (CalamityPlayer.tearMinions)
				{
					target.AddBuff(Mod.Find<ModBuff>("TemporalSadness").Type, 60);
				}
				if (CalamityPlayer.shadowMinions)
				{
					target.AddBuff(BuffID.ShadowFlame, 300);
				}
				if (CalamityPlayer.xerocSummon)
				{
					int num = projectile.damage / 2;
					CalamityPlayer.xerocDmg += (float)num;
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
