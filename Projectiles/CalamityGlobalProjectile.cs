using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;
using CalamityModClassicPreTrailer.World;
using CalamityModClassicPreTrailer.NPCs.TheDevourerofGods;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Projectiles
{
	public class CalamityGlobalProjectile : GlobalProjectile
	{
		#region InstancePerEntity
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}
		#endregion

		public bool rogue = false;

		public bool trueMelee = false;

		private bool setDamageValues = true;

		public bool forceMelee = false;

		public bool forceRanged = false;

		public bool forceMagic = false;

		public bool forceRogue = false;

		public float spawnedPlayerMinionDamageValue = 1f;

		public int spawnedPlayerMinionProjectileDamageValue = 0;

		private int counter = 0;

		private int counter2 = 0;

		private int defDamage = 0;

		#region SetDefaults
		public override void SetDefaults(Projectile projectile)
		{
			switch (projectile.type)
			{
				case ProjectileID.Spear:
				case ProjectileID.ChainKnife:
				case ProjectileID.TheRottedFork:
				case ProjectileID.BallOHurt:
				case ProjectileID.TheMeatball:
				case ProjectileID.Swordfish:
				case ProjectileID.Arkhalis:
				case ProjectileID.BlueMoon:
				case ProjectileID.DarkLance:
				case ProjectileID.Sunfury:
				case ProjectileID.Anchor:
				case ProjectileID.BoxingGlove:
				case ProjectileID.CobaltNaginata:
				case ProjectileID.PalladiumPike:
				case ProjectileID.MythrilHalberd:
				case ProjectileID.OrichalcumHalberd:
				case ProjectileID.AdamantiteGlaive:
				case ProjectileID.TitaniumTrident:
				case ProjectileID.TheDaoofPow:
				case ProjectileID.GolemFist:
				case ProjectileID.Gungnir:
				case ProjectileID.ObsidianSwordfish:
				case ProjectileID.ChainGuillotine:
				case ProjectileID.MonkStaffT3:
				case ProjectileID.MonkStaffT1:
				case ProjectileID.SolarWhipSword:
					trueMelee = true;
					break;
			}
		}
		#endregion

		#region AI
		public override void AI(Projectile projectile)
		{
			if (defDamage == 0)
				defDamage = projectile.damage;

			if (NPC.downedMoonlord)
			{
				if (CalamityModClassicPreTrailer.dungeonProjectileBuffList.Contains(projectile.type))
					projectile.damage = defDamage + 60;
			}
			if (CalamityWorldPreTrailer.downedDoG && (Main.pumpkinMoon || Main.snowMoon))
			{
				if (CalamityModClassicPreTrailer.eventProjectileBuffList.Contains(projectile.type))
					projectile.damage = defDamage + 90;
			}
			else if (CalamityWorldPreTrailer.buffedEclipse && Main.eclipse)
			{
				if (CalamityModClassicPreTrailer.eventProjectileBuffList.Contains(projectile.type))
					projectile.damage = defDamage + 120;
			}

			if (projectile.ModProjectile != null && projectile.ModProjectile.Mod.Name.Equals("CalamityModClassicPreTrailer"))
				goto SKIP_CALAMITY;

			if ((projectile.minion || projectile.sentry) && !ProjectileID.Sets.StardustDragon[projectile.type]) //For all other mods and vanilla, exclude dragon due to bugs
			{
				if (setDamageValues)
				{
					spawnedPlayerMinionDamageValue = Main.player[projectile.owner].GetDamage(DamageClass.Summon).Base;
					spawnedPlayerMinionProjectileDamageValue = projectile.damage;
					setDamageValues = false;
				}
				if (Main.player[projectile.owner].GetDamage(DamageClass.Summon).Base != spawnedPlayerMinionDamageValue)
				{
					int damage2 = (int)(((float)spawnedPlayerMinionProjectileDamageValue / spawnedPlayerMinionDamageValue) * Main.player[projectile.owner].GetDamage(DamageClass.Summon).Base);
					projectile.damage = damage2;
				}
			}
			SKIP_CALAMITY:

			if (forceMelee)
			{
				projectile.hostile = false;
				projectile.friendly = true;
				projectile.DamageType = DamageClass.Melee;
				rogue = false;
			}
			else if (forceRanged)
			{
				projectile.hostile = false;
				projectile.friendly = true;
				projectile.DamageType = DamageClass.Ranged;
				rogue = false;
			}
			else if (forceMagic)
			{
				projectile.hostile = false;
				projectile.friendly = true;
				projectile.DamageType = DamageClass.Magic;
				rogue = false;
			}
			else if (forceRogue)
			{
				projectile.hostile = false;
				projectile.friendly = true;
				rogue = true;
			}

			if (projectile.type == ProjectileID.NettleBurstRight || projectile.type == ProjectileID.NettleBurstLeft || projectile.type == ProjectileID.NettleBurstEnd)
			{
				if (Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].type == Mod.Find<ModItem>("ThornBlossom").Type)
					projectile.penetrate = 1;
			}
			else if (projectile.type == ProjectileID.GiantBee || projectile.type == ProjectileID.Bee)
			{
				if (projectile.timeLeft > 570) //all of these have a time left of 600 or 660
				{
					if (Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].type == ItemID.BeesKnees)
					{
						projectile.DamageType = DamageClass.Ranged;
					}
				}
			}

			if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().eQuiver && projectile.CountsAsClass(DamageClass.Ranged) &&
				projectile.friendly && CalamityModClassicPreTrailer.rangedProjectileExceptionList.TrueForAll(x => projectile.type != x))
			{
				if (Main.rand.Next(200) > 198)
				{
					float spread = 180f * 0.0174f;
					double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
					double deltaAngle = spread / 8f;
					double offsetAngle;
					int i;
					for (i = 0; i < 1; i++)
					{
						offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
						if (projectile.owner == Main.myPlayer)
						{
							int projectile1 = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 8f), (float)(Math.Cos(offsetAngle) * 8f), projectile.type, (int)((double)projectile.damage * 0.5), projectile.knockBack, projectile.owner, 0f, 0f);
							int projectile2 = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 8f), (float)(-Math.Cos(offsetAngle) * 8f), projectile.type, (int)((double)projectile.damage * 0.5), projectile.knockBack, projectile.owner, 0f, 0f);
							Main.projectile[projectile1].timeLeft = 60;
							Main.projectile[projectile2].timeLeft = 60;
							Main.projectile[projectile1].noDropItem = true;
							Main.projectile[projectile2].noDropItem = true;
						}
					}
				}
			}

			if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().fungalSymbiote && trueMelee)
			{
				counter++;
				if (counter >= 6)
				{
					counter = 0;
					if (projectile.owner == Main.myPlayer && Main.player[projectile.owner].ownedProjectileCounts[ProjectileID.Mushroom] < 30)
					{
						Projectile.NewProjectile(projectile.GetSource_FromThis(),projectile.Center.X, projectile.Center.Y, 0f, 0f, ProjectileID.Mushroom,
							(int)((double)projectile.damage * 0.25), 0f, projectile.owner, 0f, 0f);
					}
				}
			}

			if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().nanotech && rogue && projectile.friendly)
			{
				counter++;
				if (counter >= 30)
				{
					counter = 0;
					if (projectile.owner == Main.myPlayer && Main.player[projectile.owner].ownedProjectileCounts[Mod.Find<ModProjectile>("Nanotech").Type] < 30)
					{
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("Nanotech").Type,
							(int)((double)projectile.damage * 0.15), 0f, projectile.owner, 0f, 0f);
					}
				}
			}

			if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().daedalusSplit && rogue && projectile.friendly)
			{
				counter2++;
				if (counter2 >= 30)
				{
					counter2 = 0;
					if (projectile.owner == Main.myPlayer && Main.player[projectile.owner].ownedProjectileCounts[90] < 30)
					{
						for (int num252 = 0; num252 < 2; num252++)
						{
							Vector2 value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
							while (value15.X == 0f && value15.Y == 0f)
							{
								value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
							}
							value15.Normalize();
							value15 *= (float)Main.rand.Next(70, 101) * 0.1f;
							int shard = Projectile.NewProjectile(projectile.GetSource_FromThis(null), projectile.oldPosition.X + (float)(projectile.width / 2), projectile.oldPosition.Y + (float)(projectile.height / 2), value15.X, value15.Y, 90, (int)((double)projectile.damage * 0.25), 0f, projectile.owner, 0f, 0f);
						}
					}
				}
			}

			if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().theBeeDamage > 0 && projectile.owner == Main.myPlayer && projectile.friendly && projectile.damage > 0 &&
				(projectile.CountsAsClass(DamageClass.Melee) || projectile.CountsAsClass(DamageClass.Ranged) || projectile.CountsAsClass(DamageClass.Magic) || rogue))
			{
				int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 91, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f, 0, default(Color), 0.5f);
				Main.dust[dust].noGravity = true;
			}
			if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().providenceLore && projectile.owner == Main.myPlayer && projectile.friendly && projectile.damage > 0 &&
				(projectile.CountsAsClass(DamageClass.Melee) || projectile.CountsAsClass(DamageClass.Ranged) || projectile.CountsAsClass(DamageClass.Magic) || rogue))
			{
				int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 244, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f, 0, default(Color), 0.5f);
				Main.dust[dust].noGravity = true;
			}
		}
		#endregion

		#region PostAI
		public override void PostAI(Projectile projectile)
		{
			int x = (int)(projectile.Center.X / 16f);
			int y = (int)(projectile.Center.Y / 16f);
			for (int i = x - 1; i <= x + 1; i++)
			{
				for (int j = y - 1; j <= y + 1; j++)
				{
					if (projectile.type == ProjectileID.PureSpray)
					{
						WorldGenerationMethods.ConvertFromAstral(i, j, ConvertType.Pure);
					}
					if (projectile.type == ProjectileID.CorruptSpray)
					{
						WorldGenerationMethods.ConvertFromAstral(i, j, ConvertType.Corrupt);
					}
					if (projectile.type == ProjectileID.CrimsonSpray)
					{
						WorldGenerationMethods.ConvertFromAstral(i, j, ConvertType.Crimson);
					}
					if (projectile.type == ProjectileID.HallowSpray)
					{
						WorldGenerationMethods.ConvertFromAstral(i, j, ConvertType.Hallow);
					}
				}
			}
		}
		#endregion

		#region OnHitNPC
		public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (projectile.owner == Main.myPlayer)
			{
				if (projectile.CountsAsClass(DamageClass.Magic) && Main.player[projectile.owner].ghostHeal)
				{
					float num = 0.1f;
					num -= (float)projectile.numHits * 0.05f;
					if (num < 0f)
					{
						num = 0f;
					}
					float num2 = (float)projectile.damage * num;
					Main.player[Main.myPlayer].lifeSteal -= num2;
				}
				if (projectile.type == ProjectileID.VampireKnife)
				{
					float num = (float)projectile.damage * 0.0375f;
					if (num < 0f)
					{
						num = 0f;
					}
					Main.player[Main.myPlayer].lifeSteal -= num;
				}
				if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().alchFlask &&
					(projectile.CountsAsClass(DamageClass.Magic) || rogue || projectile.CountsAsClass(DamageClass.Melee) || projectile.minion || projectile.CountsAsClass(DamageClass.Ranged) || projectile.sentry || CalamityModClassicPreTrailer.projectileMinionList.Contains(projectile.type)) &&
					Main.player[projectile.owner].ownedProjectileCounts[Mod.Find<ModProjectile>("PlagueSeeker").Type] < 6)
				{
					int newDamage = (int)((double)projectile.damage * 0.25);
					if (newDamage > 30)
					{
						newDamage = 30;
					}
					int plague = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("PlagueSeeker").Type, newDamage, 0f, projectile.owner, 0f, 0f);
				}
				if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().reaverBlast && projectile.CountsAsClass(DamageClass.Melee))
				{
					int newDamage = (int)((double)projectile.damage * 0.2);
					if (newDamage > 30)
					{
						newDamage = 30;
					}
					Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ReaverBlast").Type, newDamage, 0f, projectile.owner, 0f, 0f);
				}
				if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().auricSet && target.canGhostHeal)
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
					Main.player[Main.myPlayer].lifeSteal -= num12 * 1.5f;
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
				else if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().silvaSet && target.canGhostHeal)
				{
					float num11 = 0.03f;
					num11 -= (float)projectile.numHits * 0.015f;
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
					Main.player[Main.myPlayer].lifeSteal -= num12 * 1.5f;
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
					if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().silvaMage && projectile.penetrate == 1 &&
						Main.rand.Next(0, 100) >= 97)
					{
						SoundEngine.PlaySound(SoundID.Zombie103, projectile.position);
						projectile.position = projectile.Center;
						projectile.width = (projectile.height = 96);
						projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
						projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
						for (int num193 = 0; num193 < 3; num193++)
						{
							Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 157, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 1.5f);
						}
						for (int num194 = 0; num194 < 30; num194++)
						{
							int num195 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 157, 0f, 0f, 0, new Color(Main.DiscoR, 203, 103), 2.5f);
							Main.dust[num195].noGravity = true;
							Main.dust[num195].velocity *= 3f;
							num195 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 157, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 1.5f);
							Main.dust[num195].velocity *= 2f;
							Main.dust[num195].noGravity = true;
						}
						projectile.damage *= (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().auricSet ? 7 : 4);
						projectile.Damage();
					}
					if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().tarraMage && target.canGhostHeal)
					{
						if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().tarraMageHealCooldown <= 0)
						{
							Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().tarraMageHealCooldown = 90;
							float num11 = 0.03f;
							num11 -= (float)projectile.numHits * 0.015f;
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
							Main.player[Main.myPlayer].lifeSteal -= num12 * 1.5f;
							int healAmount = (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().auricSet ? projectile.damage / 100 : projectile.damage / 50);
							Player player = Main.player[projectile.owner];
							player.statLife += healAmount;
							player.HealEffect(healAmount);
							if (player.statLife > player.statLifeMax2)
							{
								player.statLife = player.statLifeMax2;
							}
						}
					}
					if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().reaverBurst)
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
							int newDamage = (int)((double)projectile.damage * 0.25);
							if (newDamage > 40)
							{
								newDamage = 40;
							}
							Projectile.NewProjectile(projectile.GetSource_FromThis(null), projectile.oldPosition.X + (float)(projectile.width / 2), projectile.oldPosition.Y + (float)(projectile.height / 2), value15.X, value15.Y, 569 + Main.rand.Next(3), newDamage, 0f, projectile.owner, 0f, 0f);
						}
					}
					else if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().ataxiaMage &&
						Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().ataxiaDmg <= 0)
					{
						int num = projectile.damage / 2;
						Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().ataxiaDmg += (float)num;
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
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, num8, num9, Mod.Find<ModProjectile>("AtaxiaOrb").Type, (int)((double)num * 1.25), 0f, projectile.owner, (float)num6, 0f);
						if (target.canGhostHeal)
						{
							float num11 = 0.1f; //0.2
							num11 -= (float)projectile.numHits * 0.05f; //0.05
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
							Main.player[Main.myPlayer].lifeSteal -= num12 * 1.5f;
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
					}
					else if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().xerocSet &&
						Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().xerocDmg <= 0)
					{
						int num = projectile.damage / 2;
						Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().xerocDmg += (float)num;
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
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, num8, num9, Mod.Find<ModProjectile>("XerocOrb").Type, (int)((double)num * 1.25), 0f, projectile.owner, (float)num6, 0f);
						if (target.canGhostHeal)
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
							Main.player[Main.myPlayer].lifeSteal -= num12 * 1.5f;
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
					else if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().godSlayerMage &&
						Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().godSlayerDmg <= 0)
					{
						int num = projectile.damage / 2;
						Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().godSlayerDmg += (float)num;
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
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, num8, num9, Mod.Find<ModProjectile>("GodSlayerOrb").Type,
							(int)((double)num * (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().auricSet ? 2.0 : 1.5)), 0f, projectile.owner, (float)num6, 0f);
						if (target.canGhostHeal)
						{
							float num11 = (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().auricSet ? 0.03f : 0.06f); //0.2
							num11 -= (float)projectile.numHits * 0.015f; //0.05
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
							Main.player[Main.myPlayer].lifeSteal -= num12 * 1.5f;
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
							Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("GodSlayerHealOrb").Type, 0, 0f, projectile.owner, (float)num14, num12);
						}
					}
				}
				else if (projectile.CountsAsClass(DamageClass.Melee))
				{
					if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().ataxiaGeyser &&
						Main.player[projectile.owner].ownedProjectileCounts[Mod.Find<ModProjectile>("ChaosGeyser").Type] < 3)
					{
						int newDamage = (int)((double)projectile.damage * 0.15);
						if (newDamage > 35)
						{
							newDamage = 35;
						}
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ChaosGeyser").Type, newDamage, 0f, projectile.owner, 0f, 0f);
					}
					else if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().xerocSet &&
						Main.player[projectile.owner].ownedProjectileCounts[Mod.Find<ModProjectile>("XerocBlast").Type] < 3)
					{
						int newDamage = (int)((double)projectile.damage * 0.2);
						if (newDamage > 40)
						{
							newDamage = 40;
						}
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("XerocBlast").Type, newDamage, 0f, projectile.owner, 0f, 0f);
					}
				}
				else if (projectile.CountsAsClass(DamageClass.Ranged))
				{
					if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().xerocSet &&
						Main.player[projectile.owner].ownedProjectileCounts[Mod.Find<ModProjectile>("XerocFire").Type] < 3)
					{
						int newDamage = (int)((double)projectile.damage * 0.15);
						if (newDamage > 40)
						{
							newDamage = 40;
						}
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("XerocFire").Type, newDamage, 0f, projectile.owner, 0f, 0f);
					}
				}
				else if (rogue)
				{
					if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().xerocSet &&
						Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().xerocDmg <= 0)
					{
						int num = projectile.damage / 2;
						Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().xerocDmg += (float)num;
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
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, num8, num9, Mod.Find<ModProjectile>("XerocStar").Type, (int)((double)num * 1.6), 0f, projectile.owner, (float)num6, 0f);
					}
				}
				else if (projectile.minion || projectile.sentry || CalamityModClassicPreTrailer.projectileMinionList.Contains(projectile.type))
				{
					if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().pArtifact)
					{
						target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 300);
					}
					if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().tearMinions)
					{
						target.AddBuff(Mod.Find<ModBuff>("TemporalSadness").Type, 60);
					}
					if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().shadowMinions)
					{
						target.AddBuff(BuffID.ShadowFlame, 300);
					}
					if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().godSlayerSummon &&
						Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().godSlayerDmg <= 0)
					{
						int num = projectile.damage / 2;
						float ai1 = (Main.rand.NextFloat() + 0.5f);
						Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().godSlayerDmg += (float)num;
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
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, num8, num9, Mod.Find<ModProjectile>("GodSlayerPhantom").Type, (int)((double)num * 2.0), 0f, projectile.owner, 0f, ai1);
					}
					else if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().xerocSet &&
						Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().xerocDmg <= 0)
					{
						int num = projectile.damage / 2;
						Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().xerocDmg += (float)num;
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
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, num8, num9, Mod.Find<ModProjectile>("XerocBubble").Type, (int)((double)num * 1.2), 0f, projectile.owner, (float)num6, 0f);
					}
				}
			}
		}
		#endregion

		#region CanDamage
		public override bool? CanDamage(Projectile projectile)/* tModPorter Suggestion: Return null instead of true */
		{
			switch (projectile.type)
			{
				case ProjectileID.Sharknado:
					if (projectile.timeLeft > 440)
						return false;
					break;
				case ProjectileID.Cthulunado:
					if (projectile.timeLeft > 740)
						return false;
					break;
			}
			return true;
		}
		#endregion

		#region Drawing
		public override Color? GetAlpha(Projectile projectile, Color lightColor)
		{
			if (Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>().trippy)
				return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, projectile.alpha);
			return null;
		}

		public override bool PreDraw(Projectile projectile, ref Color lightColor)
		{
			if (Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>().trippy)
			{
				Texture2D texture = TextureAssets.Projectile[projectile.type].Value;
				SpriteEffects spriteEffects = SpriteEffects.None;
				if (projectile.spriteDirection == 1)
				{
					spriteEffects = SpriteEffects.FlipHorizontally;
				}
				float num66 = 0f;
				Vector2 vector11 = new Vector2((float)(texture.Width / 2), (float)(texture.Height / Main.projFrames[projectile.type] / 2));
				Microsoft.Xna.Framework.Color color9 = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
				Microsoft.Xna.Framework.Color alpha15 = projectile.GetAlpha(color9);
				float num212 = 0.99f;
				alpha15.R = (byte)((float)alpha15.R * num212);
				alpha15.G = (byte)((float)alpha15.G * num212);
				alpha15.B = (byte)((float)alpha15.B * num212);
				alpha15.A = (byte)((float)alpha15.A * num212);
				for (int num213 = 0; num213 < 4; num213++)
				{
					Vector2 position9 = projectile.position;
					float num214 = Math.Abs(projectile.Center.X - Main.player[Main.myPlayer].Center.X);
					float num215 = Math.Abs(projectile.Center.Y - Main.player[Main.myPlayer].Center.Y);
					if (num213 == 0 || num213 == 2)
					{
						position9.X = Main.player[Main.myPlayer].Center.X + num214;
					}
					else
					{
						position9.X = Main.player[Main.myPlayer].Center.X - num214;
					}
					position9.X -= (float)(projectile.width / 2);
					if (num213 == 0 || num213 == 1)
					{
						position9.Y = Main.player[Main.myPlayer].Center.Y + num215;
					}
					else
					{
						position9.Y = Main.player[Main.myPlayer].Center.Y - num215;
					}
					int frames = texture.Height / Main.projFrames[projectile.type];
					int y = frames * projectile.frame;
					position9.Y -= (float)(projectile.height / 2);
					Main.spriteBatch.Draw(texture,
						new Vector2(position9.X - Main.screenPosition.X + (float)(projectile.width / 2) - (float)texture.Width * projectile.scale / 2f + vector11.X * projectile.scale, position9.Y - Main.screenPosition.Y + (float)projectile.height - (float)texture.Height * projectile.scale / (float)Main.projFrames[projectile.type] + 4f + vector11.Y * projectile.scale + num66 + projectile.gfxOffY),
						new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y, texture.Width, frames)), alpha15, projectile.rotation, vector11, projectile.scale, spriteEffects, 0f);
				}
			}
			return true;
		}
		#endregion

		#region Kill
		public override void OnKill(Projectile projectile, int timeLeft)
		{
			if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().providenceLore && projectile.owner == Main.myPlayer && projectile.friendly && projectile.damage > 0 &&
				(projectile.CountsAsClass(DamageClass.Melee) || projectile.CountsAsClass(DamageClass.Ranged) || projectile.CountsAsClass(DamageClass.Magic) || rogue))
			{
				SoundEngine.PlaySound(SoundID.Item20, projectile.position);
				for (int i = 0; i < 3; i++)
				{
					int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 244, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f, 0, default(Color), 1f);
					Main.dust[dust].noGravity = true;
				}
			}
			if (projectile.CountsAsClass(DamageClass.Ranged))
			{
				if (Main.player[projectile.owner].GetModPlayer<CalamityPlayerPreTrailer>().tarraRanged && Main.rand.Next(0, 100) >= 88)
				{
					int num251 = Main.rand.Next(2, 4);
					for (int num252 = 0; num252 < num251; num252++)
					{
						Vector2 value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
						while (value15.X == 0f && value15.Y == 0f)
						{
							value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
						}
						value15.Normalize();
						value15 *= (float)Main.rand.Next(70, 101) * 0.1f;
						int newDamage = (int)((double)projectile.damage * 0.33);
						if (newDamage > 65)
						{
							newDamage = 65;
						}
						Projectile.NewProjectile(projectile.GetSource_FromThis(null), projectile.oldPosition.X + (float)(projectile.width / 2), projectile.oldPosition.Y + (float)(projectile.height / 2), value15.X, value15.Y, Mod.Find<ModProjectile>("TarraEnergy").Type, newDamage, 0f, projectile.owner, 0f, 0f);
					}
				}
			}
		}
		#endregion

		#region CanHit
		public override bool CanHitPlayer(Projectile projectile, Player target)
		{
			if (projectile.type == ProjectileID.CultistBossLightningOrb)
			{
				return false;
			}
			return true;
		}
		#endregion
	}
}
