using System;
using System.Collections.Generic;
using CalamityModClassicPreTrailer.Items.AquaticScourge;
using CalamityModClassicPreTrailer.Items.Astrageldon;
using CalamityModClassicPreTrailer.Items.AstrumDeus;
using CalamityModClassicPreTrailer.Items.BrimstoneWaifu;
using CalamityModClassicPreTrailer.Items.Bumblefuck;
using CalamityModClassicPreTrailer.Items.Calamitas;
using CalamityModClassicPreTrailer.Items.Crabulon;
using CalamityModClassicPreTrailer.Items.Cryogen;
using CalamityModClassicPreTrailer.Items.DesertScourge;
using CalamityModClassicPreTrailer.Items.DevourerMunsters;
using CalamityModClassicPreTrailer.Items.HiveMind;
using CalamityModClassicPreTrailer.Items.Perforator;
using CalamityModClassicPreTrailer.Items.PlaguebringerGoliath;
using CalamityModClassicPreTrailer.Items.Polterghast;
using CalamityModClassicPreTrailer.Items.ProfanedGuardian;
using CalamityModClassicPreTrailer.Items.Providence;
using CalamityModClassicPreTrailer.Items.Scavenger;
using CalamityModClassicPreTrailer.Items.SlimeGod;
using CalamityModClassicPreTrailer.Items.SupremeCalamitas;
using CalamityModClassicPreTrailer.Items.TheDevourerofGods;
using CalamityModClassicPreTrailer.Items.Yharon;
using CalamityModClassicPreTrailer.NPCs.AbyssNPCs;
using CalamityModClassicPreTrailer.NPCs.Astrageldon;
using CalamityModClassicPreTrailer.NPCs.AstrumDeus;
using CalamityModClassicPreTrailer.NPCs.BrimstoneWaifu;
using CalamityModClassicPreTrailer.NPCs.Bumblefuck;
using CalamityModClassicPreTrailer.NPCs.Calamitas;
using CalamityModClassicPreTrailer.NPCs.CeaselessVoid;
using CalamityModClassicPreTrailer.NPCs.CosmicWraith;
using CalamityModClassicPreTrailer.NPCs.Crabulon;
using CalamityModClassicPreTrailer.NPCs.Cryogen;
using CalamityModClassicPreTrailer.NPCs.DesertScourge;
using CalamityModClassicPreTrailer.NPCs.HiveMind;
using CalamityModClassicPreTrailer.NPCs.Leviathan;
using CalamityModClassicPreTrailer.NPCs.Perforator;
using CalamityModClassicPreTrailer.NPCs.PlaguebringerGoliath;
using CalamityModClassicPreTrailer.NPCs.Polterghast;
using CalamityModClassicPreTrailer.NPCs.ProfanedGuardianBoss;
using CalamityModClassicPreTrailer.NPCs.Providence;
using CalamityModClassicPreTrailer.NPCs.Scavenger;
using CalamityModClassicPreTrailer.NPCs.SlimeGod;
using CalamityModClassicPreTrailer.NPCs.StormWeaver;
using CalamityModClassicPreTrailer.NPCs.SupremeCalamitas;
using CalamityModClassicPreTrailer.NPCs.TheDevourerofGods;
using CalamityModClassicPreTrailer.NPCs.Yharon;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer
{
	internal class WeakReferenceSupport
	{
		public static void Setup()
		{
			BossChecklistSupport();
			CensusSupport();
		}

		private static void BossChecklistSupport()
		{
			// Mod mod = ModLoader.GetMod("CalamityModClassicPreTrailer");
			if (ModLoader.HasMod("BossChecklist"))
			{
				Mod bossChecklist = ModLoader.GetMod("BossChecklist");
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"DesertScourge",
					1.5f,
					() => CalamityWorldPreTrailer.downedDesertScourge,
					ModContent.NPCType<DesertScourgeHead>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<DriedSeafood>(),
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.DesertScourgeHead.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"Crabulon",
					2.5f,
					() => CalamityWorldPreTrailer.downedCrabulon,
					ModContent.NPCType<CrabulonIdle>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<DecapoditaSprout>(),
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.CrabulonIdle.SpawnInfo")
					});
				Func<bool> isCorruption = () => !WorldGen.crimson || Main.drunkWorld;
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"HiveMind",
					3.5f,
					() => CalamityWorldPreTrailer.downedHiveMind,
					ModContent.NPCType<HiveMind>(),
					new Dictionary<string, object>()
					{
						["availability"] = isCorruption,
						["spawnItems"] = ModContent.ItemType<Teratoma>(),
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.HiveMind.SpawnInfo")
					});
				Func<bool> isCrimson = () => WorldGen.crimson || Main.drunkWorld;
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"Perforator",
					3.5f,
					() => CalamityWorldPreTrailer.downedPerforator,
					ModContent.NPCType<PerforatorHive>(),
					new Dictionary<string, object>()
					{
						["availability"] = isCrimson,
						["spawnItems"] = ModContent.ItemType<BloodyWormFood>(),
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.PerforatorHive.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"SlimeGod",
					5.5f,
					() => CalamityWorldPreTrailer.downedSlimeGod,
					ModContent.NPCType<SlimeGodCore>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<OverloadedSludge>(),
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.SlimeGodCore.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"Cryogen",
					8.5f,
					() => CalamityWorldPreTrailer.downedCryogen,
					ModContent.NPCType<Cryogen>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<CryoKey>(),
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.Cryogen.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"BrimstoneElemental",
					9.5f,
					() => CalamityWorldPreTrailer.downedBrimstoneElemental,
					ModContent.NPCType<BrimstoneElemental>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<CharredIdol>(),
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.BrimstoneElemental.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"AquaticScourge",
					10.5f,
					() => CalamityWorldPreTrailer.downedAquaticScourge,
					ModContent.NPCType<AquaticScourgeHead>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Seafood>(),
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.AquaticScourgeHead.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"Calamitas",
					11.7f,
					() => CalamityWorldPreTrailer.downedCalamitas,
					ModContent.NPCType<Calamitas>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<BlightedEyeball>(),
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.Calamitas.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"Leviathan",
					12.5f,
					() => CalamityWorldPreTrailer.downedLeviathan,
					ModContent.NPCType<Leviathan>(),
					new Dictionary<string, object>()
					{
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.Leviathan.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"AstrumAureus",
					12.55f,
					() => CalamityWorldPreTrailer.downedAstrageldon,
					ModContent.NPCType<Astrageldon>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<AstralChunk>(),
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.Astrageldon.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"AstrumDeus",
					12.6f,
					() => CalamityWorldPreTrailer.downedStarGod,
					ModContent.NPCType<AstrumDeusHead>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Starcore>(),
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.AstrumDeusHead.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"PlaguebringerGoliath",
					13.5f,
					() => CalamityWorldPreTrailer.downedPlaguebringer,
					ModContent.NPCType<PlaguebringerGoliath>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Abomination>(),
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.PlaguebringerGoliath.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"Ravager",
					14.5f,
					() => CalamityWorldPreTrailer.downedScavenger,
					ModContent.NPCType<ScavengerBody>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<AncientMedallion>(),
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.ScavengerBody.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"ProfanedGuardians",
					18.5f,
					() => CalamityWorldPreTrailer.downedGuardians,
					ModContent.NPCType<ProfanedGuardianBoss>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<ProfanedShard>(),
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.ProfanedGuardianBoss.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"Bumblebirb",
					18.6f,
					() => CalamityWorldPreTrailer.downedBumble,
					ModContent.NPCType<Bumblefuck>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<BirbPheromones>(),
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.Bumblefuck.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"Providence",
					19f,
					() => CalamityWorldPreTrailer.downedProvidence,
					ModContent.NPCType<Providence>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<ProfanedCore>(), 
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.Providence.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"CeaselessVoid",
					19.1f,
					() => CalamityWorldPreTrailer.downedSentinel1,
					ModContent.NPCType<CeaselessVoid>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<RuneofCos>(), 
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.CeaselessVoid.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"StormWeaver",
					19.2f,
					() => CalamityWorldPreTrailer.downedSentinel2,
					ModContent.NPCType<StormWeaverHead>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<RuneofCos>(), 
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.StormWeaverHead.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"Signus",
					19.3f,
					() => CalamityWorldPreTrailer.downedSentinel3,
					ModContent.NPCType<CosmicWraith>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<RuneofCos>(), 
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.CosmicWraith.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"Polterghast",
					19.5f,
					() => CalamityWorldPreTrailer.downedPolterghast,
					ModContent.NPCType<Polterghast>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<NecroplasmicBeacon>(), 
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.Polterghast.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"DevourerofGods",
					20f,
					() => CalamityWorldPreTrailer.downedDoG,
					ModContent.NPCType<DevourerofGodsHead>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<CosmicWorm>(), 
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.DevourerofGodsHead.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"Yharon",
					21f,
					() => CalamityWorldPreTrailer.downedYharon,
					ModContent.NPCType<Yharon>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<ChickenEgg>(), 
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.Yharon.SpawnInfo")
					});
				bossChecklist.Call(
					"LogBoss",
					CalamityModClassicPreTrailer.Instance,
					"SupremeCalamitas",
					22f,
					() => CalamityWorldPreTrailer.downedSCal,
					ModContent.NPCType<SupremeCalamitas>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<EyeofExtinction>(), 
						["spawnInfo"] = CalamityModClassicPreTrailer.Instance.GetLocalization("NPCs.SupremeCalamitas.SpawnInfo")
					});
				/*
				original checklist code
				// 14 is moonlord, 12 is duke fishron
				bossChecklist.Call("AddBossWithInfo", "Desert Scourge", 1.5f, (Func<bool>)(() => CalamityWorldPreTrailer.downedDesertScourge), "Use a [i:" + mod.Find<ModItem>("DriedSeafood").Type + "] in the Desert Biome"); //1
				bossChecklist.Call("AddBossWithInfo", "Crabulon", 2.5f, (Func<bool>)(() => CalamityWorldPreTrailer.downedCrabulon), "Use a [i:" + mod.Find<ModItem>("DecapoditaSprout").Type + "] in the Mushroom Biome"); //1.5
				bossChecklist.Call("AddBossWithInfo", "Hive Mind / Perforator", 3.5f, (Func<bool>)(() => (CalamityWorldPreTrailer.downedPerforator || CalamityWorldPreTrailer.downedHiveMind)), "By killing a Cyst in the World's Evil Biome"); //2
				bossChecklist.Call("AddBossWithInfo", "Slime God", 5.5f, (Func<bool>)(() => CalamityWorldPreTrailer.downedSlimeGod), "Use an [i:" + mod.Find<ModItem>("OverloadedSludge").Type + "]"); //4
				bossChecklist.Call("AddBossWithInfo", "Cryogen", 6.5f, (Func<bool>)(() => CalamityWorldPreTrailer.downedCryogen), "Use a [i:" + mod.Find<ModItem>("CryoKey").Type + "] in the Snow Biome"); //5
				bossChecklist.Call("AddBossWithInfo", "Brimstone Elemental", 7.5f, (Func<bool>)(() => CalamityWorldPreTrailer.downedBrimstoneElemental), "Use a [i:" + mod.Find<ModItem>("CharredIdol").Type + "] in the Hell Crag"); //6
				bossChecklist.Call("AddBossWithInfo", "Aquatic Scourge", 8.5f, (Func<bool>)(() => CalamityWorldPreTrailer.downedAquaticScourge), "Use a [i:" + mod.Find<ModItem>("Seafood").Type + "] in the Sulphuric Sea or wait for it to spawn in the Sulphuric Sea"); //6
				bossChecklist.Call("AddBossWithInfo", "Calamitas", 9.7f, (Func<bool>)(() => CalamityWorldPreTrailer.downedCalamitas), "Use an [i:" + mod.Find<ModItem>("BlightedEyeball").Type + "] at Night"); //7
				bossChecklist.Call("AddBossWithInfo", "Leviathan", 10.5f, (Func<bool>)(() => CalamityWorldPreTrailer.downedLeviathan), "By killing an unknown entity in the Ocean Biome"); //8
				bossChecklist.Call("AddBossWithInfo", "Astrum Aureus", 10.55f, (Func<bool>)(() => CalamityWorldPreTrailer.downedAstrageldon), "Use an [i:" + mod.Find<ModItem>("AstralChunk").Type + "] at Night"); //8.25
				bossChecklist.Call("AddBossWithInfo", "Astrum Deus", 10.6f, (Func<bool>)(() => CalamityWorldPreTrailer.downedStarGod), "Use a [i:" + mod.Find<ModItem>("Starcore").Type + "] at Night"); //8.5
				bossChecklist.Call("AddBossWithInfo", "Plaguebringer Goliath", 11.5f, (Func<bool>)(() => CalamityWorldPreTrailer.downedPlaguebringer), "Use an [i:" + mod.Find<ModItem>("Abomination").Type + "] in the Jungle Biome"); //9
				bossChecklist.Call("AddBossWithInfo", "Ravager", 12.5f, (Func<bool>)(() => CalamityWorldPreTrailer.downedScavenger), "Use an [i:" + mod.Find<ModItem>("AncientMedallion").Type + "]"); //9.5
				//bossChecklist.Call("AddBossWithInfo", "The Old Duke", 13.5f, (Func<bool>)(() => CalamityWorld.downedOldDuke), "Fishing with some type of bait in the Sulphuric Sea"); //9.6
				bossChecklist.Call("AddBossWithInfo", "Profaned Guardians", 14.5f, (Func<bool>)(() => CalamityWorldPreTrailer.downedGuardians), "Use a [i:" + mod.Find<ModItem>("ProfanedShard").Type + "] in the Hallow or Underworld Biomes"); //10
				bossChecklist.Call("AddBossWithInfo", "Bumblebirb", 14.6f, (Func<bool>)(() => CalamityWorldPreTrailer.downedBumble), "Use [i:" + mod.Find<ModItem>("BirbPheromones").Type + "] in the Jungle Biome"); //16
				bossChecklist.Call("AddBossWithInfo", "Providence", 15f, (Func<bool>)(() => CalamityWorldPreTrailer.downedProvidence), "Use a [i:" + mod.Find<ModItem>("ProfanedCore").Type + "] in the Hallow or Underworld Biomes"); //11
				bossChecklist.Call("AddBossWithInfo", "Ceaseless Void", 15.1f, (Func<bool>)(() => CalamityWorldPreTrailer.downedSentinel1), "Use a [i:" + mod.Find<ModItem>("RuneofCos").Type + "] in the Dungeon"); //12
				bossChecklist.Call("AddBossWithInfo", "Storm Weaver", 15.2f, (Func<bool>)(() => CalamityWorldPreTrailer.downedSentinel2), "Use a [i:" + mod.Find<ModItem>("RuneofCos").Type + "] in Space"); //13
				bossChecklist.Call("AddBossWithInfo", "Signus", 15.3f, (Func<bool>)(() => CalamityWorldPreTrailer.downedSentinel3), "Use a [i:" + mod.Find<ModItem>("RuneofCos").Type + "] in the Underworld"); //14
				bossChecklist.Call("AddBossWithInfo", "Polterghast", 15.5f, (Func<bool>)(() => CalamityWorldPreTrailer.downedPolterghast), "Use a [i:" + mod.Find<ModItem>("NecroplasmicBeacon").Type + "] in the Dungeon or kill 30 phantom spirits"); //11
				bossChecklist.Call("AddBossWithInfo", "Devourer of Gods", 16f, (Func<bool>)(() => CalamityWorldPreTrailer.downedDoG), "Use a [i:" + mod.Find<ModItem>("CosmicWorm").Type + "]"); //15
				bossChecklist.Call("AddBossWithInfo", "Yharon", 17f, (Func<bool>)(() => CalamityWorldPreTrailer.downedYharon), "Use a [i:" + mod.Find<ModItem>("ChickenEgg").Type + "] in the Jungle Biome"); //17
				bossChecklist.Call("AddBossWithInfo", "Supreme Calamitas", 18f, (Func<bool>)(() => CalamityWorldPreTrailer.downedSCal), "Use an [i:" + mod.Find<ModItem>("EyeofExtinction").Type + "]"); //18
				//bossChecklist.Call("AddBossWithInfo", "MEME GOD", 19f, (Func<bool>)(() => CalamityWorld.downedLORDE), "Use [i:" + mod.ItemType("NO") + "]"); //19
				*/
			}
		}

		private static void CensusSupport()
		{
			Mod mod = ModLoader.GetMod("CalamityModClassicPreTrailer");
			
			if (ModLoader.HasMod("Census"))
			{
				Mod censusMod = ModLoader.GetMod("Census");
				censusMod.Call("TownNPCCondition", mod.Find<ModNPC>("SEAHOE").Type, "Defeat a Giant Clam");
				censusMod.Call("TownNPCCondition", mod.Find<ModNPC>("FAP").Type, "Have [i:" + mod.Find<ModItem>("FabsolsVodka").Type + "] in your inventory in Hardmode");
				censusMod.Call("TownNPCCondition", mod.Find<ModNPC>("DILF").Type, "Defeat Cryogen");
			}
		}
	}
}
