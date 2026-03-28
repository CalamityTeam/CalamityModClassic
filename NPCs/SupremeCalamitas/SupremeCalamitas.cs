using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Weapons;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.SupremeCalamitas
{
	[AutoloadBossHead]
	public class SupremeCalamitas : ModNPC
	{
		private float bossLife;
		private float uDieLul = 1f;
		private float passedVar = 0f;

		private bool protectionBoost = false;
		private bool canDespawn = false;
		private bool despawnProj = false;
		private bool startText = false;
		private bool wormAlive = false;
		private bool startBattle = false; //100%
		private bool startSecondAttack = false; //80%
		private bool startThirdAttack = false; //60%
		private bool halfLife = false; //40%
		private bool startFourthAttack = false; //30%
		private bool secondStage = false; //20%
		private bool startFifthAttack = false; //10%
		private bool gettingTired = false; //8%
		private bool gettingTired2 = false; //6%
		private bool gettingTired3 = false; //4%
		private bool gettingTired4 = false; //2%
		private bool gettingTired5 = false; //1%
		private bool willCharge = false;
		private bool canFireSplitingFireball = true;

		private int giveUpCounter = 1200;
		private int lootTimer = 0; //900 * 5 = 4500
		private int phaseChange = 0;
		private int spawnX = 0;
		private int spawnX2 = 0;
		private int spawnXReset = 0;
		private int spawnXReset2 = 0;
		private int spawnXAdd = 200;
		private int spawnY = 0;
		private int spawnYReset = 0;
		private int spawnYAdd = 0;

		private Rectangle safeBox = default(Rectangle);

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Supreme Calamitas");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
				new FlavorTextBestiaryInfoElement("The Witch herself in the flesh. Though in this case piloting a mechanical vessel for unknown reasons, her brimstone magic is not something to be taken lightly.")
			});
		}

		public override void SetDefaults()
		{
			NPC.damage = 350;
			NPC.npcSlots = 50f;
			NPC.width = 120; //324
			NPC.height = 120; //216
			NPC.defense = 0;
			NPC.value = Item.buyPrice(10, 0, 0, 0);
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 5500000 : 5000000;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 6250000;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 2300000 : 2100000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.aiStyle = -1; //new
			AIType = -1; //new
			NPC.knockBackResist = 0f;
			NPCID.Sets.TrailCacheLength[NPC.type] = 8;
			NPCID.Sets.TrailingMode[NPC.type] = 1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.dontTakeDamage = false;
			NPC.chaseable = true;
			NPC.boss = true;
			NPC.canGhostHeal = false;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/SCG");
		}

		public override void AI()
		{
			#region StartUp
			CalamityGlobalNPC.SCal = NPC.whoAmI;
			lootTimer++;
			if (Main.slimeRain)
			{
				Main.StopSlimeRain(true);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			if (Main.raining)
			{
				Main.raining = false;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			Player player = Main.player[NPC.target];
			if (!startText)
			{
				if (Main.LocalPlayer.GetModPlayer<CalamityPlayerPreTrailer>().sCalKillCount == 4)
				{
					string key = "Don't get me wrong, I like pain too, but you're just ridiculous."; //kill SCal 4 times
					Color messageColor = Color.Orange;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
				}
				else if (Main.LocalPlayer.GetModPlayer<CalamityPlayerPreTrailer>().sCalKillCount == 1)
				{
					string key = "Do you enjoy going through hell?"; //kill SCal once
					Color messageColor = Color.Orange;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
				}
				if (Main.LocalPlayer.GetModPlayer<CalamityPlayerPreTrailer>().sCalDeathCount < 51)
				{
					if (Main.LocalPlayer.GetModPlayer<CalamityPlayerPreTrailer>().sCalDeathCount == 50)
					{
						string key = "Alright, I'm done counting. You probably died this much just to see what I'd say."; //die 50 or more times
						Color messageColor = Color.Orange;
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
					}
					else if (Main.LocalPlayer.GetModPlayer<CalamityPlayerPreTrailer>().sCalDeathCount > 19)
					{
						string key = "Do you have a fetish for getting killed or something?"; //die 20 or more times
						Color messageColor = Color.Orange;
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
					}
					else if (Main.LocalPlayer.GetModPlayer<CalamityPlayerPreTrailer>().sCalDeathCount > 4)
					{
						string key = "You must enjoy dying more than most people, huh?"; //die 5 or more times
						Color messageColor = Color.Orange;
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
					}
				}
				startText = true;
			}
			#endregion
			#region ArenaCreation
			if (NPC.localAI[3] == 0f)
			{
				NPC.localAI[3] = 1f;
				Vector2 vectorPlayer = new Vector2(player.position.X, player.position.Y);
				if (CalamityWorldPreTrailer.bossRushActive)
				{
					safeBox.X = spawnX = spawnXReset = (int)(vectorPlayer.X - 1250f);
					spawnX2 = spawnXReset2 = (int)(vectorPlayer.X + 1250f);
					safeBox.Y = spawnY = spawnYReset = (int)(vectorPlayer.Y - 1250f);
					safeBox.Width = 2500;
					safeBox.Height = 2500;
					spawnYAdd = 125;
				}
				else if (CalamityWorldPreTrailer.death)
				{
					safeBox.X = spawnX = spawnXReset = (int)(vectorPlayer.X - 1000f);
					spawnX2 = spawnXReset2 = (int)(vectorPlayer.X + 1000f);
					safeBox.Y = spawnY = spawnYReset = (int)(vectorPlayer.Y - 1000f);
					safeBox.Width = 2000;
					safeBox.Height = 2000;
					spawnYAdd = 100;
				}
				else
				{
					safeBox.X = spawnX = spawnXReset = (int)(vectorPlayer.X - 1500f);
					spawnX2 = spawnXReset2 = (int)(vectorPlayer.X + 1500f);
					safeBox.Y = spawnY = spawnYReset = (int)(vectorPlayer.Y - 1500f);
					safeBox.Width = 3000;
					safeBox.Height = 3000;
					spawnYAdd = 150;
				}
				if (Main.netMode != 1)
				{
					int num52 = (int)(safeBox.X + (float)(safeBox.Width / 2)) / 16;
					int num53 = (int)(safeBox.Y + (float)(safeBox.Height / 2)) / 16;
					int num54 = safeBox.Width / 2 / 16 + 1;
					for (int num55 = num52 - num54; num55 <= num52 + num54; num55++)
					{
						for (int num56 = num53 - num54; num56 <= num53 + num54; num56++)
						{
							if ((num55 == num52 - num54 || num55 == num52 + num54 || num56 == num53 - num54 || num56 == num53 + num54) && !Main.tile[num55, num56].HasTile)
							{
								Main.tile[num55, num56].TileType = (ushort)Mod.Find<ModTile>("ArenaTile").Type;
								Main.tile[num55, num56].Get<TileWallWireStateData>().HasTile = true;
							}
							
							Main.tile[num55, num56].LiquidAmount = 0;
							if (Main.netMode == 2)
							{
								NetMessage.SendTileSquare(-1, num55, num56, 1, TileChangeType.None);
							}
							else
							{
								WorldGen.SquareTileFrame(num55, num56, true);
							}
						}
					}
				}
			}
			if (!player.Hitbox.Intersects(safeBox))
			{
				if (uDieLul < 3f)
				{
					uDieLul *= 1.01f;
				}
				else if (uDieLul > 3f)
				{
					uDieLul = 3f;
				}
				protectionBoost = true;
			}
			else
			{
				if (uDieLul > 1f)
				{
					uDieLul *= 0.99f;
				}
				else if (uDieLul < 1f)
				{
					uDieLul = 1f;
				}
				protectionBoost = false;
			}
			#endregion
			#region Despawn
			if (!player.active || player.dead)
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead)
				{
					NPC.velocity = new Vector2(0f, -50f);
					canDespawn = true;
					if (NPC.timeLeft > 150)
					{
						NPC.timeLeft = 150;
					}
					return;
				}
			}
			else
			{
				canDespawn = false;
			}
			#endregion
			#region FirstAttack
			if (NPC.localAI[2] < 900f)
			{
				despawnProj = true;
				NPC.localAI[2] += 1f;
				NPC.damage = 0;
				NPC.chaseable = false;
				NPC.dontTakeDamage = true;
				NPC.TargetClosest(true);
				NPC.velocity *= 0.95f;
				Vector2 vector92 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num740 = player.Center.X - vector92.X;
				float num741 = player.Center.Y - vector92.Y;
				NPC.rotation = (float)Math.Atan2((double)num741, (double)num740) - 1.57f;
				if (Main.netMode != 1)
				{
					NPC.localAI[0] += 1f;
					if (NPC.localAI[0] > ((NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)) ? 4f : 6f))
					{
						NPC.localAI[0] = 0f;
						int damage = expertMode ? 200 : 250; //800 500
						if (NPC.localAI[2] < 300f) //blasts from above
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y - 1000f, 0f, 4f * uDieLul, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						else if (NPC.localAI[2] < 600f) //blasts from left and right
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), -3.5f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), 3.5f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						else //blasts from above, left, and right
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y - 1000f, 0f, 3f * uDieLul, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), -3f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), 3f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						return;
					}
				}
				return;
			}
			else if (!startBattle)
			{
				string key = "Alright, let's get started. Not sure why you're bothering.";
				Color messageColor = Color.Orange;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
				else if (Main.netMode == 2)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
				if (Main.netMode != 1)
				{
					spawnY += 300;
					if (CalamityWorldPreTrailer.bossRushActive)
					{
						spawnY -= 50;
					}
					else if (CalamityWorldPreTrailer.death)
					{
						spawnY -= 100;
					}
					for (int x = 0; x < 5; x++)
					{
						int heart = NPC.NewNPC(NPC.GetSource_FromThis(null), spawnX + 50, spawnY, Mod.Find<ModNPC>("SCalWormHeart").Type, 0, 0f, 0f, 0f, 0f, 255);
						spawnX += spawnXAdd;
						if (Main.netMode == 2)
						{
							NetMessage.SendData(23, -1, -1, null, heart, 0f, 0f, 0f, 0, 0, 0);
						}
						int heart2 = NPC.NewNPC(NPC.GetSource_FromThis(null), spawnX2 - 50, spawnY, Mod.Find<ModNPC>("SCalWormHeart").Type, 0, 0f, 0f, 0f, 0f, 255);
						spawnX2 -= spawnXAdd;
						if (Main.netMode == 2)
						{
							NetMessage.SendData(23, -1, -1, null, heart2, 0f, 0f, 0f, 0, 0, 0);
						}
						spawnY += spawnYAdd;
					}
					spawnX = spawnXReset;
					spawnX2 = spawnXReset2;
					spawnY = spawnYReset;
					NPC.SpawnOnPlayer(NPC.FindClosestPlayer(), Mod.Find<ModNPC>("SCalWormHead").Type);
				}
				startBattle = true;
			}
			#endregion
			#region SecondAttack
			if (NPC.localAI[2] < 1800f && startSecondAttack)
			{
				despawnProj = true;
				NPC.localAI[2] += 1f;
				NPC.damage = 0;
				NPC.chaseable = false;
				NPC.dontTakeDamage = true;
				NPC.TargetClosest(true);
				NPC.velocity *= 0.95f;
				Vector2 vector92 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num740 = player.Center.X - vector92.X;
				float num741 = player.Center.Y - vector92.Y;
				NPC.rotation = (float)Math.Atan2((double)num741, (double)num740) - 1.57f;
				if (Main.netMode != 1)
				{
					int damage = expertMode ? 150 : 200; //600 400
					if (NPC.localAI[2] < 1200f)
					{
						if (NPC.localAI[2] % 180 == 0) //blasts from top
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y - 1000f, 0f, 5f * uDieLul, Mod.Find<ModProjectile>("BrimstoneGigaBlast").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					else if (NPC.localAI[2] < 1500f && NPC.localAI[2] > 1200f)
					{
						if (NPC.localAI[2] % 180 == 0) //blasts from right
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), -5f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneGigaBlast").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					else if (NPC.localAI[2] > 1500f)
					{
						if (NPC.localAI[2] % 180 == 0) //blasts from top
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y - 1000f, 0f, 5f * uDieLul, Mod.Find<ModProjectile>("BrimstoneGigaBlast").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					NPC.localAI[0] += 1f;
					if (NPC.localAI[0] > ((NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)) ? 6f : 9f))
					{
						NPC.localAI[0] = 0f;
						if (NPC.localAI[2] < 1200f) //blasts from below
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y + 1000f, 0f, -4f * uDieLul, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						else if (NPC.localAI[2] < 1500f) //blasts from left
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), 3.5f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						else //blasts from left and right
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), 3f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), -3f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						return;
					}
				}
				return;
			}
			if (!startSecondAttack && ((double)NPC.life <= (double)NPC.lifeMax * 0.75))
			{
				string key = "You seem so confident, even though you are painfully ignorant of what has yet to transpire.";
				Color messageColor = Color.Orange;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
				else if (Main.netMode == 2)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
				startSecondAttack = true;
				return;
			}
			#endregion
			#region ThirdAttack
			if (NPC.localAI[2] < 2700f && startThirdAttack)
			{
				despawnProj = true;
				NPC.localAI[2] += 1f;
				NPC.damage = 0;
				NPC.chaseable = false;
				NPC.dontTakeDamage = true;
				NPC.TargetClosest(true);
				NPC.velocity *= 0.95f;
				Vector2 vector92 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num740 = player.Center.X - vector92.X;
				float num741 = player.Center.Y - vector92.Y;
				NPC.rotation = (float)Math.Atan2((double)num741, (double)num740) - 1.57f;
				if (Main.netMode != 1)
				{
					int damage = expertMode ? 150 : 200;
					if (NPC.localAI[2] % 180 == 0) //blasts from top
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y - 1000f, 0f, 5f * uDieLul, Mod.Find<ModProjectile>("BrimstoneGigaBlast").Type, damage, 0f, Main.myPlayer, 0f, 0f);
					}
					if (NPC.localAI[2] % 240 == 0) //fireblasts from above
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y - 1000f, 0f, 10f * uDieLul, Mod.Find<ModProjectile>("BrimstoneFireblast").Type, damage, 0f, Main.myPlayer, 1f, 0f);
					}
					NPC.localAI[0] += 1f;
					if (NPC.localAI[0] > ((NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)) ? 9f : 11f))
					{
						NPC.localAI[0] = 0f;
						if (NPC.localAI[2] < 2100f) //blasts from above
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y - 1000f, 0f, 4f * uDieLul, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						else if (NPC.localAI[2] < 2400f) //blasts from right
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), -3.5f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						else //blasts from left and right
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), -3.5f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), 3.5f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						return;
					}
				}
				return;
			}
			if (!startThirdAttack && ((double)NPC.life <= (double)NPC.lifeMax * 0.5))
			{
				Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/SCL");
				string key = "Everything was going well until you came along.";
				Color messageColor = Color.Orange;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
				else if (Main.netMode == 2)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
				startThirdAttack = true;
				return;
			}
			#endregion
			#region FourthAttack
			if (NPC.localAI[2] < 3600f && startFourthAttack)
			{
				despawnProj = true;
				NPC.localAI[2] += 1f;
				NPC.damage = 0;
				NPC.chaseable = false;
				NPC.dontTakeDamage = true;
				NPC.TargetClosest(true);
				NPC.velocity *= 0.95f;
				Vector2 vector92 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num740 = player.Center.X - vector92.X;
				float num741 = player.Center.Y - vector92.Y;
				NPC.rotation = (float)Math.Atan2((double)num741, (double)num740) - 1.57f;
				if (Main.netMode != 1) //more clustered attack
				{
					int damage = expertMode ? 150 : 200;
					if (NPC.localAI[2] % 180 == 0) //blasts from top
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y - 1000f, 0f, 5f * uDieLul, Mod.Find<ModProjectile>("BrimstoneGigaBlast").Type, damage, 0f, Main.myPlayer, 0f, 0f);
					}
					if (NPC.localAI[2] % 240 == 0) //fireblasts from above
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y - 1000f, 0f, 10f * uDieLul, Mod.Find<ModProjectile>("BrimstoneFireblast").Type, damage, 0f, Main.myPlayer, 1f, 0f);
					}
					if (NPC.localAI[2] % 450 == 0) //giant homing fireballs
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y - 1000f, 0f, 1f * uDieLul, Mod.Find<ModProjectile>("BrimstoneMonster").Type, damage, 0f, Main.myPlayer, 0f, passedVar);
						passedVar += 1f;
					}
					NPC.localAI[0] += 1f;
					if (NPC.localAI[0] > ((NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)) ? 12f : 15f))
					{
						NPC.localAI[0] = 0f;
						if (NPC.localAI[2] < 3000f) //blasts from below
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y + 1000f, 0f, -4f * uDieLul, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						else if (NPC.localAI[2] < 3300f) //blasts from left
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), 3.5f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						else //blasts from left and right
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), -3.5f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), 3.5f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						return;
					}
				}
				return;
			}
			if (!startFourthAttack && ((double)NPC.life <= (double)NPC.lifeMax * 0.3))
			{
				Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/SCE");
				string key = "Hmm...perhaps I should let the little ones out to play for a while.";
				Color messageColor = Color.Orange;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
				else if (Main.netMode == 2)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
				startFourthAttack = true;
				return;
			}
			#endregion
			#region FifthAttack
			if (NPC.localAI[2] < 4500f && startFifthAttack)
			{
				despawnProj = true;
				NPC.localAI[2] += 1f;
				NPC.damage = 0;
				NPC.chaseable = false;
				NPC.dontTakeDamage = true;
				NPC.TargetClosest(true);
				NPC.velocity *= 0.95f;
				Vector2 vector92 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num740 = player.Center.X - vector92.X;
				float num741 = player.Center.Y - vector92.Y;
				NPC.rotation = (float)Math.Atan2((double)num741, (double)num740) - 1.57f;
				if (Main.netMode != 1)
				{
					int damage = expertMode ? 150 : 200;
					if (NPC.localAI[2] % 240 == 0) //blasts from top
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y - 1000f, 0f, 5f * uDieLul, Mod.Find<ModProjectile>("BrimstoneGigaBlast").Type, damage, 0f, Main.myPlayer, 0f, 0f);
					}
					if (NPC.localAI[2] % 360 == 0) //fireblasts from above
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y - 1000f, 0f, 10f * uDieLul, Mod.Find<ModProjectile>("BrimstoneFireblast").Type, damage, 0f, Main.myPlayer, 1f, 0f);
					}
					if (NPC.localAI[2] % 450 == 0) //giant homing fireballs
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y - 1000f, 0f, 1f * uDieLul, Mod.Find<ModProjectile>("BrimstoneMonster").Type, damage, 0f, Main.myPlayer, 0f, passedVar);
						passedVar += 1f;
					}
					if (NPC.localAI[2] % 30 == 0) //projectiles that move in wave pattern
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-500, 500), -10f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneWave").Type, damage, 0f, Main.myPlayer, 0f, 0f);
					}
					NPC.localAI[0] += 1f;
					if (NPC.localAI[0] > ((NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)) ? 15f : 18f))
					{
						NPC.localAI[0] = 0f;
						if (NPC.localAI[2] < 3900f) //blasts from above
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y - 1000f, 0f, 4f * uDieLul, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						else if (NPC.localAI[2] < 4200f) //blasts from left and right
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), -3.5f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), 3.5f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						else //blasts from above, left, and right
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y - 1000f, 0f, 3f * uDieLul, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), -3f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1000), 3f * uDieLul, 0f, Mod.Find<ModProjectile>("BrimstoneHellblast2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						return;
					}
				}
				return;
			}
			if (!startFifthAttack && ((double)NPC.life <= (double)NPC.lifeMax * 0.1))
			{
				string key = "I'm just getting started!";
				Color messageColor = Color.Orange;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
				else if (Main.netMode == 2)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
				startFifthAttack = true;
				return;
			}
			#endregion
			#region EndSections
			if (startFifthAttack)
			{
				if (gettingTired5)
				{
					Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/SCA");
					NPC.noGravity = false;
					NPC.noTileCollide = false;
					NPC.damage = 0;
					NPC.velocity.X *= 0.98f;
					NPC.velocity.Y = 5f;
					Vector2 vector2 = new Vector2(NPC.Center.X, NPC.Center.Y);
					float num = player.Center.X - vector2.X;
					float num1 = player.Center.Y - vector2.Y;
					NPC.rotation = (float)Math.Atan2((double)num1, (double)num) - 1.57f;
					if (player.GetModPlayer<CalamityPlayerPreTrailer>().sCalKillCount > 0) //after first time you kill her
					{
						if (giveUpCounter == 900)
						{
							string key = "Perhaps one of these times I'll change my mind...";
							Color messageColor = Color.Orange;
							if (Main.netMode == 0)
							{
								Main.NewText(Language.GetTextValue(key), messageColor);
							}
							else if (Main.netMode == 2)
							{
								ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
							}
						}
						giveUpCounter--;
						NPC.chaseable = (giveUpCounter < 900) ? true : false;
						NPC.dontTakeDamage = (giveUpCounter < 900) ? false : true;
						return;
					}
					if (giveUpCounter == 600)
					{
						string key = "He has grown far stronger since we last fought...you stand no chance.";
						Color messageColor = Color.Orange;
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
					}
					if (giveUpCounter == 300)
					{
						string key = "Well...I suppose this is the end...";
						Color messageColor = Color.Orange;
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
					}
					if (giveUpCounter <= 0)
					{
						NPC.chaseable = true;
						NPC.dontTakeDamage = false;
						return;
					}
					giveUpCounter--;
					NPC.chaseable = false;
					NPC.dontTakeDamage = true;
					return;
				}
				if (!gettingTired5 && ((double)NPC.life <= (double)NPC.lifeMax * 0.01))
				{
					string key = "Not even I could defeat him! What hope do you have!?";
					Color messageColor = Color.Orange;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
					gettingTired5 = true;
					return;
				}
				else if (!gettingTired4 && ((double)NPC.life <= (double)NPC.lifeMax * 0.02))
				{
					string key = "He has never lost a battle!";
					Color messageColor = Color.Orange;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
					gettingTired4 = true;
					return;
				}
				else if (!gettingTired3 && ((double)NPC.life <= (double)NPC.lifeMax * 0.04))
				{
					string key = "Even if you defeat me you would still have the lord to contend with!";
					Color messageColor = Color.Orange;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
					gettingTired3 = true;
					return;
				}
				else if (!gettingTired2 && ((double)NPC.life <= (double)NPC.lifeMax * 0.06))
				{
					string key = "Just stop!";
					Color messageColor = Color.Orange;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
					gettingTired2 = true;
					return;
				}
				else if (!gettingTired && ((double)NPC.life <= (double)NPC.lifeMax * 0.08))
				{
					string key = "How are you still alive!?";
					Color messageColor = Color.Orange;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
					if (Main.netMode != 1)
					{
						spawnY += 300;
						if (CalamityWorldPreTrailer.bossRushActive)
						{
							spawnY -= 50;
						}
						else if (CalamityWorldPreTrailer.death)
						{
							spawnY -= 100;
						}
						for (int x = 0; x < 5; x++)
						{
							int heart = NPC.NewNPC(NPC.GetSource_FromThis(null), spawnX + 50, spawnY, Mod.Find<ModNPC>("SCalWormHeart").Type, 0, 0f, 0f, 0f, 0f, 255);
							spawnX += spawnXAdd;
							if (Main.netMode == 2)
							{
								NetMessage.SendData(23, -1, -1, null, heart, 0f, 0f, 0f, 0, 0, 0);
							}
							int heart2 = NPC.NewNPC(NPC.GetSource_FromThis(null), spawnX2 - 50, spawnY, Mod.Find<ModNPC>("SCalWormHeart").Type, 0, 0f, 0f, 0f, 0f, 255);
							spawnX2 -= spawnXAdd;
							if (Main.netMode == 2)
							{
								NetMessage.SendData(23, -1, -1, null, heart2, 0f, 0f, 0f, 0, 0, 0);
							}
							spawnY += spawnYAdd;
						}
						spawnX = spawnXReset;
						spawnX2 = spawnXReset2;
						spawnY = spawnYReset;
						NPC.SpawnOnPlayer(NPC.FindClosestPlayer(), Mod.Find<ModNPC>("SCalWormHead").Type);
					}
					gettingTired = true;
					return;
				}
			}
			#endregion
			#region DespawnProjectiles
			if (NPC.localAI[2] % 900 == 0 && despawnProj)
			{
				int proj;
				for (int x = 0; x < 1000; x = proj + 1)
				{
					Projectile projectile = Main.projectile[x];
					if (projectile.active)
					{
						if (projectile.type == Mod.Find<ModProjectile>("BrimstoneHellblast2").Type ||
							projectile.type == Mod.Find<ModProjectile>("BrimstoneBarrage").Type ||
							projectile.type == Mod.Find<ModProjectile>("BrimstoneWave").Type)
						{
							projectile.Kill();
						}
						else if (projectile.type == Mod.Find<ModProjectile>("BrimstoneGigaBlast").Type ||
							projectile.type == Mod.Find<ModProjectile>("BrimstoneFireblast").Type)
						{
							projectile.active = false;
						}
					}
					proj = x;
				}
				despawnProj = false;
			}
			#endregion
			#region TransformSeekerandBrotherTriggers
			if (!halfLife && ((double)NPC.life <= (double)NPC.lifeMax * 0.4))
			{
				string key = "Don't worry, I still have plenty of tricks left.";
				Color messageColor = Color.Orange;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
				else if (Main.netMode == 2)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
				halfLife = true;
			}
			if ((double)NPC.life <= (double)NPC.lifeMax * 0.2)
			{
				if (secondStage == false)
				{
					string key = "Impressive...but still not good enough!";
					Color messageColor = Color.Orange;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
					if (Main.netMode != 1)
					{
						SoundEngine.PlaySound(SoundID.Item74, NPC.position);
						for (int I = 0; I < 20; I++)
						{
							int FireEye = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)(NPC.Center.X + (Math.Sin(I * 18) * 300)), (int)(NPC.Center.Y + (Math.Cos(I * 18) * 300)), Mod.Find<ModNPC>("SoulSeekerSupreme").Type, NPC.whoAmI, 0, 0, 0, -1);
							NPC Eye = Main.npc[FireEye];
							Eye.ai[0] = I * 18;
							Eye.ai[3] = I * 18;
						}
					}
					secondStage = true;
				}
			}
			if (bossLife == 0f && NPC.life > 0)
			{
				bossLife = (float)NPC.lifeMax;
			}
			if (NPC.life > 0)
			{
				if (Main.netMode != 1)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.55);
					if ((float)(NPC.life + num660) < bossLife)
					{
						bossLife = (float)NPC.life;
						NPC.SpawnOnPlayer(NPC.FindClosestPlayer(), Mod.Find<ModNPC>("SupremeCataclysm").Type);
						NPC.SpawnOnPlayer(NPC.FindClosestPlayer(), Mod.Find<ModNPC>("SupremeCatastrophe").Type);
						string key = "Brothers, could you assist me for a moment? This ordeal is growing tiresome.";
						Color messageColor = Color.Orange;
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
						return;
					}
				}
			}
			#endregion
			#region TargetandRotation
			if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
			{
				NPC.TargetClosest(true);
			}
			float num801 = NPC.position.X + (float)(NPC.width / 2) - player.position.X - (float)(player.width / 2);
			float num802 = NPC.position.Y + (float)NPC.height - 59f - player.position.Y - (float)(player.height / 2);
			float num803 = (float)Math.Atan2((double)num802, (double)num801) + 1.57f;
			if (num803 < 0f)
			{
				num803 += 6.283f;
			}
			else if ((double)num803 > 6.283)
			{
				num803 -= 6.283f;
			}
			float num804 = 0.1f;
			if (NPC.rotation < num803)
			{
				if ((double)(num803 - NPC.rotation) > 3.1415)
				{
					NPC.rotation -= num804;
				}
				else
				{
					NPC.rotation += num804;
				}
			}
			else if (NPC.rotation > num803)
			{
				if ((double)(NPC.rotation - num803) > 3.1415)
				{
					NPC.rotation += num804;
				}
				else
				{
					NPC.rotation -= num804;
				}
			}
			if (NPC.rotation > num803 - num804 && NPC.rotation < num803 + num804)
			{
				NPC.rotation = num803;
			}
			if (NPC.rotation < 0f)
			{
				NPC.rotation += 6.283f;
			}
			else if ((double)NPC.rotation > 6.283)
			{
				NPC.rotation -= 6.283f;
			}
			if (NPC.rotation > num803 - num804 && NPC.rotation < num803 + num804)
			{
				NPC.rotation = num803;
			}
			#endregion
			#region FirstStage
			if (NPC.ai[0] == 0f)
			{
				NPC.damage = expertMode ? 720 : 450;
				if (NPC.AnyNPCs(Mod.Find<ModNPC>("SCalWormHead").Type))
				{
					wormAlive = true;
					NPC.dontTakeDamage = true;
					NPC.chaseable = false;
				}
				else
				{
					if (NPC.AnyNPCs(Mod.Find<ModNPC>("SupremeCataclysm").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("SupremeCatastrophe").Type))
					{
						NPC.dontTakeDamage = true;
						NPC.chaseable = false;
						NPC.damage = 0;
						NPC.TargetClosest(true);
						NPC.velocity *= 0.95f;
						Vector2 vector92 = new Vector2(NPC.Center.X, NPC.Center.Y);
						float num740 = player.Center.X - vector92.X;
						float num741 = player.Center.Y - vector92.Y;
						NPC.rotation = (float)Math.Atan2((double)num741, (double)num740) - 1.57f;
						return;
					}
					else
					{
						NPC.dontTakeDamage = false;
						NPC.chaseable = true;
					}
					wormAlive = false;
				}
				if (NPC.ai[1] == 0f)
				{
					float num823 = 12f;
					float num824 = 0.12f;
					Vector2 vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num825 = player.position.X + (float)(player.width / 2) - vector82.X;
					float num826 = player.position.Y + (float)(player.height / 2) - 550f - vector82.Y;
					float num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
					num827 = num823 / num827;
					num825 *= num827;
					num826 *= num827;
					if (NPC.velocity.X < num825)
					{
						NPC.velocity.X = NPC.velocity.X + num824;
						if (NPC.velocity.X < 0f && num825 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num824;
						}
					}
					else if (NPC.velocity.X > num825)
					{
						NPC.velocity.X = NPC.velocity.X - num824;
						if (NPC.velocity.X > 0f && num825 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num824;
						}
					}
					if (NPC.velocity.Y < num826)
					{
						NPC.velocity.Y = NPC.velocity.Y + num824;
						if (NPC.velocity.Y < 0f && num826 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num824;
						}
					}
					else if (NPC.velocity.Y > num826)
					{
						NPC.velocity.Y = NPC.velocity.Y - num824;
						if (NPC.velocity.Y > 0f && num826 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num824;
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 300f)
					{
						NPC.ai[1] = -1f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
					}
					vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					num825 = player.position.X + (float)(player.width / 2) - vector82.X;
					num826 = player.position.Y + (float)(player.height / 2) - vector82.Y;
					NPC.rotation = (float)Math.Atan2((double)num826, (double)num825) - 1.57f;
					if (Main.netMode != 1)
					{
						NPC.localAI[1] += wormAlive ? 0.5f : 1f;
						if (NPC.localAI[1] > 90f)
						{
							NPC.localAI[1] = 0f;
							float num828 = 10f * uDieLul;
							int num829 = expertMode ? 150 : 200; //600 400
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
							float num181 = Math.Abs(num180) * 0.1f;
							float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
							float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							num183 = num828 / num183;
							num180 *= num183;
							num182 *= num183;
							value9.X += num180;
							value9.Y += num182;
							int randomShot = Main.rand.Next(6); //0 to 5
							if (randomShot == 0 && canFireSplitingFireball)
							{
								canFireSplitingFireball = false;
								randomShot = Mod.Find<ModProjectile>("BrimstoneFireblast").Type;
								num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
								num827 = num828 / num827;
								num825 *= num827;
								num826 *= num827;
								vector82.X += num825 * 15f;
								vector82.Y += num826 * 15f;
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector82.X, vector82.Y, num825, num826, randomShot, num829, 0f, Main.myPlayer, 0f, 0f);
							}
							else if (randomShot == 1 && canFireSplitingFireball)
							{
								canFireSplitingFireball = false;
								randomShot = Mod.Find<ModProjectile>("BrimstoneGigaBlast").Type;
								num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
								num827 = num828 / num827;
								num825 *= num827;
								num826 *= num827;
								vector82.X += num825 * 15f;
								vector82.Y += num826 * 15f;
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector82.X, vector82.Y, num825, num826, randomShot, num829, 0f, Main.myPlayer, 0f, 0f);
							}
							else
							{
								canFireSplitingFireball = true;
								randomShot = Mod.Find<ModProjectile>("BrimstoneBarrage").Type;
								for (int num186 = 1; num186 <= 8; num186++)
								{
									num180 = player.position.X + (float)player.width * 0.5f - value9.X;
									num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
									num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									float speedBoost = (float)(num186 > 4 ? -(num186 - 4) : num186);
									num183 = (8f + speedBoost) / num183;
									num180 *= num183;
									num182 *= num183;
									Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, num180 + speedBoost, num182 + speedBoost, randomShot, num829, 0f, Main.myPlayer, 0f, 0f);
								}
							}
							return;
						}
					}
				}
				else if (NPC.ai[1] == 1f)
				{
					NPC.rotation = num803; //change
					float num383 = wormAlive ? 26f : 30f;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.95)
					{
						num383 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.85)
					{
						num383 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.7)
					{
						num383 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.6)
					{
						num383 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
					{
						num383 += 1f;
					}
					Vector2 vector37 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num384 = player.position.X + (float)(player.width / 2) - vector37.X;
					float num385 = player.position.Y + (float)(player.height / 2) - vector37.Y;
					float num386 = (float)Math.Sqrt((double)(num384 * num384 + num385 * num385));
					num386 = num383 / num386;
					NPC.velocity.X = num384 * num386;
					NPC.velocity.Y = num385 * num386;
					NPC.ai[1] = 2f;
				}
				else if (NPC.ai[1] == 2f)
				{
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 25f)
					{
						NPC.velocity.X = NPC.velocity.X * 0.96f;
						NPC.velocity.Y = NPC.velocity.Y * 0.96f;
						if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
						{
							NPC.velocity.X = 0f;
						}
						if ((double)NPC.velocity.Y > -0.1 && (double)NPC.velocity.Y < 0.1)
						{
							NPC.velocity.Y = 0f;
						}
					}
					else
					{
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
					}
					if (NPC.ai[2] >= 70f)
					{
						NPC.ai[3] += 1f;
						NPC.ai[2] = 0f;
						NPC.target = 255;
						NPC.rotation = num803;
						if (NPC.ai[3] >= 2f)
						{
							NPC.ai[1] = -1f;
						}
						else
						{
							NPC.ai[1] = 1f;
						}
					}
				}
				else if (NPC.ai[1] == 3f)
				{
					NPC.TargetClosest(true);
					float num412 = 32f;
					float num413 = 1.2f;
					int num414 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)player.width)
					{
						num414 = -1;
					}
					Vector2 vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num415 = player.position.X + (float)(player.width / 2) + (float)(num414 * 600) - vector40.X;
					float num416 = player.position.Y + (float)(player.height / 2) - vector40.Y;
					float num417 = (float)Math.Sqrt((double)(num415 * num415 + num416 * num416));
					num417 = num412 / num417;
					num415 *= num417;
					num416 *= num417;
					if (NPC.velocity.X < num415)
					{
						NPC.velocity.X = NPC.velocity.X + num413;
						if (NPC.velocity.X < 0f && num415 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num413;
						}
					}
					else if (NPC.velocity.X > num415)
					{
						NPC.velocity.X = NPC.velocity.X - num413;
						if (NPC.velocity.X > 0f && num415 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num413;
						}
					}
					if (NPC.velocity.Y < num416)
					{
						NPC.velocity.Y = NPC.velocity.Y + num413;
						if (NPC.velocity.Y < 0f && num416 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num413;
						}
					}
					else if (NPC.velocity.Y > num416)
					{
						NPC.velocity.Y = NPC.velocity.Y - num413;
						if (NPC.velocity.Y > 0f && num416 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num413;
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 480f)
					{
						NPC.ai[1] = -1f;
						NPC.target = 255;
						NPC.netUpdate = true;
					}
					else
					{
						if (!player.dead)
						{
							NPC.ai[3] += wormAlive ? 0.5f : 1f;
						}
						if (NPC.ai[3] >= 20f)
						{
							NPC.ai[3] = 0f;
							vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							num415 = player.position.X + (float)(player.width / 2) - vector40.X;
							num416 = player.position.Y + (float)(player.height / 2) - vector40.Y;
							if (Main.netMode != 1)
							{
								float num418 = 10f * uDieLul;
								int num419 = expertMode ? 150 : 200; //600 500
								int num420 = Mod.Find<ModProjectile>("BrimstoneHellblast").Type;
								num417 = (float)Math.Sqrt((double)(num415 * num415 + num416 * num416));
								num417 = num418 / num417;
								num415 *= num417;
								num416 *= num417;
								vector40.X += num415 * 4f;
								vector40.Y += num416 * 4f;
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector40.X, vector40.Y, num415, num416, num420, num419, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				}
				else if (NPC.ai[1] == 4f)
				{
					int num831 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)player.width)
					{
						num831 = -1;
					}
					float num832 = 32f;
					float num833 = 1.2f;
					Vector2 vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num834 = player.position.X + (float)(player.width / 2) + (float)(num831 * 750) - vector83.X; //600
					float num835 = player.position.Y + (float)(player.height / 2) - vector83.Y;
					float num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
					num836 = num832 / num836;
					num834 *= num836;
					num835 *= num836;
					if (NPC.velocity.X < num834)
					{
						NPC.velocity.X = NPC.velocity.X + num833;
						if (NPC.velocity.X < 0f && num834 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num833;
						}
					}
					else if (NPC.velocity.X > num834)
					{
						NPC.velocity.X = NPC.velocity.X - num833;
						if (NPC.velocity.X > 0f && num834 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num833;
						}
					}
					if (NPC.velocity.Y < num835)
					{
						NPC.velocity.Y = NPC.velocity.Y + num833;
						if (NPC.velocity.Y < 0f && num835 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num833;
						}
					}
					else if (NPC.velocity.Y > num835)
					{
						NPC.velocity.Y = NPC.velocity.Y - num833;
						if (NPC.velocity.Y > 0f && num835 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num833;
						}
					}
					vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					num834 = player.position.X + (float)(player.width / 2) - vector83.X;
					num835 = player.position.Y + (float)(player.height / 2) - vector83.Y;
					NPC.rotation = (float)Math.Atan2((double)num835, (double)num834) - 1.57f;
					if (Main.netMode != 1)
					{
						NPC.localAI[1] += wormAlive ? 0.5f : 1f;
						if (NPC.localAI[1] > 140f)
						{
							NPC.localAI[1] = 0f;
							float num837 = 5f * uDieLul;
							int num838 = expertMode ? 150 : 200; //600 500
							int num839 = Mod.Find<ModProjectile>("BrimstoneFireblast").Type;
							num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
							num836 = num837 / num836;
							num834 *= num836;
							num835 *= num836;
							vector83.X += num834 * 15f;
							vector83.Y += num835 * 15f;
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector83.X, vector83.Y, num834, num835, num839, num838, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 300f)
					{
						NPC.ai[1] = -1f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
						return;
					}
				}
				if (NPC.ai[1] == -1f)
				{
					phaseChange++;
					if (phaseChange > 23)
					{
						phaseChange = 0;
					}
					int phase = 0; //0 = shots above 1 = charge 2 = nothing 3 = hellblasts 4 = fireblasts
					switch (phaseChange)
					{
						case 0: phase = 0; willCharge = false; break; //0341
						case 1: phase = 3; break;
						case 2: phase = 4; willCharge = true; break;
						case 3: phase = 1; break;
						case 4: phase = 1; break; //1430
						case 5: phase = 4; willCharge = false; break;
						case 6: phase = 3; break;
						case 7: phase = 0; willCharge = true; break;
						case 8: phase = 1; break; //1034
						case 9: phase = 0; willCharge = false; break;
						case 10: phase = 3; break;
						case 11: phase = 4; break;
						case 12: phase = 4; break; //4310
						case 13: phase = 3; willCharge = true; break;
						case 14: phase = 1; break;
						case 15: phase = 0; willCharge = false; break;
						case 16: phase = 4; break; //4411
						case 17: phase = 4; willCharge = true; break;
						case 18: phase = 1; break;
						case 19: phase = 1; break;
						case 20: phase = 0; break; //0101
						case 21: phase = 1; break;
						case 22: phase = 0; break;
						case 23: phase = 1; break;
					}
					NPC.ai[1] = (float)phase;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					return;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.4)
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			#endregion
			#region Transition
			else if (NPC.ai[0] == 1f || NPC.ai[0] == 2f)
			{
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
				if (NPC.ai[0] == 1f)
				{
					NPC.ai[2] += 0.005f;
					if ((double)NPC.ai[2] > 0.5)
					{
						NPC.ai[2] = 0.5f;
					}
				}
				else
				{
					NPC.ai[2] -= 0.005f;
					if (NPC.ai[2] < 0f)
					{
						NPC.ai[2] = 0f;
					}
				}
				NPC.rotation += NPC.ai[2];
				NPC.ai[1] += 1f;
				if (NPC.ai[1] == 100f)
				{
					NPC.ai[0] += 1f;
					NPC.ai[1] = 0f;
					if (NPC.ai[0] == 3f)
					{
						NPC.ai[2] = 0f;
					}
					else
					{
						for (int num388 = 0; num388 < 50; num388++)
						{
							Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
						}
						SoundEngine.PlaySound(SoundID.Roar, NPC.position);
					}
				}
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
				NPC.velocity.X = NPC.velocity.X * 0.98f;
				NPC.velocity.Y = NPC.velocity.Y * 0.98f;
				if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
				{
					NPC.velocity.X = 0f;
				}
				if ((double)NPC.velocity.Y > -0.1 && (double)NPC.velocity.Y < 0.1)
				{
					NPC.velocity.Y = 0f;
					return;
				}
			}
			#endregion
			#region LastStage
			else
			{
				NPC.damage = expertMode ? 720 : 450;
				if (NPC.AnyNPCs(Mod.Find<ModNPC>("SCalWormHead").Type))
				{
					wormAlive = true;
					NPC.dontTakeDamage = true;
					NPC.chaseable = false;
				}
				else
				{
					if (NPC.AnyNPCs(Mod.Find<ModNPC>("SoulSeekerSupreme").Type))
					{
						NPC.dontTakeDamage = true;
						NPC.chaseable = false;
					}
					else
					{
						NPC.dontTakeDamage = false;
						NPC.chaseable = true;
					}
					wormAlive = false;
				}
				if (NPC.ai[1] == 0f)
				{
					float num823 = 12f;
					float num824 = 0.12f;
					Vector2 vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num825 = player.position.X + (float)(player.width / 2) - vector82.X;
					float num826 = player.position.Y + (float)(player.height / 2) - 550f - vector82.Y;
					float num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
					num827 = num823 / num827;
					num825 *= num827;
					num826 *= num827;
					if (NPC.velocity.X < num825)
					{
						NPC.velocity.X = NPC.velocity.X + num824;
						if (NPC.velocity.X < 0f && num825 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num824;
						}
					}
					else if (NPC.velocity.X > num825)
					{
						NPC.velocity.X = NPC.velocity.X - num824;
						if (NPC.velocity.X > 0f && num825 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num824;
						}
					}
					if (NPC.velocity.Y < num826)
					{
						NPC.velocity.Y = NPC.velocity.Y + num824;
						if (NPC.velocity.Y < 0f && num826 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num824;
						}
					}
					else if (NPC.velocity.Y > num826)
					{
						NPC.velocity.Y = NPC.velocity.Y - num824;
						if (NPC.velocity.Y > 0f && num826 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num824;
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 240f)
					{
						NPC.ai[1] = -1f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
					}
					vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					num825 = player.position.X + (float)(player.width / 2) - vector82.X;
					num826 = player.position.Y + (float)(player.height / 2) - vector82.Y;
					NPC.rotation = (float)Math.Atan2((double)num826, (double)num825) - 1.57f;
					if (Main.netMode != 1)
					{
						NPC.localAI[1] += wormAlive ? 0.5f : 1f;
						if (NPC.localAI[1] > 60f)
						{
							NPC.localAI[1] = 0f;
							float num828 = 10f * uDieLul;
							int num829 = expertMode ? 150 : 200; //600 500
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
							float num181 = Math.Abs(num180) * 0.1f;
							float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
							float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							num183 = num828 / num183;
							num180 *= num183;
							num182 *= num183;
							value9.X += num180;
							value9.Y += num182;
							int randomShot = Main.rand.Next(6);
							if (randomShot == 0 && canFireSplitingFireball)
							{
								canFireSplitingFireball = false;
								randomShot = Mod.Find<ModProjectile>("BrimstoneFireblast").Type;
								num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
								num827 = num828 / num827;
								num825 *= num827;
								num826 *= num827;
								vector82.X += num825 * 15f;
								vector82.Y += num826 * 15f;
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector82.X, vector82.Y, num825, num826, randomShot, num829, 0f, Main.myPlayer, 0f, 0f);
							}
							else if (randomShot == 1 && canFireSplitingFireball)
							{
								canFireSplitingFireball = false;
								randomShot = Mod.Find<ModProjectile>("BrimstoneGigaBlast").Type;
								num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
								num827 = num828 / num827;
								num825 *= num827;
								num826 *= num827;
								vector82.X += num825 * 15f;
								vector82.Y += num826 * 15f;
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector82.X, vector82.Y, num825, num826, randomShot, num829, 0f, Main.myPlayer, 0f, 0f);
							}
							else
							{
								canFireSplitingFireball = true;
								randomShot = Mod.Find<ModProjectile>("BrimstoneBarrage").Type;
								for (int num186 = 1; num186 <= 8; num186++)
								{
									num180 = player.position.X + (float)player.width * 0.5f - value9.X;
									num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
									num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									float speedBoost = (float)(num186 > 4 ? -(num186 - 4) : num186);
									num183 = (8f + speedBoost) / num183;
									num180 *= num183;
									num182 *= num183;
									Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, num180 + speedBoost, num182 + speedBoost, randomShot, num829, 0f, Main.myPlayer, 0f, 0f);
								}
							}
							return;
						}
					}
				}
				else if (NPC.ai[1] == 1f)
				{
					NPC.rotation = num803; //change
					float num383 = wormAlive ? 31f : 35f;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.3)
					{
						num383 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.2)
					{
						num383 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
					{
						num383 += 1f;
					}
					Vector2 vector37 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num384 = player.position.X + (float)(player.width / 2) - vector37.X;
					float num385 = player.position.Y + (float)(player.height / 2) - vector37.Y;
					float num386 = (float)Math.Sqrt((double)(num384 * num384 + num385 * num385));
					num386 = num383 / num386;
					NPC.velocity.X = num384 * num386;
					NPC.velocity.Y = num385 * num386;
					NPC.ai[1] = 2f;
				}
				else if (NPC.ai[1] == 2f)
				{
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 25f)
					{
						NPC.velocity.X = NPC.velocity.X * 0.96f;
						NPC.velocity.Y = NPC.velocity.Y * 0.96f;
						if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
						{
							NPC.velocity.X = 0f;
						}
						if ((double)NPC.velocity.Y > -0.1 && (double)NPC.velocity.Y < 0.1)
						{
							NPC.velocity.Y = 0f;
						}
					}
					else
					{
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
					}
					if (NPC.ai[2] >= 70f)
					{
						NPC.ai[3] += 1f;
						NPC.ai[2] = 0f;
						NPC.target = 255;
						NPC.rotation = num803; //change
						if (NPC.ai[3] >= 1f)
						{
							NPC.ai[1] = -1f;
						}
						else
						{
							NPC.ai[1] = 1f;
						}
					}
				}
				else if (NPC.ai[1] == 3f)
				{
					NPC.TargetClosest(true);
					float num412 = 32f;
					float num413 = 1.2f;
					int num414 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)player.width)
					{
						num414 = -1;
					}
					Vector2 vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num415 = player.position.X + (float)(player.width / 2) + (float)(num414 * 600) - vector40.X;
					float num416 = player.position.Y + (float)(player.height / 2) - vector40.Y;
					float num417 = (float)Math.Sqrt((double)(num415 * num415 + num416 * num416));
					num417 = num412 / num417;
					num415 *= num417;
					num416 *= num417;
					if (NPC.velocity.X < num415)
					{
						NPC.velocity.X = NPC.velocity.X + num413;
						if (NPC.velocity.X < 0f && num415 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num413;
						}
					}
					else if (NPC.velocity.X > num415)
					{
						NPC.velocity.X = NPC.velocity.X - num413;
						if (NPC.velocity.X > 0f && num415 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num413;
						}
					}
					if (NPC.velocity.Y < num416)
					{
						NPC.velocity.Y = NPC.velocity.Y + num413;
						if (NPC.velocity.Y < 0f && num416 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num413;
						}
					}
					else if (NPC.velocity.Y > num416)
					{
						NPC.velocity.Y = NPC.velocity.Y - num413;
						if (NPC.velocity.Y > 0f && num416 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num413;
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 300f)
					{
						NPC.ai[1] = -1f;
						NPC.target = 255;
						NPC.netUpdate = true;
					}
					else
					{
						if (!player.dead)
						{
							NPC.ai[3] += wormAlive ? 0.5f : 1f;
						}
						if (NPC.ai[3] >= 24f)
						{
							NPC.ai[3] = 0f;
							vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							num415 = player.position.X + (float)(player.width / 2) - vector40.X;
							num416 = player.position.Y + (float)(player.height / 2) - vector40.Y;
							if (Main.netMode != 1)
							{
								float num418 = 10f * uDieLul;
								int num419 = expertMode ? 150 : 200; //600 500
								int num420 = Mod.Find<ModProjectile>("BrimstoneHellblast").Type;
								num417 = (float)Math.Sqrt((double)(num415 * num415 + num416 * num416));
								num417 = num418 / num417;
								num415 *= num417;
								num416 *= num417;
								vector40.X += num415 * 4f;
								vector40.Y += num416 * 4f;
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector40.X, vector40.Y, num415, num416, num420, num419, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				}
				else if (NPC.ai[1] == 4f)
				{
					int num831 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)player.width)
					{
						num831 = -1;
					}
					float num832 = 32f;
					float num833 = 1.2f;
					Vector2 vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num834 = player.position.X + (float)(player.width / 2) + (float)(num831 * 750) - vector83.X; //600
					float num835 = player.position.Y + (float)(player.height / 2) - vector83.Y;
					float num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
					num836 = num832 / num836;
					num834 *= num836;
					num835 *= num836;
					if (NPC.velocity.X < num834)
					{
						NPC.velocity.X = NPC.velocity.X + num833;
						if (NPC.velocity.X < 0f && num834 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num833;
						}
					}
					else if (NPC.velocity.X > num834)
					{
						NPC.velocity.X = NPC.velocity.X - num833;
						if (NPC.velocity.X > 0f && num834 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num833;
						}
					}
					if (NPC.velocity.Y < num835)
					{
						NPC.velocity.Y = NPC.velocity.Y + num833;
						if (NPC.velocity.Y < 0f && num835 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num833;
						}
					}
					else if (NPC.velocity.Y > num835)
					{
						NPC.velocity.Y = NPC.velocity.Y - num833;
						if (NPC.velocity.Y > 0f && num835 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num833;
						}
					}
					vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					num834 = player.position.X + (float)(player.width / 2) - vector83.X;
					num835 = player.position.Y + (float)(player.height / 2) - vector83.Y;
					NPC.rotation = (float)Math.Atan2((double)num835, (double)num834) - 1.57f;
					if (Main.netMode != 1)
					{
						NPC.localAI[1] += wormAlive ? 0.5f : 1f;
						if (NPC.localAI[1] > 100f)
						{
							NPC.localAI[1] = 0f;
							float num837 = 5f * uDieLul;
							int num838 = expertMode ? 150 : 200; //600 500
							int num839 = Mod.Find<ModProjectile>("BrimstoneFireblast").Type;
							num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
							num836 = num837 / num836;
							num834 *= num836;
							num835 *= num836;
							vector83.X += num834 * 15f;
							vector83.Y += num835 * 15f;
							int shot = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector83.X, vector83.Y, num834, num835, num839, num838, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 240f)
					{
						NPC.ai[1] = -1f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
						return;
					}
				}
				if (NPC.ai[1] == -1f)
				{
					phaseChange++;
					if (phaseChange > 23)
					{
						phaseChange = 0;
					}
					int phase = 0; //0 = shots above 1 = charge 2 = nothing 3 = hellblasts 4 = fireblasts
					switch (phaseChange)
					{
						case 0: phase = 0; willCharge = false; break; //0341
						case 1: phase = 3; break;
						case 2: phase = 4; willCharge = true; break;
						case 3: phase = 1; break;
						case 4: phase = 1; break; //1430
						case 5: phase = 4; willCharge = false; break;
						case 6: phase = 3; break;
						case 7: phase = 0; willCharge = true; break;
						case 8: phase = 1; break; //1034
						case 9: phase = 0; willCharge = false; break;
						case 10: phase = 3; break;
						case 11: phase = 4; break;
						case 12: phase = 4; break; //4310
						case 13: phase = 3; willCharge = true; break;
						case 14: phase = 1; break;
						case 15: phase = 0; willCharge = false; break;
						case 16: phase = 4; break; //4411
						case 17: phase = 4; willCharge = true; break;
						case 18: phase = 1; break;
						case 19: phase = 1; break;
						case 20: phase = 0; break; //0101
						case 21: phase = 1; break;
						case 22: phase = 0; break;
						case 23: phase = 1; break;
					}
					NPC.ai[1] = (float)phase;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					return;
				}
			}
			#endregion
		}

		#region Loot
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule isExpert = new LeadingConditionRule(new Conditions.IsExpert());
			
			npcLoot.Add(ItemDropRule.ByCondition(new SpecialSCalItem(), Mod.Find<ModItem>("CheatTestThing").Type, 1));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("CalamitousEssence").Type, 1, 30, 41));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("CalamitousEssence").Type, 1, 20, 31));
			npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), Mod.Find<ModItem>("Vehemenc").Type, 1));
			isExpert.OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				ModContent.ItemType<Animus>(),
				ModContent.ItemType<Azathoth>(),
				ModContent.ItemType<Contagion>(),
				ModContent.ItemType<DraconicDestruction>(),
				ModContent.ItemType<Earth>(),
				ModContent.ItemType<Megafleet>(),
				ModContent.ItemType<RedSun>(),
				ModContent.ItemType<RoyalKnives>(),
				ModContent.ItemType<RoyalKnivesMelee>(),
				ModContent.ItemType<Svantechnical>(),
				ModContent.ItemType<TriactisTruePaladinianMageHammerofMight>(),
				ModContent.ItemType<TriactisTruePaladinianMageHammerofMightMelee>(),
				ModContent.ItemType<Svantechnical>(),
				ModContent.ItemType<CrystylCrusher>(),
			}));
			npcLoot.Add(isExpert);
		}
		
		public override void OnKill()
		{
			if (lootTimer < 6000) //75 seconds for bullet hells + 25 seconds for normal phases
			{
				string key = "Go to hell.";
				Color messageColor = Color.Orange;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
				else if (Main.netMode == 2)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
				return;
			}
			if (Main.player[NPC.target].GetModPlayer<CalamityPlayerPreTrailer>().sCalKillCount == 0) //first time you kill her
			{
				if (Main.LocalPlayer.GetModPlayer<CalamityPlayerPreTrailer>().sCalDeathCount == 0)
				{
					string key = "You didn't die at all huh? Welp, you probably cheated. Do it again, for real this time...but here's your reward I guess."; //die no times
					Color messageColor = Color.Orange;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
				}
				else if (Main.LocalPlayer.GetModPlayer<CalamityPlayerPreTrailer>().sCalDeathCount == 1)
				{
					string key = "One death? That's it? ...I guess you earned this then."; //die one time
					Color messageColor = Color.Orange;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
				}
				else if (Main.LocalPlayer.GetModPlayer<CalamityPlayerPreTrailer>().sCalDeathCount == 2)
				{
					string key = "Two deaths, nice job. Here's your reward."; //die two times
					Color messageColor = Color.Orange;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
				}
				else if (Main.LocalPlayer.GetModPlayer<CalamityPlayerPreTrailer>().sCalDeathCount == 3)
				{
					string key = "Third time's the charm. Here's a special reward."; //die three times
					Color messageColor = Color.Orange;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
				}
				else //die however many times
				{
					string key = "At long last I am free...for a time. I'll keep coming back, just like you. Until we meet again, farewell.";
					Color messageColor = Color.Orange;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
				}
			}
			else //all times after first kill
			{
				string key = "At long last I am free...for a time. I'll keep coming back, just like you. Until we meet again, farewell.";
				Color messageColor = Color.Orange;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
				else if (Main.netMode == 2)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
			}
			if (Main.player[NPC.target].GetModPlayer<CalamityPlayerPreTrailer>().sCalKillCount < 5)
			{
				Main.player[NPC.target].GetModPlayer<CalamityPlayerPreTrailer>().sCalKillCount++;
			}
		}
		#endregion

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = Mod.Find<ModItem>("OmegaHealingPotion").Type;
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			if (projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type)
			{
				projectile.damage /= 2;
			}
			if (projectile.type == Mod.Find<ModProjectile>("ApothMark").Type || projectile.type == Mod.Find<ModProjectile>("ApothJaws").Type)
			{
				projectile.damage /= 3;
			}
		}

		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			if (modifiers.FinalDamage.Base > NPC.lifeMax / 10)
			{
				modifiers.SetMaxDamage(0);
			}
			double newDamage = (modifiers.FinalDamage.Base + (int)((double)NPC.defense * 0.25));
			float protection = (CalamityWorldPreTrailer.death ? 0.75f : 0.7f); //45%
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				protection = 0.6f;
			}
			if (newDamage < 1.0)
			{
				newDamage = 1.0;
			}
			if (NPC.ichor)
			{
				protection *= 0.9f; //41%
			}
			else if (NPC.onFire2)
			{
				protection *= 0.91f;
			}
			if (startFifthAttack)
			{
				protection *= 1.2f; //90% or 84%
			}
			if (protectionBoost)
			{
				protection = 0.99f; //99%
			}
			if (newDamage >= 1.0)
			{
				newDamage = (double)((int)((double)(1f - protection) * newDamage));
				if (newDamage < 1.0)
				{
					newDamage = 1.0;
				}
			}
			modifiers.FinalDamage.Base = (float)newDamage;
		}

		public override bool CheckActive()
		{
			return canDespawn;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.15f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (!NPC.active || NPC.IsABestiaryIconDummy)
			{
				return true;
			}
			Mod mod = ModLoader.GetMod("CalamityModClassicPreTrailer");
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;
			if (NPC.ai[0] > 1f)
			{
				texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/SupremeCalamitas/SupremeCalamitas2").Value;
			}
			else
			{
				texture = TextureAssets.Npc[NPC.type].Value;
			}
			Color newColor = (willCharge ? new Color(100, 0, 0, 0) : drawColor);
			SpriteEffects spriteEffects = SpriteEffects.None;
			Microsoft.Xna.Framework.Color color24 = NPC.GetAlpha(newColor);
			Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
			int num156 = TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type];
			int y3 = num156 * (int)NPC.frameCounter;
			Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(0, y3, texture.Width, num156);
			Vector2 origin2 = rectangle.Size() / 2f;
			int num157 = 8;
			int num158 = 2;
			int num159 = 1;
			float num160 = 0f;
			int num161 = num159;
			while (((num158 > 0 && num161 < num157) || (num158 < 0 && num161 > num157)) && Lighting.NotRetro)
			{
				Microsoft.Xna.Framework.Color color26 = NPC.GetAlpha(color25);
				{
					goto IL_6899;
				}
			IL_6881:
				num161 += num158;
				continue;
			IL_6899:
				float num164 = (float)(num157 - num161);
				if (num158 < 0)
				{
					num164 = (float)(num159 - num161);
				}
				color26 *= num164 / ((float)NPCID.Sets.TrailCacheLength[NPC.type] * 1.5f);
				Vector2 value4 = (NPC.oldPos[num161]);
				float num165 = NPC.rotation;
				Main.spriteBatch.Draw(texture, value4 + NPC.Size / 2f - Main.screenPosition + new Vector2(0, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26, num165 + NPC.rotation * num160 * (float)(num161 - 1) * -(float)spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin2, NPC.scale, spriteEffects, 0f);
				goto IL_6881;
			}
			var something = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(texture, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), NPC.frame, color24, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, something, 0);
			return false;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 100;
				NPC.height = 100;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("VulnerabilityHex").Type, 600, true);
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 600, true);
			}
		}
	}
}