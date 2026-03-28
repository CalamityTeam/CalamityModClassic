using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Events;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.GameContent.Generation;
using CalamityModClassicPreTrailer.Tiles;
using CalamityModClassicPreTrailer.Projectiles;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.Accessories.RareVariants;
using CalamityModClassicPreTrailer.Items.AquaticScourge;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.AstrumDeus;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage.RareVariants;
using CalamityModClassicPreTrailer.Items.DesertScourge;
using CalamityModClassicPreTrailer.Items.SlimeGod;
using CalamityModClassicPreTrailer.Items.TheDevourerofGods;
using CalamityModClassicPreTrailer.Items.Weapons;
using CalamityModClassicPreTrailer.Items.Weapons.RareVariants;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;
using CalamityModClassicPreTrailer.NPCs.Yharon;
using CalamityModClassicPreTrailer.World;
using Terraria.GameContent.ItemDropRules;
using Terraria.WorldBuilding;
using Conditions = Terraria.GameContent.ItemDropRules.Conditions;

namespace CalamityModClassicPreTrailer.NPCs
{
	public class CalamityGlobalNPCLoot : GlobalNPC
	{
		#region PreNPCLoot
		public override bool PreKill(NPC npc)
		{
			#region BossRush
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				if (npc.type == Mod.Find<ModNPC>("ProfanedGuardianBoss").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 7;
					DespawnProj();
				}
				else if (npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail)
				{
					if (npc.boss) 
					{
						CalamityWorldPreTrailer.bossRushStage = 8;
						DespawnProj();
					}
				}
				else if (npc.type == Mod.Find<ModNPC>("Astrageldon").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 9;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("Bumblefuck").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 12;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("HiveMindP2").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 14;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("StormWeaverHeadNaked").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 16;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("AquaticScourgeHead").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 17;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("DesertScourgeHead").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 18;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("CrabulonIdle").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 20;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("CeaselessVoid").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 22;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("PerforatorHive").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 23;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("Cryogen").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 24;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("BrimstoneElemental").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 25;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("CosmicWraith").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 26;
					DespawnProj();
					string key = "Hmm? Your perseverance is truly a trait to behold. You've come further than even the demigods in such a short time.";
					Color messageColor = Color.LightCoral;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
				}
				else if (npc.type == Mod.Find<ModNPC>("ScavengerBody").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 27;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("AstrumDeusHeadSpectral").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 30;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("Polterghast").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 31;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("PlaguebringerGoliath").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 32;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("CalamitasRun3").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 33;
					DespawnProj();
					string key = "Hmm? So you've made it to the final tier, a remarkable feat enviable by even the mightiest of the gods.";
					Color messageColor = Color.LightCoral;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
				}
				else if (npc.type == Mod.Find<ModNPC>("Siren").Type || npc.type == Mod.Find<ModNPC>("Leviathan").Type)
				{
					int bossType = (npc.type == Mod.Find<ModNPC>("Siren").Type) ? Mod.Find<ModNPC>("Leviathan").Type : Mod.Find<ModNPC>("Siren").Type;
					if (!NPC.AnyNPCs(bossType))
					{
						CalamityWorldPreTrailer.bossRushStage = 34;
						DespawnProj();
					}
				}
				else if (npc.type == Mod.Find<ModNPC>("SlimeGodCore").Type || npc.type == Mod.Find<ModNPC>("SlimeGodSplit").Type || npc.type == Mod.Find<ModNPC>("SlimeGodRunSplit").Type)
				{
					if (npc.type == Mod.Find<ModNPC>("SlimeGodCore").Type && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodSplit").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodRunSplit").Type) &&
						!NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGod").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodRun").Type))
					{
						CalamityWorldPreTrailer.bossRushStage = 35;
						DespawnProj();
					}
					else if (npc.type == Mod.Find<ModNPC>("SlimeGodSplit").Type && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodCore").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodRunSplit").Type) &&
						NPC.CountNPCS(Mod.Find<ModNPC>("SlimeGodSplit").Type) < 2 && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodRun").Type))
					{
						CalamityWorldPreTrailer.bossRushStage = 35;
						DespawnProj();
					}
					else if (npc.type == Mod.Find<ModNPC>("SlimeGodRunSplit").Type && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodCore").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodSplit").Type) &&
						NPC.CountNPCS(Mod.Find<ModNPC>("SlimeGodRunSplit").Type) < 2 && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGod").Type))
					{
						CalamityWorldPreTrailer.bossRushStage = 35;
						DespawnProj();
					}
				}
				else if (npc.type == Mod.Find<ModNPC>("Providence").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 36;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("SupremeCalamitas").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 37;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("Yharon").Type)
				{
					CalamityWorldPreTrailer.bossRushStage = 38;
					DespawnProj();
				}
				else if (npc.type == Mod.Find<ModNPC>("DevourerofGodsHeadS").Type)
				{
					npc.DropItemInstanced(npc.position, npc.Size, Mod.Find<ModItem>("Rock").Type, 1, true);
					CalamityWorldPreTrailer.bossRushStage = 0;
					DespawnProj();
					CalamityWorldPreTrailer.bossRushActive = false;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						var netMessage = Mod.GetPacket();
						netMessage.Write((byte)CalamityModClassicPreTrailerMessageType.BossRushStage);
						netMessage.Write(CalamityWorldPreTrailer.bossRushStage);
						netMessage.Send();
					}
					string key = "Hmm? You expected a reward beyond this mere pebble? Patience, the true reward will become apparent in time...";
					Color messageColor = Color.LightCoral;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
					return false;
				}
				switch (npc.type)
				{
					case NPCID.QueenBee:
						CalamityWorldPreTrailer.bossRushStage = 1;
						DespawnProj();
						break;
					case NPCID.BrainofCthulhu:
						CalamityWorldPreTrailer.bossRushStage = 2;
						DespawnProj();
						break;
					case NPCID.KingSlime:
						CalamityWorldPreTrailer.bossRushStage = 3;
						DespawnProj();
						break;
					case NPCID.EyeofCthulhu:
						CalamityWorldPreTrailer.bossRushStage = 4;
						DespawnProj();
						break;
					case NPCID.SkeletronPrime:
						CalamityWorldPreTrailer.bossRushStage = 5;
						DespawnProj();
						break;
					case NPCID.Golem:
						CalamityWorldPreTrailer.bossRushStage = 6;
						DespawnProj();
						break;
					case NPCID.TheDestroyer:
						CalamityWorldPreTrailer.bossRushStage = 10;
						DespawnProj();
						string key = "Hmm? Oh, you're still alive. Unexpected, but don't get complacent just yet.";
						Color messageColor = Color.LightCoral;
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
						break;
					case NPCID.Spazmatism:
						CalamityWorldPreTrailer.bossRushStage = 11;
						DespawnProj();
						break;
					case NPCID.Retinazer:
						CalamityWorldPreTrailer.bossRushStage = 11;
						DespawnProj();
						break;
					case NPCID.WallofFlesh:
						CalamityWorldPreTrailer.bossRushStage = 13;
						DespawnProj();
						break;
					case NPCID.SkeletronHead:
						CalamityWorldPreTrailer.bossRushStage = 15;
						DespawnProj();
						break;
					case NPCID.CultistBoss:
						CalamityWorldPreTrailer.bossRushStage = 19;
						DespawnProj();
						string key2 = "Hmm? Persistent aren't you? Perhaps you have some hope of prosperity, unlike past challengers.";
						Color messageColor2 = Color.LightCoral;
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key2), messageColor2);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key2), messageColor2);
						}
						break;
					case NPCID.Plantera:
						CalamityWorldPreTrailer.bossRushStage = 21;
						DespawnProj();
						break;
					case NPCID.DukeFishron:
						CalamityWorldPreTrailer.bossRushStage = 28;
						DespawnProj();
						break;
					case NPCID.MoonLordCore:
						CalamityWorldPreTrailer.bossRushStage = 29;
						DespawnProj();
						break;
					default:
						break;
				}
				if (Main.netMode == 2)
				{
					var netMessage = Mod.GetPacket();
					netMessage.Write((byte)CalamityModClassicPreTrailerMessageType.BossRushStage);
					netMessage.Write(CalamityWorldPreTrailer.bossRushStage);
					netMessage.Send();
				}
				return false;
			}
			#endregion
			#region AbyssDropCancel
			int x = Main.maxTilesX;
			int y = Main.maxTilesY;
			int genLimit = x / 2;
			int abyssChasmY = y - 250;
			int abyssChasmX = (CalamityWorldPreTrailer.abyssSide ? genLimit - (genLimit - 135) : genLimit + (genLimit - 135));
			bool abyssPosX = false;
			bool abyssPosY = ((double)(npc.position.Y / 16f) <= abyssChasmY);
			if (CalamityWorldPreTrailer.abyssSide)
			{
				if ((double)(npc.position.X / 16f) < abyssChasmX + 80)
				{
					abyssPosX = true;
				}
			}
			else
			{
				if ((double)(npc.position.X / 16f) > abyssChasmX - 80)
				{
					abyssPosX = true;
				}
			}
			bool hurtByAbyss = (npc.wet && npc.damage > 0 && !npc.boss && !npc.friendly && !npc.dontTakeDamage &&
				((((double)(npc.position.Y / 16f) > (Main.rockLayer - (double)Main.maxTilesY * 0.05)) &&
				abyssPosY && abyssPosX) || CalamityWorldPreTrailer.abyssTiles > 200) &&
				!npc.buffImmune[Mod.Find<ModBuff>("CrushDepth").Type]);
			if (hurtByAbyss)
			{
				return false;
			}
			#endregion
			if (CalamityWorldPreTrailer.revenge)
			{
				if (npc.type == NPCID.Probe)
					return false;
			}
			
			return true;
		}
		#endregion

		public override void OnKill(NPC npc)
		{
			bool revenge = CalamityWorldPreTrailer.revenge;
			if (CalamityGlobalNPC.DraedonMayhem)
			{
				if (!CalamityPlayerPreTrailer.areThereAnyDamnBosses)
				{
					CalamityGlobalNPC.DraedonMayhem = false;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}

			#region SpawnPolterghast

			if ((npc.type == Mod.Find<ModNPC>("PhantomSpirit").Type ||
			     npc.type == Mod.Find<ModNPC>("PhantomSpiritS").Type ||
			     npc.type == Mod.Find<ModNPC>("PhantomSpiritM").Type ||
			     npc.type == Mod.Find<ModNPC>("PhantomSpiritL").Type) &&
			    !NPC.AnyNPCs(Mod.Find<ModNPC>("Polterghast").Type) && !CalamityWorldPreTrailer.downedPolterghast)
			{
				CalamityModClassicPreTrailer.ghostKillCount++;
				if (CalamityModClassicPreTrailer.ghostKillCount >= 30 && Main.netMode != 1)
				{
					int lastPlayer = npc.lastInteraction;
					if (!Main.player[lastPlayer].active || Main.player[lastPlayer].dead)
					{
						lastPlayer = npc.FindClosestPlayer();
					}

					if (lastPlayer >= 0)
					{
						NPC.SpawnOnPlayer(lastPlayer, Mod.Find<ModNPC>("Polterghast").Type);
						CalamityModClassicPreTrailer.ghostKillCount = 0;
					}
				}
			}

			#endregion

			#region SpawnGSS

			if ((NPC.downedPlantBoss || CalamityWorldPreTrailer.downedCalamitas) && npc.type == NPCID.SandShark &&
			    !NPC.AnyNPCs(Mod.Find<ModNPC>("GreatSandShark").Type))
			{
				CalamityModClassicPreTrailer.sharkKillCount++;
				if (CalamityModClassicPreTrailer.sharkKillCount >= 10 && Main.netMode != 1)
				{
					if (!Main.player[Main.myPlayer].dead && Main.player[Main.myPlayer].active)
					{
						SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/MaulerRoar"),
							Main.player[Main.myPlayer].position);
					}

					int lastPlayer = npc.lastInteraction;
					if (!Main.player[lastPlayer].active || Main.player[lastPlayer].dead)
					{
						lastPlayer = npc.FindClosestPlayer();
					}

					if (lastPlayer >= 0)
					{
						NPC.SpawnOnPlayer(lastPlayer, Mod.Find<ModNPC>("GreatSandShark").Type);
						CalamityModClassicPreTrailer.sharkKillCount = -10;
					}
				}
			}

			#endregion
			if(npc.type == Mod.Find<ModNPC>("DesertScourgeHead").Type)
			{
				string key = "The depths of the underground desert are rumbling...";
				Color messageColor = Color.Aquamarine;
				if (!CalamityWorldPreTrailer.downedDesertScourge)
				{
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
				}

				CalamityWorldPreTrailer.downedDesertScourge = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}

			#region AdrenalineReset

			if (npc.boss && CalamityWorldPreTrailer.revenge)
			{
				if (npc.type != Mod.Find<ModNPC>("HiveMind").Type && npc.type != Mod.Find<ModNPC>("Leviathan").Type &&
				    npc.type != Mod.Find<ModNPC>("Siren").Type &&
				    npc.type != Mod.Find<ModNPC>("StormWeaverHead").Type &&
				    npc.type != Mod.Find<ModNPC>("StormWeaverBody").Type &&
				    npc.type != Mod.Find<ModNPC>("StormWeaverTail").Type &&
				    npc.type != Mod.Find<ModNPC>("DevourerofGodsHead").Type &&
				    npc.type != Mod.Find<ModNPC>("DevourerofGodsBody").Type &&
				    npc.type != Mod.Find<ModNPC>("DevourerofGodsTail").Type)
				{
					if (Main.netMode != 2)
					{
						if (!Main.player[Main.myPlayer].dead && Main.player[Main.myPlayer].active)
						{
							Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>().adrenaline = 0;
						}
					}
				}
			}

			#endregion

			if (npc.type == Mod.Find<ModNPC>("AquaticScourgeHead").Type)
			{
				CalamityWorldPreTrailer.downedAquaticScourge = true;
				if (Main.netMode == 2)
				{ 
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("AstrumDeusHeadSpectral").Type)
			{
				string key = "The seal of the stars has been broken!";
				Color messageColor = Color.Gold;
				if (Main.netMode == 0)
					Main.NewText(Language.GetTextValue(key), messageColor);
				else if (Main.netMode == 2)
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				CalamityWorldPreTrailer.downedStarGod = true;
				if (Main.netMode == 2)	
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}

			if (npc.type == Mod.Find<ModNPC>("DevourerofGodsHead").Type)
			{
				CalamityWorldPreTrailer.DoGSecondStageCountdown = 21600; //6 minutes
				if (Main.netMode == 2)
				{
					var netMessage = Mod.GetPacket();
					netMessage.Write((byte)CalamityModClassicPreTrailerMessageType.DoGCountdownSync);
					netMessage.Write(CalamityWorldPreTrailer.DoGSecondStageCountdown);
					netMessage.Send();
				}
			}

			if (npc.type == Mod.Find<ModNPC>("DevourerofGodsHeadS").Type)
			{
				string key = "The frigid moon shimmers brightly.";
				Color messageColor = Color.Cyan;
				string key2 = "The harvest moon glows eerily.";
				Color messageColor2 = Color.Orange;
				if (!CalamityWorldPreTrailer.downedDoG)
				{
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
						Main.NewText(Language.GetTextValue(key2), messageColor2);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key2), messageColor2);
					}
				}
			CalamityWorldPreTrailer.downedDoG = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}

			#region Boss Specials Messages
				if (npc.boss && !CalamityWorldPreTrailer.downedBossAny)
				{
					CalamityWorldPreTrailer.downedBossAny = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}

				if (npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody ||
				    npc.type == NPCID.EaterofWorldsTail || npc.type == NPCID.BrainofCthulhu)
				{
					if (npc.boss)
					{
						bool downedEvil = CalamityWorldPreTrailer.downedWhar;
						CalamityWorldPreTrailer.downedWhar = true;
						if (Main.netMode == 2)
						{
							NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
			if (npc.type == NPCID.SkeletronHead)
			{
				CalamityWorldPreTrailer.downedSkullHead = true;
				if (Main.netMode == 2)
				{ 
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			if (npc.type == NPCID.WallofFlesh)
			{
				if (WorldGenerationMethods.checkAstralMeteor())
				{
					if (!CalamityWorldPreTrailer.spawnAstralMeteor)
					{
						string key = "A star has fallen from the heavens!";
						Color messageColor = Color.Gold;
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
            
						CalamityWorldPreTrailer.spawnAstralMeteor = true;
						if (Main.netMode == 2)
						{
							NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						}
            
						WorldGenerationMethods.dropAstralMeteor();
					}
				}
				else if (Main.rand.Next(2) == 0 && !CalamityWorldPreTrailer.spawnAstralMeteor2)
				{
					string key = "A star has fallen from the heavens!";
					Color messageColor = Color.Gold;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
					CalamityWorldPreTrailer.spawnAstralMeteor2 = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					WorldGenerationMethods.dropAstralMeteor();
				}
				else if (Main.rand.Next(4) == 0 && !CalamityWorldPreTrailer.spawnAstralMeteor3)
				{
					string key = "A star has fallen from the heavens!";
					Color messageColor = Color.Gold;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
					CalamityWorldPreTrailer.spawnAstralMeteor3 = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					WorldGenerationMethods.dropAstralMeteor();
				}
				CalamityWorldPreTrailer.downedUgly = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				if (!Main.hardMode)
				{
					string key2 = "The Sunken Sea trembles...";
					Color messageColor2 = Color.Aquamarine;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key2), messageColor2);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key2), messageColor2);
					}
				}
			}
			if (npc.type == NPCID.SkeletronPrime || npc.type == Mod.Find<ModNPC>("BrimstoneElemental").Type)
			{
				bool downedPrime = CalamityWorldPreTrailer.downedSkeletor;
				if (npc.type == NPCID.SkeletronPrime)
				{
					CalamityWorldPreTrailer.downedSkeletor = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				string key = "A blood red inferno lingers in the night...";
				Color messageColor = Color.Crimson;
				if (!downedPrime && !CalamityWorldPreTrailer.downedBrimstoneElemental)
				{
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
				}
				if (npc.type == Mod.Find<ModNPC>("BrimstoneElemental").Type)
				{
					CalamityWorldPreTrailer.downedBrimstoneElemental = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
			
			if (npc.type == NPCID.Plantera || npc.type == Mod.Find<ModNPC>("CalamitasRun3").Type)
			{
				bool downedPlant = CalamityWorldPreTrailer.downedPlantThing;
				if (npc.type == NPCID.Plantera)
				{
					CalamityWorldPreTrailer.downedPlantThing = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				string key = "The ocean depths are trembling.";
				Color messageColor = Color.RoyalBlue;
				string key2 = "Energized plant matter has formed in the underground.";
				Color messageColor2 = Color.GreenYellow;
				if (npc.type == Mod.Find<ModNPC>("CalamitasRun3").Type)
				{
					if (!CalamityWorldPreTrailer.downedCalamitas && !downedPlant)
					{
						if (!Main.player[Main.myPlayer].dead && Main.player[Main.myPlayer].active)
						{
							SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/WyrmScream"), Main.player[Main.myPlayer].position);
						}
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
					}
					CalamityWorldPreTrailer.downedCalamitas = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (npc.type == NPCID.Plantera)
				{
					if (!downedPlant)
					{
						WorldGenerationMethods.spawnOre(Mod.Find<ModTile>("PerennialOre").Type, 12E-05, .5f, .7f);
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key2), messageColor2);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key2), messageColor2);
						}
					}
					if (!downedPlant && !CalamityWorldPreTrailer.downedCalamitas)
					{
						if (!Main.player[Main.myPlayer].dead && Main.player[Main.myPlayer].active)
						{
							SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/WyrmScream"), Main.player[Main.myPlayer].position);
						}
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
			}
			
			if (npc.type == NPCID.Golem)
			{
				bool downedIdiot = CalamityWorldPreTrailer.downedGolemBaby;
				CalamityWorldPreTrailer.downedGolemBaby = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				string key = "A plague has befallen the Jungle.";
				Color messageColor = Color.Lime;
				string key2 = "An ancient automaton roams the land.";
				Color messageColor2 = Color.Yellow;
				if (!downedIdiot)
				{
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
						Main.NewText(Language.GetTextValue(key2), messageColor2);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key2), messageColor2);
					}
				}
			}
			
			if (npc.type == NPCID.MoonLordCore)
			{
				bool downedMoonDude = CalamityWorldPreTrailer.downedMoonDude;
				CalamityWorldPreTrailer.downedMoonDude = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				string key = "The profaned flame blazes fiercely!";
				Color messageColor = Color.Orange;
				string key2 = "Cosmic terrors are watching...";
				Color messageColor2 = Color.Violet;
				string key3 = "The bloody moon beckons...";
				Color messageColor3 = Color.Crimson;
				string key4 = "Shrieks are echoing from the dungeon.";
				Color messageColor4 = Color.Cyan;
				string key5 = "A cold and dark energy has materialized in space.";
				Color messageColor5 = Color.LightGray;
				if (!downedMoonDude)
				{
					WorldGenerationMethods.spawnOre(Mod.Find<ModTile>("ExodiumOre").Type, 12E-05, .01f, .07f);
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
						Main.NewText(Language.GetTextValue(key2), messageColor2);
						Main.NewText(Language.GetTextValue(key3), messageColor3);
						Main.NewText(Language.GetTextValue(key4), messageColor4);
						Main.NewText(Language.GetTextValue(key5), messageColor5);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key2), messageColor2);
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key3), messageColor3);
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key4), messageColor4);
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key5), messageColor5);
					}
				}
			}
			
			if (npc.type == NPCID.DD2Betsy)
			{
				CalamityWorldPreTrailer.downedBetsy = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == NPCID.Mothron && CalamityWorldPreTrailer.buffedEclipse)
			{
				CalamityWorldPreTrailer.downedBuffedMothron = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
				
			if (npc.type == Mod.Find<ModNPC>("Astrageldon").Type)
			{
				if (WorldGenerationMethods.checkAstralMeteor())
				{
					string key = "A star has fallen from the heavens!";
					Color messageColor = Color.Gold;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
					WorldGenerationMethods.dropAstralMeteor();
				}
				CalamityWorldPreTrailer.downedAstrageldon = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("HiveMindP2").Type) //boss 2
			{
				if (!CalamityWorldPreTrailer.downedHiveMind)
				{
					if (!CalamityWorldPreTrailer.downedPerforator)
					{
						string key = "The ground is glittering with cyan light.";
						Color messageColor = Color.Cyan;
						WorldGenerationMethods.spawnOre(Mod.Find<ModTile>("AerialiteOre").Type, 12E-05, .4f, .6f);
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
				CalamityWorldPreTrailer.downedHiveMind = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("PerforatorHive").Type) //boss 3
			{
				if (!CalamityWorldPreTrailer.downedPerforator)
				{
					if (!CalamityWorldPreTrailer.downedHiveMind)
					{
						string key = "The ground is glittering with cyan light.";
						Color messageColor = Color.Cyan;
						WorldGenerationMethods.spawnOre(Mod.Find<ModTile>("AerialiteOre").Type, 12E-05, .4f, .6f);
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
				CalamityWorldPreTrailer.downedPerforator = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("SlimeGodCore").Type || npc.type == Mod.Find<ModNPC>("SlimeGodSplit").Type || npc.type == Mod.Find<ModNPC>("SlimeGodRunSplit").Type) //boss 4
			{
				if (npc.type == Mod.Find<ModNPC>("SlimeGodCore").Type && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodSplit").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodRunSplit").Type)
				    && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGod").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodRun").Type))
				{
					Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayerPreTrailer>().revJamDrop = true;
					CalamityWorldPreTrailer.downedSlimeGod = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				else if (npc.type == Mod.Find<ModNPC>("SlimeGodSplit").Type && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodCore").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodRunSplit").Type) &&
				         NPC.CountNPCS(Mod.Find<ModNPC>("SlimeGodSplit").Type) < 2 && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodRun").Type))
				{
					Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayerPreTrailer>().revJamDrop = true;
					CalamityWorldPreTrailer.downedSlimeGod = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				else if (npc.type == Mod.Find<ModNPC>("SlimeGodRunSplit").Type && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodCore").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodSplit").Type) &&
				         NPC.CountNPCS(Mod.Find<ModNPC>("SlimeGodRunSplit").Type) < 2 && !NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGod").Type))
				{
					Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayerPreTrailer>().revJamDrop = true;
					CalamityWorldPreTrailer.downedSlimeGod = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("Cryogen").Type) //boss 5
			{
				if (!CalamityWorldPreTrailer.downedCryogen)
				{
					string key = "The ice caves are crackling with frigid energy.";
					Color messageColor = Color.LightSkyBlue;
					WorldGenerationMethods.spawnOre(Mod.Find<ModTile>("CryonicOre").Type, 15E-05, .45f, .65f);
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
				}
				CalamityWorldPreTrailer.downedCryogen = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("Siren").Type || npc.type == Mod.Find<ModNPC>("Leviathan").Type) //boss 8
			{
				int bossType = (npc.type == Mod.Find<ModNPC>("Siren").Type) ? Mod.Find<ModNPC>("Leviathan").Type : Mod.Find<ModNPC>("Siren").Type;
				if (!NPC.AnyNPCs(bossType))
				{
					CalamityWorldPreTrailer.downedLeviathan = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("PlaguebringerGoliath").Type) //boss 9
			{
				CalamityWorldPreTrailer.downedPlaguebringer = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("ProfanedGuardianBoss").Type) //boss 10
			{
			
				CalamityWorldPreTrailer.downedGuardians = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("Providence").Type) //boss 11
			{
				string key2 = "The calamitous beings have been inundated with bloodstone.";
				Color messageColor2 = Color.Orange;
				string key3 = "Fossilized tree bark is bursting through the jungle's mud.";
				Color messageColor3 = Color.LightGreen;
				if (!CalamityWorldPreTrailer.downedProvidence)
				{
					WorldGenerationMethods.spawnOre(Mod.Find<ModTile>("UelibloomOre").Type, 15E-05, .4f, .8f);
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key2), messageColor2);
						Main.NewText(Language.GetTextValue(key3), messageColor3);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key2), messageColor2);
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key3), messageColor3);
					}
				}
				CalamityWorldPreTrailer.downedProvidence = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("CeaselessVoid").Type) //boss 12
			{
				CalamityWorldPreTrailer.downedSentinel1 = true; //21600
				if (CalamityWorldPreTrailer.DoGSecondStageCountdown > 14460)
				{
					CalamityWorldPreTrailer.DoGSecondStageCountdown = 14460;
					if (Main.netMode == 2)
					{
						var netMessage = Mod.GetPacket();
						netMessage.Write((byte)CalamityModClassicPreTrailerMessageType.DoGCountdownSync);
						netMessage.Write(CalamityWorldPreTrailer.DoGSecondStageCountdown);
						netMessage.Send();
					}
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("StormWeaverHeadNaked").Type) //boss 13
			{
				CalamityWorldPreTrailer.downedSentinel2 = true; //21600
				if (CalamityWorldPreTrailer.DoGSecondStageCountdown > 7260)
				{
					CalamityWorldPreTrailer.DoGSecondStageCountdown = 7260;
					if (Main.netMode == 2)
					{
						var netMessage = Mod.GetPacket();
						netMessage.Write((byte)CalamityModClassicPreTrailerMessageType.DoGCountdownSync);
						netMessage.Write(CalamityWorldPreTrailer.DoGSecondStageCountdown);
						netMessage.Send();
					}
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("CosmicWraith").Type) //boss 14
			{
				CalamityWorldPreTrailer.downedSentinel3 = true; //21600
				if (CalamityWorldPreTrailer.DoGSecondStageCountdown > 600)
				{
					CalamityWorldPreTrailer.DoGSecondStageCountdown = 600;
					if (Main.netMode == 2)
					{
						var netMessage = Mod.GetPacket();
						netMessage.Write((byte)CalamityModClassicPreTrailerMessageType.DoGCountdownSync);
						netMessage.Write(CalamityWorldPreTrailer.DoGSecondStageCountdown);
						netMessage.Send();
					}
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("Bumblefuck").Type) //boss 16
			{
				CalamityWorldPreTrailer.downedBumble = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("Yharon").Type) //boss 17
			{
				string key = "The dark sun awaits.";
				Color messageColor = Color.Orange;
				string key2 = "A godly aura has blessed the world's caverns.";
				Color messageColor2 = Color.Gold;
				if (!CalamityWorldPreTrailer.downedYharon && npc.localAI[2] == 1f)
				{
					WorldGenerationMethods.spawnOre(Mod.Find<ModTile>("AuricOre").Type, 2E-05, .6f, .8f);
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key2), messageColor2);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key2), messageColor2);
					}
				}
				if (npc.localAI[2] == 1f)
				{
					CalamityWorldPreTrailer.downedYharon = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (!CalamityWorldPreTrailer.buffedEclipse && npc.localAI[2] != 2f)
				{
					CalamityWorldPreTrailer.buffedEclipse = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
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
			
			if (npc.type == Mod.Find<ModNPC>("SupremeCalamitas").Type) //boss 18
			{
				CalamityWorldPreTrailer.downedSCal = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("CrabulonIdle").Type) //boss 19
			{
			
				CalamityWorldPreTrailer.downedCrabulon = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("ScavengerBody").Type) //boss 20
			{
			
				CalamityWorldPreTrailer.downedScavenger = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (npc.type == Mod.Find<ModNPC>("Polterghast").Type) //boss 21
			{
				string key = "The abyssal spirits have been disturbed.";
				Color messageColor = Color.RoyalBlue;
				if (!CalamityWorldPreTrailer.downedPolterghast)
				{
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
				}
				CalamityWorldPreTrailer.downedPolterghast = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			// thanks lorde very cool
			/*
			if (npc.type == Mod.Find<ModNPC>("THELORDE").Type) //boss 22
			{
				CalamityWorldPreTrailer.downedLORDE = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			*/
			if (npc.type == Mod.Find<ModNPC>("GiantClam").Type) //boss 23
			{
				CalamityWorldPreTrailer.downedCLAM = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			
			#endregion
			// thanks doggo very cool :thumbsup: - siberian
			if (npc.type == Mod.Find<ModNPC>("DevourerofGodsHead").Type)
			{
				for (int playerIndex = 0; playerIndex < 255; playerIndex++)
				{
					if (Main.player[playerIndex].active)
					{
						Player player = Main.player[playerIndex];
						for (int l = 0; l < 22; l++)
						{
							int hasBuff = player.buffType[l];
							if (hasBuff == Mod.Find<ModBuff>("AdrenalineMode").Type)
							{
								player.DelBuff(l);
								l = -1;
							}
							if (hasBuff == Mod.Find<ModBuff>("RageMode").Type)
							{
								player.DelBuff(l);
								l = -1;
							}
						}
					}
				}
			}
			#region WormLootSpawns
			// curse you modifynpcloot!!! - siberian
			if (npc.type == Mod.Find<ModNPC>("DesertScourgeHead").Type)
			{
				Vector2 center = Main.player[npc.target].Center;
				float num2 = 1E+08f;
				Vector2 position2 = npc.position;
				for (int k = 0; k < 200; k++)
				{
					if (Main.npc[k].active && (Main.npc[k].type == Mod.Find<ModNPC>("DesertScourgeHead").Type ||
					                           Main.npc[k].type == Mod.Find<ModNPC>("DesertScourgeBody").Type ||
					                           Main.npc[k].type == Mod.Find<ModNPC>("DesertScourgeTail").Type))
					{
						float num3 = Math.Abs(Main.npc[k].Center.X - center.X) +
						             Math.Abs(Main.npc[k].Center.Y - center.Y);
						if (num3 < num2)
						{
							num2 = num3;
							position2 = Main.npc[k].position;
						}
					}
				}
				npc.position = position2;
			}

			if (npc.type == Mod.Find<ModNPC>("AquaticScourgeHead").Type)
			{
				Vector2 center = Main.player[npc.target].Center;
				float num2 = 1E+08f;
				Vector2 position2 = npc.position;
				for (int k = 0; k < 200; k++)
				{
					if (Main.npc[k].active &&
					    (Main.npc[k].type == Mod.Find<ModNPC>("AquaticScourgeHead").Type ||
					     Main.npc[k].type == Mod.Find<ModNPC>("AquaticScourgeBody").Type ||
					     Main.npc[k].type == Mod.Find<ModNPC>("AquaticScourgeBodyAlt").Type ||
					     Main.npc[k].type == Mod.Find<ModNPC>("AquaticScourgeTail").Type))
					{
						float num3 = Math.Abs(Main.npc[k].Center.X - center.X) +
						             Math.Abs(Main.npc[k].Center.Y - center.Y);
						if (num3 < num2)
						{
							num2 = num3;
							position2 = Main.npc[k].position;
						}
					}
				}
				npc.position = position2;
			}

			if (npc.type == Mod.Find<ModNPC>("AstrumDeusHeadSpectral").Type)
			{
				Vector2 center = Main.player[npc.target].Center;
				float num2 = 1E+08f;
				Vector2 position2 = npc.position;
				for (int k = 0; k < 200; k++)
				{
					if (Main.npc[k].active && (Main.npc[k].type == Mod.Find<ModNPC>("AstrumDeusHeadSpectral").Type ||
					                           Main.npc[k].type == Mod.Find<ModNPC>("AstrumDeusBodySpectral").Type ||
					                           Main.npc[k].type == Mod.Find<ModNPC>("AstrumDeusTailSpectral").Type))
					{
						float num3 = Math.Abs(Main.npc[k].Center.X - center.X) +
						             Math.Abs(Main.npc[k].Center.Y - center.Y);
						if (num3 < num2)
						{
							num2 = num3;
							position2 = Main.npc[k].position;
						}
					}
				}
				npc.position = position2;
			}

			if (npc.type == Mod.Find<ModNPC>("DevourerofGodsHeadS").Type)
			{
				Vector2 center = Main.player[npc.target].Center;
				float num2 = 1E+08f;
				Vector2 position2 = npc.position;
				for (int k = 0; k < 200; k++)
				{
					if (Main.npc[k].active && (Main.npc[k].type == Mod.Find<ModNPC>("DevourerofGodsHeadS").Type ||
					                           Main.npc[k].type == Mod.Find<ModNPC>("DevourerofGodsBodyS").Type ||
					                           Main.npc[k].type == Mod.Find<ModNPC>("DevourerofGodsTailS").Type))
					{
						float num3 = Math.Abs(Main.npc[k].Center.X - center.X) +
						             Math.Abs(Main.npc[k].Center.Y - center.Y);
						if (num3 < num2)
						{
							num2 = num3;
							position2 = Main.npc[k].position;
						}
					}
				}
				npc.position = position2;
			}
			#region ArmorSetLoot
			if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayerPreTrailer>().tarraSet)
			{
				if (!npc.SpawnedFromStatue && (npc.damage > 5 || npc.boss) && npc.lifeMax > 100 && Main.rand.Next(5) == 0)
				{
					Item.NewItem(npc.GetSource_FromThis(null), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 58, 1, false, 0, false, false);
				}
			}
			if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayerPreTrailer>().bloodflareSet)
			{
				if (!npc.SpawnedFromStatue && (npc.damage > 5 || npc.boss) && Main.rand.Next(2) == 0 && Main.bloodMoon && npc.HasPlayerTarget && (double)(npc.position.Y / 16f) < Main.worldSurface)
				{
					Item.NewItem(npc.GetSource_FromThis(null), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.Mod.Find<ModItem>("BloodOrb").Type, 1, false, 0, false, false);
				}
			}
			if (!npc.SpawnedFromStatue && (npc.damage > 5 || npc.boss) && Main.rand.Next(12) == 0 && Main.bloodMoon && npc.HasPlayerTarget && (double)(npc.position.Y / 16f) < Main.worldSurface)
			{
				Item.NewItem(npc.GetSource_FromThis(null), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, base.Mod.Find<ModItem>("BloodOrb").Type, 1, false, 0, false, false);
			}
			#endregion
			
			#endregion 
		}
		#region NPCLoot
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{	
			LeadingConditionRule isExpert = new LeadingConditionRule(new Conditions.IsExpert());
			LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
			if ((npc.type == NPCID.Spazmatism || npc.type == NPCID.TheDestroyer || npc.type == NPCID.SkeletronPrime))
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NoDownedMechBosses(), Mod.Find<ModItem>("Knowledge20").Type, 1));
			}
			if (npc.type == NPCID.KingSlime)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedKS(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 2, 2));
			}
			else if (!NPC.downedBoss1 && npc.type == NPCID.EyeofCthulhu)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedEye(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 2, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedEye(), Mod.Find<ModItem>("Knowledge3").Type, 1));
			}
			else if (npc.type == NPCID.QueenBee)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedQB(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedQB(), Mod.Find<ModItem>("Knowledge16").Type, 1));
			}
			else if (npc.type == NPCID.TheDestroyer)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedDestroyer(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedDestroyer(), Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedDestroyer(), Mod.Find<ModItem>("ExplosiveShells").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedDestroyer(), Mod.Find<ModItem>("Knowledge21").Type, 1));
			}
			else if (npc.type == NPCID.Spazmatism)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedTwins(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedTwins(), Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedTwins(), Mod.Find<ModItem>("ExplosiveShells").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedTwins(), Mod.Find<ModItem>("Knowledge22").Type, 1));
			}
			else if (npc.type == NPCID.SkeletronPrime)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPrime(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPrime(), Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPrime(), Mod.Find<ModItem>("ExplosiveShells").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPrime(), Mod.Find<ModItem>("Knowledge23").Type, 1));
			}
			else if (npc.type == NPCID.Plantera)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPlantera(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPlantera(), Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPlantera(), Mod.Find<ModItem>("ExplosiveShells").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPlantera(), Mod.Find<ModItem>("Knowledge25").Type, 1));
			}
			else if (!NPC.downedFishron && npc.type == NPCID.DukeFishron)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedDuke(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedDuke(), Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedDuke(), Mod.Find<ModItem>("ExplosiveShells").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedDuke(), Mod.Find<ModItem>("Knowledge2").Type, 1));
			}
			else if (npc.type == NPCID.CultistBoss)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedCultist(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedCultist(), Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedCultist(), Mod.Find<ModItem>("ExplosiveShells").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedCultist(), Mod.Find<ModItem>("Knowledge4").Type, 1)); 
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(), Mod.Find<ModItem>("Knowledge34").Type, 1));
			}
			#region DefiledLoot
				if (npc.type == NPCID.AnglerFish || npc.type == NPCID.Werewolf)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.AdhesiveBandage, 20));
					if (npc.type == NPCID.Werewolf)
					{
						npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.MoonCharm, 20));
					}
				}
				else if (npc.type == NPCID.DesertBeast)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.AncientHorn, 20));
				}
				else if (npc.type == NPCID.ArmoredSkeleton)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.BeamSword, 20));
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.ArmorPolish, 20));
				}
				else if (npc.type == NPCID.Clown)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Bananarang, 20));
				}
				else if (npc.type == NPCID.Hornet || npc.type == NPCID.MossHornet || npc.type == NPCID.ToxicSludge)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Bezoar, 20));
					if (npc.type == NPCID.MossHornet)
					{
						npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.TatteredBeeWing, 20));
					}
				}
				else if (npc.type == NPCID.EyeofCthulhu)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Binoculars, 20));
				}
				else if (npc.type == NPCID.DemonEye || npc.type == NPCID.WanderingEye)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.BlackLens, 20));
				}
				else if (npc.type == NPCID.CorruptSlime || npc.type == NPCID.DarkMummy)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Blindfold, 20));
				}
				else if (npc.type >= 269 && npc.type <= 280)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Keybrand, 20));
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.BoneFeather, 20));
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.MagnetSphere, 20));
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.WispinaBottle, 20));
				}
				else if (npc.type == NPCID.UndeadMiner)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.BonePickaxe, 20));
				}
				else if (npc.type == NPCID.Skeleton)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.BoneSword, 20));
				}
				else if (npc.type == NPCID.ScutlixRider)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.BrainScrambler, 20));
				}
				else if (npc.type == NPCID.Vampire)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.BrokenBatWing, 20));
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.MoonStone, 20));
				}
				else if (npc.type == NPCID.CaveBat)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.ChainKnife, 20));
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.DepthMeter, 20));
				}
				else if (npc.type == NPCID.DarkCaster || npc.type == NPCID.AngryBones)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.ClothierVoodooDoll, 20));
				}
				else if (npc.type == NPCID.PirateCaptain)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.CoinGun, 20));
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.DiscountCard, 20));
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Cutlass, 20));
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.LuckyCoin, 20));
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.PirateStaff, 20));
				}
				else if (npc.type == NPCID.Reaper)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.DeathSickle, 20));
				}
				else if (npc.type == NPCID.Demon)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.DemonScythe, 20));
				}
				else if (npc.type == NPCID.DesertDjinn)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.DjinnLamp, 20));
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.DjinnsCurse, 20));
				}
				else if (npc.type == NPCID.Shark)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.DivingHelmet, 20));
				}
				else if (npc.type == NPCID.Pixie || npc.type == NPCID.Wraith || npc.type == NPCID.Mummy)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.FastClock, 20));
				}
				else if (npc.type == NPCID.RedDevil)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.FireFeather, 20));
				}
				else if (npc.type == NPCID.IceElemental || npc.type == NPCID.IcyMerman ||
				         npc.type == NPCID.ArmoredViking || npc.type == NPCID.IceTortoise)
				{
					if (npc.type == NPCID.IceElemental || npc.type == NPCID.IcyMerman)
					{
						npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.FrostStaff, 20));
					}
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.IceSickle, 20));

					if (npc.type == NPCID.IceTortoise)
					{
						npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.FrozenTurtleShell, 20));
					}
				}
				else if (npc.type == NPCID.Harpy && Main.hardMode)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.GiantHarpyFeather, 20));
				}
				else if (npc.type == Mod.Find<ModNPC>("SunBat").Type)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.HelFire, 20));
				}
				else if (npc.type == Mod.Find<ModNPC>("Cryon").Type)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Amarok, 20));
				}
				else if (npc.type == NPCID.QueenBee)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.HoneyedGoggles, 20));
				}
				else if (npc.type == NPCID.Piranha)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Hook, 20));
				}
				else if (npc.type == NPCID.DiabolistRed || npc.type == NPCID.DiabolistWhite)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.InfernoFork, 20));
				}
				else if (npc.type == NPCID.PinkJellyfish)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.JellyfishNecklace, 20));
				}
				else if (npc.type == NPCID.Paladin)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Kraken, 20));
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.PaladinsHammer, 20));
				}
				else if (npc.type == NPCID.SkeletonArcher)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.MagicQuiver, 20));
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Marrow, 20));
				}
				else if (npc.type == NPCID.Lavabat)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.MagmaStone, 20));
				}
				else if (npc.type == NPCID.WalkingAntlion)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.AntlionClaw, 20));
				}
				else if (npc.type == NPCID.DarkMummy || npc.type == NPCID.GreenJellyfish)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Megaphone, 20));
				}
				else if (npc.type == NPCID.CursedSkull)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Nazar, 20));
				}
				else if (npc.type == NPCID.FireImp)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.ObsidianRose, 20));
				}
				else if (npc.type == NPCID.BlackRecluse || npc.type == NPCID.BlackRecluseWall)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.PoisonStaff, 20));
				}
				else if (npc.type == NPCID.SkeletonSniper)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.RifleScope, 20));
				}
				else if (npc.type == NPCID.ChaosElemental)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.RodofDiscord, 20));
				}
				else if (npc.type == NPCID.Necromancer || npc.type == NPCID.NecromancerArmored)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.ShadowbeamStaff, 20));
				}
				else if (npc.type == NPCID.SnowFlinx)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.SnowballLauncher, 20));
				}
				else if (npc.type == NPCID.RaggedCaster)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.SpectreStaff, 20));
				}
				else if (npc.type == NPCID.Plantera)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.TheAxe, 20));
				}
				else if (npc.type == NPCID.GiantBat)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.TrifoldMap, 20));
				}
				else if (npc.type == NPCID.AngryTrapper)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Uzi, 20));
				}
				else if (npc.type == NPCID.FloatyGross || npc.type == NPCID.Corruptor)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Vitamins, 20));
				}
				else if (NPC.downedMechBossAny && npc.type == NPCID.GiantTortoise)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.Yelets, 20));
				}
			
			#endregion
			#region RareVariants

			if (npc.type == NPCID.BloodZombie)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new SkeletronCondition(), Mod.Find<ModItem>("Carnage").Type, 200));
			}
			else if (npc.type == NPCID.TacticalSkeleton)
			{
				npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("TrueConferenceCall").Type, 200));
			}
			else if (npc.type == NPCID.DesertBeast)
			{
				npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("EvilSmasher").Type, 200));
			}
			else if (npc.type == NPCID.DungeonSpirit)
			{
				npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("PearlGod").Type, 200));
			}
			else if (npc.type == NPCID.RuneWizard)
			{
				npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("EyeofMagnus").Type, 10));
			}
			else if (npc.type == NPCID.Mimic)
			{
				npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("TheBee").Type, 100));
			}
			#endregion
			#region ArmageddonLoot
			if (CalamityWorldPreTrailer.armageddon)
			{
				int dropAmt = 5;
				if (npc.type == NPCID.Golem)
				{
					npcLoot.Add(new CommonDrop(ItemID.GolemBossBag, 1, dropAmt));
				}
				else if (npc.type == NPCID.DukeFishron)
				{
					npcLoot.Add(new CommonDrop(ItemID.FishronBossBag, 1, dropAmt));
				}
				else if (npc.type == NPCID.DD2Betsy)
				{
					npcLoot.Add(new CommonDrop(ItemID.BossBagBetsy, 1, dropAmt));
				}
				else if (npc.type == NPCID.EyeofCthulhu)
				{
					npcLoot.Add(new CommonDrop(ItemID.EyeOfCthulhuBossBag, 1, dropAmt));
				}
				else if (npc.type == NPCID.BrainofCthulhu)
				{
					npcLoot.Add(new CommonDrop(ItemID.BrainOfCthulhuBossBag, 1, dropAmt));
				}
				else if (npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail)
				{
					if (npc.boss)
					{
						npcLoot.Add(new CommonDrop(ItemID.EaterOfWorldsBossBag, 1, dropAmt));
					}
				}
				else if (npc.type == NPCID.QueenBee)
				{
					npcLoot.Add(new CommonDrop(ItemID.QueenBeeBossBag, 1, dropAmt));
				}
				else if (npc.type == NPCID.SkeletronHead)
				{
					npcLoot.Add(new CommonDrop(ItemID.SkeletronBossBag, 1, dropAmt));
				}
				else if (npc.type == NPCID.WallofFlesh)
				{
					npcLoot.Add(new CommonDrop(ItemID.WallOfFleshBossBag, 1, dropAmt));
				}
				else if (npc.type == NPCID.MoonLordCore)
				{
					npcLoot.Add(new CommonDrop(ItemID.MoonLordBossBag, 1, dropAmt));
				}
				else if (npc.type == NPCID.KingSlime)
				{
					npcLoot.Add(new CommonDrop(ItemID.KingSlimeBossBag, 1, dropAmt));
				}
				else if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
				{
					int num64 = NPCID.Retinazer;
					if (npc.type == NPCID.Retinazer)
					{
						num64 = NPCID.Spazmatism;
					}
					if (!NPC.AnyNPCs(num64))
					{
						npcLoot.Add(new CommonDrop(ItemID.TwinsBossBag, 1, dropAmt));
					}
				}
				else if (npc.type == NPCID.SkeletronPrime)
				{
					npcLoot.Add(new CommonDrop(ItemID.SkeletronPrimeBossBag, 1, dropAmt));
				}
				else if (npc.type == NPCID.TheDestroyer)
				{
					npcLoot.Add(new CommonDrop(ItemID.DestroyerBossBag, 1, dropAmt));
				}
				else if (npc.type == NPCID.Plantera)
				{
					npcLoot.Add(new CommonDrop(ItemID.PlanteraBossBag, 1, dropAmt));
				}
			}
			#endregion
			#region WormLootFromNearestSegment
			if (npc.type == Mod.Find<ModNPC>("DesertScourgeHead").Type)
			{
				LeadingConditionRule SkeletronDead = new LeadingConditionRule(new SkeletronCondition());
				
				npcLoot.Add(new CommonDrop(ItemID.LesserHealingPotion, 1, 8, 15));
				npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("DesertScourgeTrophy").Type, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("VictoryShard").Type, 1, 7, 15));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.Coral, 1, 5, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.Seashell, 1, 5, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.Starfish, 1, 5, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("SeaboundStaff").Type, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("Barinade").Type, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("StormSpray").Type, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("AquaticDischarge").Type, 4));
				notExpert.OnSuccess(new OneFromOptionsDropRule(40, 2, new int[]
				{
					ModContent.ItemType<DuneHopper>(),
					ModContent.ItemType<ScourgeoftheDesert>(),
				}));
				npcLoot.Add(notExpert);
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("DeepDiver").Type, 40));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("DesertScourgeMask").Type, 7));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.HighTestFishingLine, 15));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.AnglerTackleBag, 15));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.TackleBox, 15));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.AnglerEarring, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.FishermansGuide, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.WeatherRadio, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.Sextant, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.AnglerHat, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.AnglerVest, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.AnglerPants, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.CratePotion, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.FishingPotion, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.SonarPotion, 5, 2, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("AeroStone").Type, 10));
				npcLoot.Add(SkeletronDead.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.GoldenBugNet, 20)));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedDS(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedDS(),Mod.Find<ModItem>("Knowledge").Type, 1));
			}
			else if (npc.type == Mod.Find<ModNPC>("AquaticScourgeHead").Type)
			{
				npcLoot.Add(new CommonDrop(ItemID.GreaterHealingPotion, 1, 8, 15));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("VictoryShard").Type, 1, 11, 21));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.Coral, 1, 5, 10));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.Seashell, 1, 5, 10));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.Starfish, 1, 5, 10));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.SoulofSight, 1, 20, 41));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("DeepseaStaff").Type, 4));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("Barinautical").Type, 4));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("Downpour").Type, 4)); 
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("SubmarineShocker").Type, 4));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.HighTestFishingLine, 8));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.AnglerTackleBag, 8));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.TackleBox, 8));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.AnglerEarring, 5));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.FishermansGuide, 5));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.WeatherRadio, 5));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.Sextant, 5));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.AnglerHat, 3));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.AnglerVest, 3));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.AnglerPants, 3));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.CratePotion, 3, 2, 4));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.FishingPotion, 3, 2, 4));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.SonarPotion, 3, 2, 4));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("AeroStone").Type, 5));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.GoldenBugNet, 10));
				npcLoot.Add(notExpert);
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), Mod.Find<ModItem>("VictoryShard").Type, 1, 11, 21));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.Coral, 1, 5, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.Seashell, 1, 5, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.Starfish, 1, 5, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.HighTestFishingLine, 15));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.AnglerTackleBag, 15));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.TackleBox, 15));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.AnglerEarring, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.FishermansGuide, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.WeatherRadio, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.Sextant, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.AnglerHat, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.AnglerVest, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.AnglerPants, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.CratePotion, 5, 2, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.FishingPotion, 5, 2, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.SonarPotion, 5, 2, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), Mod.Find<ModItem>("AeroStone").Type, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.GoldenBugNet, 20));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedAquaticScourge(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedAquaticScourge(),Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedAquaticScourge(),Mod.Find<ModItem>("ExplosiveShells").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedAquaticScourge(),Mod.Find<ModItem>("Knowledge27").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedAquaticScourge(),Mod.Find<ModItem>("Knowledge35").Type, 1));
			}
			else if (npc.type == Mod.Find<ModNPC>("AstrumDeusHeadSpectral").Type)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedAstrumDeus(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedAstrumDeus(),Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedAstrumDeus(),Mod.Find<ModItem>("ExplosiveShells").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedAstrumDeus(),Mod.Find<ModItem>("Knowledge29").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedAstrumDeus(),Mod.Find<ModItem>("Knowledge36").Type, 1));
				npcLoot.Add(new CommonDrop(ItemID.GreaterHealingPotion, 1, 8, 15));
				npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("AstrumDeusTrophy").Type, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("Stardust").Type, 1, 5, 50, 81));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("HideofAstrumDeus").Type, 40));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("Quasar").Type, 40));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.HallowedKey, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("Starfall").Type, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("Nebulash").Type, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("AstrumDeusMask").Type, 7));
			}
			else if (npc.type == Mod.Find<ModNPC>("DevourerofGodsHeadS").Type)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedDoG(), Mod.Find<ModItem>("Knowledge42").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedDoG(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 6, 6));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedDoG(), Mod.Find<ModItem>("GrenadeRounds").Type, 1, 3, 3));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedDoG(), Mod.Find<ModItem>("ExplosiveShells").Type, 1, 2, 2));
				npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("SupremeHealingPotion").Type, 1, 8, 15));
				npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("DevourerofGodsTrophy").Type, 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("DevourerofGodsMask").Type, 7));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("Norfleet").Type, 40));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("Skullmasher").Type, 40));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("DeathhailStaff").Type, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("Excelsus").Type, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("TheObliterator").Type, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("Eradicator").Type, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("EradicatorMelee").Type, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("Deathwind").Type, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("StaffoftheMechworm").Type, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("CosmiliteBar").Type, 1, 2, 25, 35));
			}
			#endregion
			#region Thingyouwillneverget
			if (npc.type == Mod.Find<ModNPC>("Yharon").Type && Main.rand.Next(100) == 0 && npc.localAI[2] == 1f)
			{
				npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("YharimsCrystal").Type, 1));
			}
			#endregion
			#region Rares
			if (npc.type == NPCID.PossessedArmor)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<PsychoticAmulet>(), 200, 150));
				if (CalamityWorldPreTrailer.defiled)
				{
					npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("PsychoticAmulet").Type, 20));
				}
			}
			else if (npc.type == NPCID.SeaSnail)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<SeaShell>(), 3, 2));
			}
			else if (npc.type == NPCID.GiantTortoise)
			{
				
						isExpert.OnSuccess(new OneFromOptionsDropRule(200, 2, new int[]
						{
							ModContent.ItemType<FabledTortoiseShell>(),
							ModContent.ItemType<GiantTortoiseShell>(),
						}));
				
						notExpert.OnSuccess(new OneFromOptionsDropRule(203, 2,new int[]
						{
							ModContent.ItemType<FabledTortoiseShell>(),
							ModContent.ItemType<GiantTortoiseShell>(),
						}));
						npcLoot.Add(isExpert);
						npcLoot.Add(notExpert);
			}
			else if (npc.type == NPCID.GiantShelly || npc.type == NPCID.GiantShelly2)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<GiantShell>(), 7, 5));
			}
			else if (npc.type == NPCID.AnomuraFungus)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<FungalCarapace>(), 7, 5));
			}
			else if (npc.type == NPCID.Crawdad || npc.type == NPCID.Crawdad2)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<CrawCarapace>(), 7, 5));
			}
			else if (npc.type == NPCID.GreenJellyfish)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<VitalJelly>(), 7, 5));
			}
			else if (npc.type == NPCID.PinkJellyfish)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<LifeJelly>(), 7, 5));
				
			}
			else if (npc.type == NPCID.BlueJellyfish)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ManaJelly>(), 7, 5));
			}
			else if (npc.type == NPCID.MossHornet)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Needler>(), 25, 20));
			}
			else if (npc.type == NPCID.DarkCaster)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<AncientShiv>(), 25, 20));
			}
			else if (npc.type == NPCID.BigMimicCorruption || npc.type == NPCID.BigMimicCrimson || npc.type == NPCID.BigMimicHallow || npc.type == NPCID.BigMimicJungle)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<CelestialClaymore>(), 7, 5));
			}
			else if (npc.type == NPCID.Clinger)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<CursedDagger>(), 25, 20));
			}
			else if (npc.type == NPCID.Shark)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(Mod.Find<ModItem>("DepthBlade").Type, 15, 10));
				npcLoot.Add(ItemDropRule.NormalvsExpert(ItemID.SharkToothNecklace, 30, 20));
				npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), ItemID.SharkToothNecklace, 20));
			}
			else if (npc.type == NPCID.PresentMimic)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<HolidayHalberd>(), 7, 5));
			}
			else if (npc.type == NPCID.IchorSticker)
			{
						isExpert.OnSuccess(new OneFromOptionsDropRule(200, 2, new int[]
						{
							ModContent.ItemType<SpearofDestiny>(),
							ModContent.ItemType<IchorSpear>(),
						}));
				
				
						notExpert.OnSuccess(new OneFromOptionsDropRule(200, 2, new int[]
						{
							ModContent.ItemType<SpearofDestiny>(),
							ModContent.ItemType<IchorSpear>(),
						}));
						npcLoot.Add(isExpert);
						npcLoot.Add(notExpert);
			}
			else if (npc.type == NPCID.Harpy && NPC.downedBoss1)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<SkyGlaze>(), 40, 30));
				npcLoot.Add(ItemDropRule.ByCondition(new DefiledCondition(), Mod.Find<ModItem>("SkyGlaze").Type,20));
			}
			else if (npc.type == NPCID.Antlion || npc.type == NPCID.WalkingAntlion || npc.type == NPCID.FlyingAntlion)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<MandibleBow>(), 40, 30));
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<MandibleClaws>(), 40, 30));
			}
			else if (npc.type == NPCID.TombCrawlerHead)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BurntSienna>(), 15, 10));
			}
			else if (npc.type == NPCID.DuneSplicerHead && NPC.downedPlantBoss)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Terracotta>(), 15, 10));
			}
			else if (npc.type == NPCID.MartianSaucerCore)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<NullificationRifle>(), 7, 5));
				
			}
			else if (npc.type == NPCID.Demon)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BladecrestOathsword>(), 25, 20));
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<DemonicBoneAsh>(), 3, 2));
			}
			else if (npc.type == NPCID.BoneSerpentHead)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<OldLordOathsword>(), 25, 20));
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<DemonicBoneAsh>(), 3, 2));
			}
			else if (npc.type == NPCID.Tim)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<PlasmaRod>(), 3, 2));
			}
			else if (npc.type == NPCID.GoblinSorcerer)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<PlasmaRod>(), 25, 20));
			}
			else if (npc.type == NPCID.PirateDeadeye)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ProporsePistol>(), 25, 20));
			}
			else if (npc.type == NPCID.PirateCrossbower)
			{
				
						isExpert.OnSuccess(new OneFromOptionsDropRule(200, 2, new int[]
						{
							ModContent.ItemType<Arbalest>(),
							ModContent.ItemType<RaidersGlory>(),
						}));
						
						notExpert.OnSuccess(new OneFromOptionsDropRule(200, 2, new int[]
						{
							ModContent.ItemType<Arbalest>(),
							ModContent.ItemType<RaidersGlory>(),
						}));
						npcLoot.Add(isExpert);
						npcLoot.Add(notExpert);
			}
			else if (npc.type == NPCID.GoblinSummoner)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<TheFirstShadowflame>(), 7, 5));
			}
			else if (npc.type == NPCID.SandElemental)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<WifeinaBottle>(), 7, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("WifeinaBottlewithBoobs").Type, 20));
			}
			else if (CalamityModClassicPreTrailer.skeletonList.Contains(npc.type))
			{
				isExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(),Mod.Find<ModItem>("Waraxe").Type, 15));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), Mod.Find<ModItem>("Waraxe").Type, 20)); ;
				npcLoot.Add(isExpert);
				npcLoot.Add(notExpert);
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<AncientBoneDust>(), 5, 4));
			}
			else if (npc.type == NPCID.GoblinWarrior)
			{
				isExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(),Mod.Find<ModItem>("Warblade").Type, 15));
				notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), Mod.Find<ModItem>("Warblade").Type, 20));
				npcLoot.Add(isExpert);
				npcLoot.Add(notExpert);
			}
			else if (npc.type == NPCID.MartianWalker)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Wingman>(), 5, 7));
			}
			else if (npc.type == NPCID.GiantCursedSkull || npc.type == NPCID.NecromancerArmored || npc.type == NPCID.Necromancer)
			{
				if (npc.type == NPCID.GiantCursedSkull)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedLeviathan(),Mod.Find<ModItem>("Keelhaul").Type, 10));
				}
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<WrathoftheAncients>(), 25, 20));
			}
			#endregion
			#region Commons
			if (npc.type == NPCID.Vulture)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<DesertFeather>(), 1, 1, 3));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DesertFeather>(), 1));
			}
			else if (CalamityModClassicPreTrailer.dungeonEnemyBuffList.Contains(npc.type))
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<Ectoblood>(), 1, 1, 3));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Ectoblood>(), 1));
			}
			else if (npc.type == NPCID.RedDevil)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<EssenceofChaos>(), 2, 1));
			}
			else if (npc.type == NPCID.WyvernHead)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<EssenceofCinder>(), 1, 1, 3));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<EssenceofCinder>(), 1));
			}
			else if (npc.type == NPCID.AngryNimbus)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<EssenceofCinder>(), 3, 2));
			}
			else if (npc.type == NPCID.IceTortoise || npc.type == NPCID.IcyMerman)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<EssenceofEleum>(), 3, 2));
			}
			else if (npc.type == NPCID.IceGolem)
			{
				npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("EssenceofEleum").Type, 1, 1, 3));
			}
			else if (npc.type == NPCID.Plantera)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("LivingShard").Type, 1, 6, 10));
			}
			else if (npc.type == NPCID.NebulaBrain || npc.type == NPCID.NebulaSoldier || npc.type == NPCID.NebulaHeadcrab || npc.type == NPCID.NebulaBeast)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<MeldBlob>(), 1, 2, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<MeldBlob>(), 1, 1, 3));
			}
			else if (npc.type == NPCID.CultistBoss)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<StardustStaff>(), 3, 5));
				npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("ThornBlossom").Type, 40));
			}
			else if (npc.type == NPCID.EyeofCthulhu)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("VictoryShard").Type, 1, 2, 2, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("TeardropCleaver").Type, 1));
			}
			else if (npc.type == NPCID.DevourerHead || npc.type == NPCID.SeekerHead)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(Mod.Find<ModItem>("FetidEssence").Type, 3, 2));
			}
			else if (npc.type == NPCID.FaceMonster || npc.type == NPCID.Herpling)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(Mod.Find<ModItem>("BloodlettingEssence").Type, 5, 4));
			}
			else if (npc.type == NPCID.ManEater)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(Mod.Find<ModItem>("ManeaterBulb").Type, 3, 2));
			}
			else if (npc.type == NPCID.AngryTrapper)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(Mod.Find<ModItem>("TrapperBulb").Type, 5, 4));
			}
			else if (npc.type == NPCID.MotherSlime || npc.type == NPCID.Crimslime || npc.type == NPCID.CorruptSlime)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(Mod.Find<ModItem>("MurkySludge").Type, 4, 3));
			}
			else if (npc.type == NPCID.Moth)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(Mod.Find<ModItem>("GypsyPowder").Type, 2, 1));
			}
			else if (npc.type == NPCID.Derpling)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(Mod.Find<ModItem>("BeetleJuice").Type, 5, 4));
			}
			else if (npc.type == NPCID.SpikedJungleSlime || npc.type == NPCID.Arapaima)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(Mod.Find<ModItem>("MurkyPaste").Type, 5, 4));
			}
			#endregion
			#region Boss Specials
			if (npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail || npc.type == NPCID.BrainofCthulhu)
			{
				LeadingConditionRule worldIsCrimson = new LeadingConditionRule(new CorruptionVSCrimson());
				if (npc.boss)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedEvilBoss(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 2, 2));
					
					npcLoot.Add(worldIsCrimson.OnSuccess(ItemDropRule.ByCondition(new NotDownedEvilBoss(), Mod.Find<ModItem>("Knowledge8").Type, 1)));
					npcLoot.Add(worldIsCrimson.OnSuccess(ItemDropRule.ByCondition(new NotDownedEvilBoss(), Mod.Find<ModItem>("Knowledge11").Type, 1)));
						
					npcLoot.Add(worldIsCrimson.OnFailedConditions(ItemDropRule.ByCondition(new NotDownedEvilBoss(), Mod.Find<ModItem>("Knowledge9").Type, 1)));
					npcLoot.Add(worldIsCrimson.OnFailedConditions(ItemDropRule.ByCondition(new NotDownedEvilBoss(), Mod.Find<ModItem>("Knowledge12").Type, 1)));
				}
			}
			else if (npc.type == NPCID.SkeletronHead)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("ClothiersWrath").Type, 40));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedSkeletron(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 3, 3, 3));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedSkeletron(), Mod.Find<ModItem>("GrenadeRounds").Type, 1)); 
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedSkeletron(), Mod.Find<ModItem>("Knowledge17").Type, 1));
			}
			else if (npc.type == NPCID.WallofFlesh)
			{
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("MLGRune").Type, 1));
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("Meowthrower").Type, 5));
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("RogueEmblem").Type, 8));
				
						notExpert.OnSuccess(ItemDropRule.OneFromOptions(5, new int[]
						{
							ItemID.CrimsonKey,
							ItemID.CorruptionKey,
						}));
						npcLoot.Add(notExpert);
				
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 3, 3));
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), Mod.Find<ModItem>("GrenadeRounds").Type, 1)); 
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), Mod.Find<ModItem>("Knowledge7").Type, 1));
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), Mod.Find<ModItem>("Knowledge18").Type, 1));
			}
			else if (npc.type == NPCID.SkeletronPrime || npc.type == Mod.Find<ModNPC>("BrimstoneElemental").Type)
			{
				if (npc.type == Mod.Find<ModNPC>("BrimstoneElemental").Type)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedBrimstoneElemental(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4)); 
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedBrimstoneElemental(),Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2)); 
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedBrimstoneElemental(),Mod.Find<ModItem>("ExplosiveShells").Type, 1)); 
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedBrimstoneElemental(),Mod.Find<ModItem>("Knowledge6").Type, 1));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedBrimstoneElemental(),Mod.Find<ModItem>("Knowledge26").Type, 1));
				}
			}
			else if (npc.type == NPCID.Plantera || npc.type == Mod.Find<ModNPC>("CalamitasRun3").Type)
			{
				if (npc.type == NPCID.Plantera)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.JungleKey, 5));
				}
				if (npc.type == Mod.Find<ModNPC>("CalamitasRun3").Type)
				{
					npc.DropItemInstanced(npc.position, npc.Size, ItemID.BrokenHeroSword, 1, true);
					if (!CalamityWorldPreTrailer.downedCalamitas)
					{
						npcLoot.Add(ItemDropRule.ByCondition(new NotDownedCalDoppel(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
						npcLoot.Add(ItemDropRule.ByCondition(new NotDownedCalDoppel(),Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
						npcLoot.Add(ItemDropRule.ByCondition(new NotDownedCalDoppel(),Mod.Find<ModItem>("ExplosiveShells").Type, 1));
						npcLoot.Add(ItemDropRule.ByCondition(new NotDownedCalDoppel(),Mod.Find<ModItem>("Knowledge24").Type, 1));
					}
				}
			}
			else if (npc.type == NPCID.Golem)
			{
				bool downedIdiot = CalamityWorldPreTrailer.downedGolemBaby;
				if (!downedIdiot)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedGolem(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedGolem(),Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedGolem(),Mod.Find<ModItem>("ExplosiveShells").Type, 1));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedGolem(),Mod.Find<ModItem>("Knowledge31").Type, 1));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedGolem(),ItemID.Picksaw, 1));
				}
			}
			else if (npc.type == NPCID.MoonLordCore)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("MLGRune2").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("Infinity").Type, 40));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("GrandDad").Type, 40));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedMoonLord(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 5, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedMoonLord(),Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedMoonLord(),Mod.Find<ModItem>("ExplosiveShells").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedMoonLord(),Mod.Find<ModItem>("Knowledge37").Type, 1));
			}
			else if (npc.type == NPCID.DD2Betsy)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedBetsy(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedBetsy(),Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedBetsy(),Mod.Find<ModItem>("ExplosiveShells").Type, 1));
			}
			else if (npc.type == NPCID.Pumpking)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new DownedDoG(),Mod.Find<ModItem>("NightmareFuel").Type, 1, 10, 21));
			}
			else if (npc.type == NPCID.IceQueen)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new DownedDoG(),Mod.Find<ModItem>("EndothermicEnergy").Type, 1, 2, 20, 41));
			}
			else if (npc.type == NPCID.Mothron)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new BuffedEclipseActive(),Mod.Find<ModItem>("DarksunFragment").Type, 1, 10, 21));
			}
			else if (npc.type == Mod.Find<ModNPC>("Astrageldon").Type)
			{
				if (!CalamityWorldPreTrailer.downedAstrageldon)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedAureus(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedAureus(),Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedAureus(),Mod.Find<ModItem>("ExplosiveShells").Type, 1));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedAureus(),Mod.Find<ModItem>("Knowledge30").Type, 1));
				}
			}
			else if (npc.type == Mod.Find<ModNPC>("HiveMindP2").Type) //boss 2
			{
				if (!CalamityWorldPreTrailer.downedHiveMind)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedHiveMind(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 2, 2));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedHiveMind(),Mod.Find<ModItem>("Knowledge14").Type, 1));
				}
			}
			else if (npc.type == Mod.Find<ModNPC>("PerforatorHive").Type) //boss 3
			{
				if (!CalamityWorldPreTrailer.downedPerforator)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPerforators(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 2, 2));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPerforators(),Mod.Find<ModItem>("Knowledge13").Type, 1));
				}
			}
			else if (npc.type == Mod.Find<ModNPC>("SlimeGodCore").Type || npc.type == Mod.Find<ModNPC>("SlimeGodSplit").Type || npc.type == Mod.Find<ModNPC>("SlimeGodRunSplit").Type) //boss 4
			{
				if (npc.type == Mod.Find<ModNPC>("SlimeGodCore").Type)
				{
					LeadingConditionRule revActive = new LeadingConditionRule(new RevCondition());
					LeadingConditionRule purifiedJam = new LeadingConditionRule(new CanGetPurifiedJam(npc));
					LeadingConditionRule corePresent = new LeadingConditionRule(new CorePresent(npc));
					corePresent.OnSuccess(revActive.OnSuccess(purifiedJam.OnSuccess(ItemDropRule.ByCondition(new NotDownedSlimeGod(),Mod.Find<ModItem>("PurifiedJam").Type, 1, 6, 9)))); 
					corePresent.OnSuccess(ItemDropRule.ByCondition(new NotDownedSlimeGod(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 3, 3));
					corePresent.OnSuccess(ItemDropRule.ByCondition(new NotDownedSlimeGod(),Mod.Find<ModItem>("GrenadeRounds").Type, 1)); 
					corePresent.OnSuccess(ItemDropRule.ByCondition(new NotDownedSlimeGod(),Mod.Find<ModItem>("Knowledge15").Type, 1));
					
					corePresent.OnSuccess(new CommonDrop(Mod.Find<ModItem>("SlimeGodTrophy").Type, 10)); 
					corePresent.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("StaticRefiner").Type, 1)); 
					corePresent.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("PurifiedGel").Type, 1, 2, 25, 41)); 
					corePresent.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.Gel, 1, 180, 251)); 
					corePresent.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("OverloadedBlaster").Type, 4)); 
					corePresent.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("GelDart").Type, 4, 80, 101)); 
					corePresent.OnSuccess(notExpert.OnSuccess(ItemDropRule.OneFromOptions(7, new int[]
						{
							ModContent.ItemType<SlimeGodMask>(),
							ModContent.ItemType<SlimeGodMask2>()
						})));
						npcLoot.Add(notExpert);
						npcLoot.Add(revActive);
						npcLoot.Add(purifiedJam);
						npcLoot.Add(corePresent);
						npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("AbyssalTome").Type, 4)); 
						npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("EldritchTome").Type, 4)); 
						npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("CrimslimeStaff").Type, 4)); 
						npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("CorroslimeStaff").Type, 4)); 
				}
				else if (npc.type == Mod.Find<ModNPC>("SlimeGodSplit").Type)
				{
					LeadingConditionRule revActive = new LeadingConditionRule(new RevCondition());
					LeadingConditionRule purifiedJam = new LeadingConditionRule(new CanGetPurifiedJam(npc));
					LeadingConditionRule ebonianPresent = new LeadingConditionRule(new EbonianPresent(npc));
					ebonianPresent.OnSuccess(revActive.OnSuccess(purifiedJam.OnSuccess(ItemDropRule.ByCondition(new NotDownedSlimeGod(),Mod.Find<ModItem>("PurifiedJam").Type, 1, 6, 9)))); 
					ebonianPresent.OnSuccess(ItemDropRule.ByCondition(new NotDownedSlimeGod(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 3, 3));
					ebonianPresent.OnSuccess(ItemDropRule.ByCondition(new NotDownedSlimeGod(),Mod.Find<ModItem>("GrenadeRounds").Type, 1)); 
					ebonianPresent.OnSuccess(ItemDropRule.ByCondition(new NotDownedSlimeGod(),Mod.Find<ModItem>("Knowledge15").Type, 1));
					
					ebonianPresent.OnSuccess(new CommonDrop(Mod.Find<ModItem>("SlimeGodTrophy").Type, 10)); 
					ebonianPresent.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("StaticRefiner").Type, 1)); 
					ebonianPresent.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("PurifiedGel").Type, 1, 2, 25, 41)); 
					ebonianPresent.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.Gel, 1, 180, 251)); 
					ebonianPresent.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("OverloadedBlaster").Type, 4)); 
					ebonianPresent.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("GelDart").Type, 4, 80, 101)); 
					ebonianPresent.OnSuccess(notExpert.OnSuccess(ItemDropRule.OneFromOptions(7, new int[]
					{
						ModContent.ItemType<SlimeGodMask>(),
						ModContent.ItemType<SlimeGodMask2>()
					})));
					npcLoot.Add(notExpert);
					npcLoot.Add(revActive);
					npcLoot.Add(purifiedJam);
					npcLoot.Add(ebonianPresent);
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("AbyssalTome").Type, 4)); 
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("EldritchTome").Type, 4)); 
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("CrimslimeStaff").Type, 4)); 
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("CorroslimeStaff").Type, 4)); 
				}
				else if (npc.type == Mod.Find<ModNPC>("SlimeGodRunSplit").Type)
				{
					LeadingConditionRule revActive = new LeadingConditionRule(new RevCondition());
					LeadingConditionRule purifiedJam = new LeadingConditionRule(new CanGetPurifiedJam(npc));
					LeadingConditionRule crimulanPresent = new LeadingConditionRule(new CrimulanPresent(npc));
					crimulanPresent.OnSuccess(revActive.OnSuccess(purifiedJam.OnSuccess(ItemDropRule.ByCondition(new NotDownedSlimeGod(),Mod.Find<ModItem>("PurifiedJam").Type, 1, 6, 9)))); 
					crimulanPresent.OnSuccess(ItemDropRule.ByCondition(new NotDownedSlimeGod(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 3, 3));
					crimulanPresent.OnSuccess(ItemDropRule.ByCondition(new NotDownedSlimeGod(),Mod.Find<ModItem>("GrenadeRounds").Type, 1)); 
					crimulanPresent.OnSuccess(ItemDropRule.ByCondition(new NotDownedSlimeGod(),Mod.Find<ModItem>("Knowledge15").Type, 1));
					
					crimulanPresent.OnSuccess(new CommonDrop(Mod.Find<ModItem>("SlimeGodTrophy").Type, 10)); 
					crimulanPresent.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("StaticRefiner").Type, 1)); 
					crimulanPresent.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("PurifiedGel").Type, 1, 2, 25, 41)); 
					crimulanPresent.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),ItemID.Gel, 1, 180, 251)); 
					crimulanPresent.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("OverloadedBlaster").Type, 4)); 
					crimulanPresent.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("GelDart").Type, 4, 80, 101)); 
					crimulanPresent.OnSuccess(notExpert.OnSuccess(ItemDropRule.OneFromOptions(7, new int[]
					{
						ModContent.ItemType<SlimeGodMask>(),
						ModContent.ItemType<SlimeGodMask2>()
					})));
					npcLoot.Add(notExpert);
					npcLoot.Add(revActive);
					npcLoot.Add(purifiedJam);
					npcLoot.Add(crimulanPresent);
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("AbyssalTome").Type, 4)); 
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("EldritchTome").Type, 4)); 
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("CrimslimeStaff").Type, 4)); 
					npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(),Mod.Find<ModItem>("CorroslimeStaff").Type, 4)); 
				}
			}
			else if (npc.type == Mod.Find<ModNPC>("Cryogen").Type) //boss 5
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedCryogen(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedCryogen(),Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedCryogen(),Mod.Find<ModItem>("ExplosiveShells").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedCryogen(),Mod.Find<ModItem>("Knowledge19").Type, 1));
			}
			else if (npc.type == Mod.Find<ModNPC>("Siren").Type || npc.type == Mod.Find<ModNPC>("Leviathan").Type) //boss 8
			{
				int bossType = (npc.type == Mod.Find<ModNPC>("Siren").Type) ? Mod.Find<ModNPC>("Leviathan").Type : Mod.Find<ModNPC>("Siren").Type;
				if (!NPC.AnyNPCs(bossType))
				{
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedLeviathan(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedLeviathan(),Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedLeviathan(),Mod.Find<ModItem>("ExplosiveShells").Type, 1));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedLeviathan(),Mod.Find<ModItem>("Knowledge10").Type, 1));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedLeviathan(),Mod.Find<ModItem>("Knowledge28").Type, 1));
				}
			}
			else if (npc.type == Mod.Find<ModNPC>("PlaguebringerGoliath").Type) //boss 9
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPBG(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPBG(),Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPBG(),Mod.Find<ModItem>("ExplosiveShells").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPBG(),Mod.Find<ModItem>("Knowledge32").Type, 1));
			}
			else if (npc.type == Mod.Find<ModNPC>("ProfanedGuardianBoss").Type) //boss 10
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedGuardians(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 5, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedGuardians(),Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedGuardians(),Mod.Find<ModItem>("ExplosiveShells").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedGuardians(),Mod.Find<ModItem>("Knowledge38").Type, 1));
			}
			else if (npc.type == Mod.Find<ModNPC>("Providence").Type) //boss 11
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedProvidence(),Mod.Find<ModItem>("MagnumRounds").Type, 1, 5, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedProvidence(),Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedProvidence(),Mod.Find<ModItem>("ExplosiveShells").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedProvidence(),Mod.Find<ModItem>("Knowledge39").Type, 1));
			}
			else if (npc.type == Mod.Find<ModNPC>("CeaselessVoid").Type) //boss 12
			{
				LeadingConditionRule downedSW = new LeadingConditionRule(new DownedStormWeaver());
				LeadingConditionRule downedSignus = new LeadingConditionRule(new DownedSignus());

				downedSW.OnSuccess(downedSignus.OnSuccess(ItemDropRule.ByCondition(new DownedCeaseless(), Mod.Find<ModItem>("Knowledge40").Type, 1)));
				
				npcLoot.Add(ItemDropRule.ByCondition(new DownedCeaseless(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 5, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new DownedCeaseless(), Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new DownedCeaseless(), Mod.Find<ModItem>("ExplosiveShells").Type, 1));
			}
			else if (npc.type == Mod.Find<ModNPC>("StormWeaverHeadNaked").Type) //boss 13
			{
				LeadingConditionRule downedCeaseless = new LeadingConditionRule(new DownedCeaseless());
				LeadingConditionRule downedSignus = new LeadingConditionRule(new DownedSignus());

				downedCeaseless.OnSuccess(downedSignus.OnSuccess(ItemDropRule.ByCondition(new DownedCeaseless(), Mod.Find<ModItem>("Knowledge40").Type, 1)));
				
				npcLoot.Add(ItemDropRule.ByCondition(new DownedStormWeaver(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 5, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new DownedStormWeaver(), Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new DownedStormWeaver(), Mod.Find<ModItem>("ExplosiveShells").Type, 1));
			}
			else if (npc.type == Mod.Find<ModNPC>("CosmicWraith").Type) //boss 14
			{
				LeadingConditionRule downedCeaseless = new LeadingConditionRule(new DownedCeaseless());
				LeadingConditionRule downedSW = new LeadingConditionRule(new DownedStormWeaver());
				
				downedCeaseless.OnSuccess(downedSW.OnSuccess(ItemDropRule.ByCondition(new DownedCeaseless(), Mod.Find<ModItem>("Knowledge40").Type, 1)));
				
				npcLoot.Add(ItemDropRule.ByCondition(new DownedSignus(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 5, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new DownedSignus(), Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new DownedSignus(), Mod.Find<ModItem>("ExplosiveShells").Type, 1));	
			}
			else if (npc.type == Mod.Find<ModNPC>("Bumblefuck").Type) //boss 16
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedBumblebirb(), Mod.Find<ModItem>("Knowledge43").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedBumblebirb(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 5, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedBumblebirb(), Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedBumblebirb(), Mod.Find<ModItem>("ExplosiveShells").Type, 1));
			}
			else if (npc.type == Mod.Find<ModNPC>("Yharon").Type) //boss 17
			{
				LeadingConditionRule darkSun = new LeadingConditionRule(new DarkSunCondition());
				
				darkSun.OnSuccess(ItemDropRule.ByCondition(new NotDownedYharon(), Mod.Find<ModItem>("Knowledge44").Type, 1));
				darkSun.OnSuccess(ItemDropRule.ByCondition(new NotDownedYharon(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 6, 6));
				darkSun.OnSuccess(ItemDropRule.ByCondition(new NotDownedYharon(), Mod.Find<ModItem>("GrenadeRounds").Type, 1, 3, 3));
				darkSun.OnSuccess(ItemDropRule.ByCondition(new NotDownedYharon(), Mod.Find<ModItem>("ExplosiveShells").Type, 1, 2, 2));
				npcLoot.Add(darkSun);
			}
			else if (npc.type == Mod.Find<ModNPC>("SupremeCalamitas").Type) //boss 18
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedSCal(), Mod.Find<ModItem>("Knowledge45").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedSCal(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 6, 6));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedSCal(), Mod.Find<ModItem>("GrenadeRounds").Type, 1, 3, 3));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedSCal(), Mod.Find<ModItem>("ExplosiveShells").Type, 1, 2, 2));
			}
			else if (npc.type == Mod.Find<ModNPC>("CrabulonIdle").Type) //boss 19
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedCrabulon(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedCrabulon(), Mod.Find<ModItem>("Knowledge5").Type, 1));
			}
			else if (npc.type == Mod.Find<ModNPC>("ScavengerBody").Type) //boss 20
			{
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedRavager(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 4, 4));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedRavager(), Mod.Find<ModItem>("GrenadeRounds").Type, 1, 2, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedRavager(), Mod.Find<ModItem>("ExplosiveShells").Type, 1));
				npcLoot.Add(ItemDropRule.ByCondition(new NotDownedRavager(), Mod.Find<ModItem>("Knowledge33").Type, 1));
			}
			else if (npc.type == Mod.Find<ModNPC>("Polterghast").Type) //boss 21
			{
				if (!CalamityWorldPreTrailer.downedPolterghast)
				{
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPolterghast(), Mod.Find<ModItem>("Knowledge41").Type, 1));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPolterghast(), Mod.Find<ModItem>("MagnumRounds").Type, 1, 6, 6));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPolterghast(), Mod.Find<ModItem>("GrenadeRounds").Type, 1, 3, 3));
					npcLoot.Add(ItemDropRule.ByCondition(new NotDownedPolterghast(), Mod.Find<ModItem>("ExplosiveShells").Type, 1, 2, 2));
				}
			}
			/*else if (npc.type == mod.NPCType("OldDuke")) //boss 23
            {
                CalamityWorld.downedOldDuke = true;
            }*/
			
			if (npc.type == Mod.Find<ModNPC>("SupremeCalamitas").Type)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new DeathCondition(),Mod.Find<ModItem>("Levi").Type, 1));
			}
			#endregion
		}
		#endregion

		#region DespawnHostileProjectiles
		public void DespawnProj()
		{
			int proj;
			for (int x = 0; x < 1000; x = proj + 1)
			{
				Projectile projectile = Main.projectile[x];
				if (projectile.active && projectile.hostile && !projectile.friendly && projectile.damage > 0)
				{
					projectile.Kill();
				}
				proj = x;
			}
		}
		#endregion
	}
}