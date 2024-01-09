using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.Localization;
using CalamityModClassic1Point2.Tiles;
using CalamityModClassic1Point2.NPCs;
using System.Linq;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;
using Terraria.UI;
using CalamityModClassic1Point2.Items.Accessories;

namespace CalamityModClassic1Point2
{
	public class CalamityWorld : ModSystem
	{
		private const int saveVersion = 0;
		
		public static int calamityTiles = 0;
		
		public static int astralTiles = 0;
		
		public int runCheck = 0;
		
		public static bool spring = false;
    	
    	public static bool summer = false;
    	
    	public static bool fall = false;
    	
    	public static bool winter = false;
		
		public static bool downedDesertScourge = false;
		
		public static bool downedHiveMind = false;
		
		public static bool downedPerforator = false;
		
		public static bool downedSlimeGod = false;
		
		public static bool downedCryogen = false;
		
		public static bool downedBrimstoneElemental = false;
		
		public static bool downedCalamitas = false;
		
		public static bool downedLeviathan = false;
		
		public static bool downedDoG = false;
		
		public static bool downedPlaguebringer = false;
		
		public static bool downedYharon = false;
		
		public static bool downedProvidence = false;
		
		public static bool downedGuardians = false;
		
		public static bool downedSentinel1 = false;
		
		public static bool downedSentinel2 = false;
		
		public static bool downedSentinel3 = false;
		
		public static bool downedSCal = false;
		
		public static bool downedBumble = false;
		
		public static bool downedCrabulon = false;
		
		public static bool downedBetsy = false;
		
		public static bool downedScavenger = false;
		
		public static bool downedWhar = false; //boss 2
		
		public static bool downedSkullHead = false; //boss 3
		
		public static bool downedUgly = false; //Wall
		
		public static bool downedSkeletor = false; //Skele of tron Prime
		
		public static bool downedPlantThing = false; //planter
		
		public static bool downedGolemBaby = false; //Baby
		
		public static bool downedMoonDude = false; //Moon guy
		
		public static bool downedBossAny = false; //Any boss
		
		public static bool spawnedHardBoss = false; //Hardmode boss spawned
		
		public static bool demonMode = false;
		
		public static bool onionMode = false;
		
		public static bool revenge = false;
		
		public static bool downedStarGod = false;
		
		public static bool spawnAstralMeteor = false;
		
		public static bool spawnAstralMeteor2 = false;
		
		public static bool spawnAstralMeteor3 = false;
		
		public static bool downedPolterghast = false;

		public override void OnWorldLoad()/* tModPorter Suggestion: Also override OnWorldUnload, and mirror your worldgen-sensitive data initialization in PreWorldGen */
		{
			runCheck = 0;
			downedDesertScourge = false;
			downedHiveMind = false;
			downedPerforator = false;
			downedSlimeGod = false;
			downedCryogen = false;
			downedBrimstoneElemental = false;
			downedCalamitas = false;
			downedLeviathan = false;
			downedDoG = false;
			downedPlaguebringer = false;
			downedGuardians = false;
			downedProvidence = false;
			downedSentinel1 = false;
			downedSentinel2 = false;
			downedSentinel3 = false;
			downedYharon = false;
			downedSCal = false;
			downedBumble = false;
			downedCrabulon = false;
			downedBetsy = false;
			downedScavenger = false;
			downedWhar = false;
			downedSkullHead = false;
			downedUgly = false;
			downedSkeletor = false;
			downedPlantThing = false;
			downedGolemBaby = false;
			downedMoonDude = false;
			downedBossAny = false;
			spawnedHardBoss = false;
			demonMode = false;
			onionMode = false;
			revenge = false;
			downedStarGod = false;
			spawnAstralMeteor = false;
			spawnAstralMeteor2 = false;
			spawnAstralMeteor3 = false;
			downedPolterghast = false;
		}
		
		public override void SaveWorldData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
		{
			var downed = new List<string>();
			if (downedDesertScourge) downed.Add("desertScourge");
			if (downedHiveMind) downed.Add("hiveMind");
			if (downedPerforator) downed.Add("perforator");
			if (downedSlimeGod) downed.Add("slimeGod");
			if (downedCryogen) downed.Add("cryogen");
			if (downedBrimstoneElemental) downed.Add("brimstoneElemental");
			if (downedCalamitas) downed.Add("calamitas");
			if (downedLeviathan) downed.Add("leviathan");
			if (downedDoG) downed.Add("devourerOfGods");
			if (downedPlaguebringer) downed.Add("plaguebringerGoliath");
			if (downedGuardians) downed.Add("guardians");
			if (downedProvidence) downed.Add("providence");
			if (downedSentinel1) downed.Add("ceaselessVoid");
			if (downedSentinel2) downed.Add("stormWeaver");
			if (downedSentinel3) downed.Add("signus");
			if (downedYharon) downed.Add("yharon");
			if (downedSCal) downed.Add("supremeCalamitas");
			if (downedBumble) downed.Add("bumblebirb");
			if (downedCrabulon) downed.Add("crabulon");
			if (downedBetsy) downed.Add("betsy");
			if (downedScavenger) downed.Add("scavenger");
			if (downedWhar) downed.Add("boss2");
			if (downedSkullHead) downed.Add("boss3");
			if (downedUgly) downed.Add("wall");
			if (downedSkeletor) downed.Add("skeletronPrime");
			if (downedPlantThing) downed.Add("planter");
			if (downedGolemBaby) downed.Add("baby");
			if (downedMoonDude) downed.Add("moonDude");
			if (downedBossAny) downed.Add("anyBoss");
			if (demonMode) downed.Add("demonMode");
			if (onionMode) downed.Add("onionMode");
			if (revenge) downed.Add("revenge");
			if (downedStarGod) downed.Add("starGod");
			if (spawnAstralMeteor) downed.Add("astralMeteor");
			if (spawnAstralMeteor2) downed.Add("astralMeteor2");
			if (spawnAstralMeteor3) downed.Add("astralMeteor3");
			if (spawnedHardBoss) downed.Add("hardBoss");
			if (downedPolterghast) downed.Add("polterghast");
		}

		public override void LoadWorldData(TagCompound tag)
		{
			var downed = tag.GetList<string>("downed");
			downedDesertScourge = downed.Contains("desertScourge");
			downedHiveMind = downed.Contains("hiveMind");
			downedPerforator = downed.Contains("perforator");
			downedSlimeGod = downed.Contains("slimeGod");
			downedCryogen = downed.Contains("cryogen");
			downedBrimstoneElemental = downed.Contains("brimstoneElemental");
			downedCalamitas = downed.Contains("calamitas");
			downedLeviathan = downed.Contains("leviathan");
			downedDoG = downed.Contains("devourerOfGods");
			downedPlaguebringer = downed.Contains("plaguebringerGoliath");
			downedGuardians = downed.Contains("guardians");
			downedProvidence = downed.Contains("providence");
			downedSentinel1 = downed.Contains("ceaselessVoid");
			downedSentinel2 = downed.Contains("stormWeaver");
			downedSentinel3 = downed.Contains("signus");
			downedYharon = downed.Contains("yharon");
			downedSCal = downed.Contains("supremeCalamitas");
			downedBumble = downed.Contains("bumblebirb");
			downedCrabulon = downed.Contains("crabulon");
			downedBetsy = downed.Contains("betsy");
			downedScavenger = downed.Contains("scavenger");
			downedWhar = downed.Contains("boss2");
			downedSkullHead = downed.Contains("boss3");
			downedUgly = downed.Contains("wall");
			downedSkeletor = downed.Contains("skeletronPrime");
			downedPlantThing = downed.Contains("planter");
			downedGolemBaby = downed.Contains("baby");
			downedMoonDude = downed.Contains("moonDude");
			downedBossAny = downed.Contains("anyBoss");
			demonMode = downed.Contains("demonMode");
			onionMode = downed.Contains("onionMode");
			revenge = downed.Contains("revenge");
			downedStarGod = downed.Contains("starGod");
			spawnAstralMeteor = downed.Contains("astralMeteor");
			spawnAstralMeteor2 = downed.Contains("astralMeteor2");
			spawnAstralMeteor3 = downed.Contains("astralMeteor3");
			spawnedHardBoss = downed.Contains("hardBoss");
			downedPolterghast = downed.Contains("polterghast");
		}
		
		public override void NetSend(BinaryWriter writer)
		{
			BitsByte flags = new BitsByte();
			flags[0] = downedDesertScourge;
			flags[1] = downedHiveMind;
			flags[2] = downedPerforator;
			flags[3] = downedSlimeGod;
			flags[4] = downedCryogen;
			flags[5] = downedBrimstoneElemental;
			flags[6] = downedCalamitas;
			flags[7] = downedLeviathan;
			
			BitsByte flags2 = new BitsByte();
			flags2[0] = downedDoG;
			flags2[1] = downedPlaguebringer;
			flags2[2] = downedGuardians;
			flags2[3] = downedProvidence;
			flags2[4] = downedSentinel1;
			flags2[5] = downedSentinel2;
			flags2[6] = downedSentinel3;
			flags2[7] = downedYharon;
			
			BitsByte flags3 = new BitsByte();
			flags3[0] = downedSCal;
			flags3[1] = downedBumble;
			flags3[2] = downedCrabulon;
			flags3[3] = downedBetsy;
			flags3[4] = downedScavenger;
			flags3[5] = downedWhar;
			flags3[6] = downedSkullHead;
			flags3[7] = downedUgly;
			
			BitsByte flags4 = new BitsByte();
			flags4[0] = downedSkeletor;
			flags4[1] = downedPlantThing;
			flags4[2] = downedGolemBaby;
			flags4[3] = downedMoonDude;
			flags4[4] = downedBossAny;
			flags4[5] = demonMode;
			flags4[6] = onionMode;
			flags4[7] = revenge;
			
			BitsByte flags5 = new BitsByte();
			flags5[0] = downedStarGod;
			flags5[1] = spawnAstralMeteor;
			flags5[2] = spawnAstralMeteor2;
			flags5[3] = spawnAstralMeteor3;
			flags5[4] = spawnedHardBoss;
			flags5[5] = downedPolterghast;
			
			writer.Write(flags);
			writer.Write(flags2);
			writer.Write(flags3);
			writer.Write(flags4);
			writer.Write(flags5);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			downedDesertScourge = flags[0];
			downedHiveMind = flags[1];
			downedPerforator = flags[2];
			downedSlimeGod = flags[3];
			downedCryogen = flags[4];
			downedBrimstoneElemental = flags[5];
			downedCalamitas = flags[6];
			downedLeviathan = flags[7];
			
			BitsByte flags2 = reader.ReadByte();
			downedDoG = flags2[0];
			downedPlaguebringer = flags2[1];
			downedGuardians = flags2[2];
			downedProvidence = flags2[3];
			downedSentinel1 = flags2[4];
			downedSentinel2 = flags2[5];
			downedSentinel3 = flags2[6];
			downedYharon = flags2[7];
			
			BitsByte flags3 = reader.ReadByte();
			downedSCal = flags3[0];
			downedBumble = flags3[1];
			downedCrabulon = flags3[2];
			downedBetsy = flags3[3];
			downedScavenger = flags3[4];
			downedWhar = flags3[5];
			downedSkullHead = flags3[6];
			downedUgly = flags3[7];
			
			BitsByte flags4 = reader.ReadByte();
			downedSkeletor = flags4[0];
			downedPlantThing = flags4[1];
			downedGolemBaby = flags4[2];
			downedMoonDude = flags4[3];
			downedBossAny = flags4[4];
			demonMode = flags4[5];
			onionMode = flags4[6];
			revenge = flags4[7];
			
			BitsByte flags5 = reader.ReadByte();
			downedStarGod = flags5[0];
			spawnAstralMeteor = flags5[1];
			spawnAstralMeteor2 = flags5[2];
			spawnAstralMeteor3 = flags5[3];
			spawnedHardBoss = flags5[4];
			downedPolterghast = flags[5];
		}
		
		public override void ResetNearbyTileEffects()
		{
			calamityTiles = 0;
			astralTiles = 0;
		}
		
		public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
		{
			calamityTiles = tileCounts[Mod.Find<ModTile>("CharredOre").Type] + tileCounts[Mod.Find<ModTile>("BrimstoneSlag").Type];
			astralTiles = tileCounts[Mod.Find<ModTile>("AstralOre").Type];
		}
		
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
		{
			int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
			if (ShiniesIndex != -1)
			{
				tasks.Insert(ShiniesIndex + 1, new PassLegacy("CalamityOres", (progress, somethingelse)=>
				{
					progress.Message = "Calamity Ores";
					int x = Main.maxTilesX;
					int y = Main.maxTilesY;
					int genLimit = x / 2;
					int generateBack = genLimit - 80; //Small = 2020
					int generateForward = genLimit + 80; //Small = 2180
					for (int k = 0; k < (int)((double)(x * y) * 15E-05); k++)
					{
						int tilesX = WorldGen.genRand.Next(0, generateBack);
						int tilesX2 = WorldGen.genRand.Next(generateForward, x);
						int tilesY = WorldGen.genRand.Next((int)(y * .3f), (int)(y * .5f));
						WorldGen.OreRunner(tilesX, tilesY, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("AerialiteOre").Type);
						WorldGen.OreRunner(tilesX2, tilesY, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("AerialiteOre").Type);
					}
					for (int k = 0; k < (int)((double)(x * y) * 15E-05); k++)
					{
						int tilesX = WorldGen.genRand.Next(0, generateBack);
						int tilesX2 = WorldGen.genRand.Next(generateForward, x);
						int tilesY = WorldGen.genRand.Next((int)(Main.maxTilesY * .35f), (int)(Main.maxTilesY * .55f));
						if (Main.tile[tilesX, tilesY].TileType == 147 || Main.tile[tilesX, tilesY].TileType == 161 || Main.tile[tilesX, tilesY].TileType == 163 || Main.tile[tilesX, tilesY].TileType == 164 || Main.tile[tilesX, tilesY].TileType == 200) 
						{
							WorldGen.OreRunner(tilesX, tilesY, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("CryonicOre").Type);
						}
						if (Main.tile[tilesX2, tilesY].TileType == 147 || Main.tile[tilesX2, tilesY].TileType == 161 || Main.tile[tilesX2, tilesY].TileType == 163 || Main.tile[tilesX2, tilesY].TileType == 164 || Main.tile[tilesX2, tilesY].TileType == 200) 
						{
							WorldGen.OreRunner(tilesX2, tilesY, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("CryonicOre").Type);
						}
					}
					for (int k = 0; k < (int)((double)(x * y) * 15E-05); k++)
					{
						int tilesX = WorldGen.genRand.Next(0, generateBack);
						int tilesX2 = WorldGen.genRand.Next(generateForward, x);
						int tilesY = WorldGen.genRand.Next((int)(Main.maxTilesY * .4f), (int)(Main.maxTilesY * .8f));
						if (Main.tile[tilesX, tilesY].TileType == 0 || Main.tile[tilesX, tilesY].TileType == 1) 
						{
							WorldGen.OreRunner(tilesX, tilesY, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("PerennialOre").Type);
						}
						if (Main.tile[tilesX2, tilesY].TileType == 0 || Main.tile[tilesX2, tilesY].TileType == 1) 
						{
							WorldGen.OreRunner(tilesX2, tilesY, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("PerennialOre").Type);
						}
					}
					for (int k = 0; k < (int)((double)(x * y) * 15E-05); k++)
					{
						int tilesX = WorldGen.genRand.Next(0, generateBack);
						int tilesX2 = WorldGen.genRand.Next(generateForward, x);
						int tilesY = WorldGen.genRand.Next((int)(y * .3f), (int)(y * .75f));
						if (Main.tile[tilesX, tilesY].TileType == 59) 
						{
							WorldGen.OreRunner(tilesX, tilesY, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("UelibloomOre").Type);
						}
						if (Main.tile[tilesX2, tilesY].TileType == 59) 
						{
							WorldGen.OreRunner(tilesX2, tilesY, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("UelibloomOre").Type);
						}
					}
				}));
			}
			int HellIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Underworld"));
			if (HellIndex != -1)
			{
				tasks.Insert(HellIndex + 1, new PassLegacy("CalamityOres2", (progress, somethingelse) =>
				{
					progress.Message = "Calamity Ores 2";
					int x = Main.maxTilesX;
					int y = Main.maxTilesY;
					int genLimit = x / 2;
					int generateBack = genLimit - 80; //Small = 2020
					int generateForward = genLimit + 80; //Small = 2180
					for (int k = 0; k < (int)((double)(x * y) * 15E-05); k++)
					{
						int tilesX = WorldGen.genRand.Next(0, generateBack);
						int tilesX2 = WorldGen.genRand.Next(generateForward, x);
						int tilesY = WorldGen.genRand.Next(y - 200, y);
						if (Main.tile[tilesX, tilesY].TileType == 57) 
						{
							WorldGen.TileRunner(tilesX, tilesY, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("ChaoticOre").Type);
						}
						if (Main.tile[tilesX2, tilesY].TileType == 57) 
						{
							WorldGen.TileRunner(tilesX2, tilesY, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("ChaoticOre").Type);
						}
					}
				}));
			}
			int CragIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
			if (CragIndex != -1) 
			{
				tasks.Insert(CragIndex + 1, new PassLegacy("HellCrag", (progress, somethingelse) =>
				{
					progress.Message = "Hell Crag";
					int SpaceOutX = Main.rand.Next(130, 155);
					int SpaceOutY = Main.maxTilesY - Main.rand.Next(30, 40);
					WorldMethods.RoundHole(SpaceOutX, SpaceOutY, 100, 125, 55, true);
					WorldGen.digTunnel(SpaceOutX, SpaceOutY, 0, 0, 55, 55, false);
					for (int rotation2 = 0; rotation2 < 350; rotation2++)
					{
						int DistX = (int)(0 - (Math.Sin(rotation2)* 100));
						int DistY = (int)(0 - (Math.Cos(rotation2)* 125));
					    WorldGen.digTunnel(SpaceOutX + DistX, SpaceOutY + DistY, 0, 0, 55, 55, false);
					}
					for (int J = 20; J < (SpaceOutX * 2) + 10; J++) 
					{
						WorldMethods.TileRunner(J, Main.maxTilesY - 108, (double)Main.rand.Next(12, 15), 1, Mod.Find<ModTile>("BrimstoneSlag").Type, true, 0f, 0f, false, true);
						WorldMethods.TileRunner(J, Main.maxTilesY - 54, (double)102, 1, Mod.Find<ModTile>("BrimstoneSlag").Type, true, 0f, 0f, false, true);
						WorldMethods.TileRunner(J + 10, Main.maxTilesY - 27, (double)75, 1, Mod.Find<ModTile>("BrimstoneSlag").Type, true, 0f, 0f, false, true);
						WorldMethods.TileRunner(J + 10, Main.maxTilesY - 78, (double)75, 1, Mod.Find<ModTile>("BrimstoneSlag").Type, true, 0f, 0f, false, true);
						if (J > 30) 
						{
							if (Main.rand.NextBool(12)) 
							{
								WorldMethods.CragSpike(J, Main.maxTilesY - Main.rand.Next(125, 158), 1, Main.rand.Next(85, 114), (ushort)Mod.Find<ModTile>("BrimstoneSlag").Type, (float)(Main.rand.Next(5, 12)), (float)(Main.rand.Next(5, 12)));
							}
							if (Main.rand.NextBool(40)) 
							{
								WorldMethods.CragSpike(J, Main.maxTilesY - Main.rand.Next(158, 204), 1, Main.rand.Next(124, 154), (ushort)Mod.Find<ModTile>("BrimstoneSlag").Type, (float)(Main.rand.Next(6, 13)), (float)(Main.rand.Next(6, 13)));
							}
						}
					}
					int Position = Main.rand.Next(183, 234);
					WorldMethods.CragSpike(SpaceOutX, Main.maxTilesY - Position, 1, Main.rand.Next(145, 167), (ushort)Mod.Find<ModTile>("BrimstoneSlag").Type, (float)4, (float)4);
					WorldGen.digTunnel(SpaceOutX + 3, (Main.maxTilesY - Position) + 30, 0, 0, 6, 6, false);
					WorldGen.digTunnel(SpaceOutX - 3, (Main.maxTilesY - Position) + 30, 0, 0, 6, 6, false);
					for (int TunnelPlace = (Main.maxTilesY - Position) + 30; TunnelPlace < Main.maxTilesY - 95; TunnelPlace++) 
					{
						WorldGen.digTunnel(SpaceOutX, TunnelPlace, 0, 0, 3, 3, false);
					}
					WorldMethods.RoundHole(SpaceOutX, (Main.maxTilesY - 72), 80, 27, 4, false);
					for (int rotation3 = 0; rotation3 < 350; rotation3++)
					{
						int DistX = (int)(0 - (Math.Sin(rotation3)* 80));
						int DistY = (int)(0 - (Math.Cos(rotation3)* 27));
					    WorldGen.digTunnel(SpaceOutX + DistX, (Main.maxTilesY - 72) + DistY, 0, 0, 4, 4, false);
					}
					WorldGen.digTunnel(SpaceOutX, Main.maxTilesY - 72, 0, 0, 5, 5, false);
					WorldMethods.RoundHole(SpaceOutX, (Main.maxTilesY - 72), 12, 6, 4, false);
					for (int OreGen = 0; OreGen < 150; OreGen++) 
					{
						int why = (Main.maxTilesY - 108) + Main.rand.Next(-102, 75);
						int ex = SpaceOutX + Main.rand.Next(-102, 155);
						if (Main.tile[ex, why].TileType == Mod.Find<ModTile>("BrimstoneSlag").Type) 
						{
							WorldGen.TileRunner(ex, why, (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), Mod.Find<ModTile>("CharredOre").Type, false, 0f, 0f, false, true);
						}
					}
					for (int rotation3 = 0; rotation3 < 350; rotation3++)
					{
						int DistX = (int)(0 - (Math.Sin(rotation3)* 12));
						int DistY = (int)(0 - (Math.Cos(rotation3)* 6));
					    WorldGen.digTunnel(SpaceOutX + DistX, (Main.maxTilesY - 72) + DistY, 0, 0, 4, 4, false);
					}
				}));
			}
		}
		
		public static void checkSpring()
		{
			DateTime now = DateTime.Now;
			int day = now.Day;
			int month = now.Month;
			if (month >= 3 && month <= 6)
			{
				if (month == 3 && day >= 20)
				{
					spring = true;
					return;
				}
				if (month == 4)
				{
					spring = true;
					return;
				}
				if (month == 5)
				{
					spring = true;
					return;
				}
				if (month == 6 && day <= 20)
				{
					spring = true;
					return;
				}
			}
			spring = false;
		}
    	
    	public static void checkSummer()
		{
			DateTime now = DateTime.Now;
			int day = now.Day;
			int month = now.Month;
			if (month >= 6 && month <= 9)
			{
				if (month == 6 && day >= 21)
				{
					summer = true;
					return;
				}
				if (month == 7)
				{
					summer = true;
					return;
				}
				if (month == 8)
				{
					summer = true;
					return;
				}
				if (month == 9 && day <= 21)
				{
					summer = true;
					return;
				}
			}
			summer = false;
		}
    	
    	public static void checkFall()
		{
			DateTime now = DateTime.Now;
			int day = now.Day;
			int month = now.Month;
			if (month >= 9 && month <= 12)
			{
				if (month == 9 && day >= 22)
				{
					fall = true;
					return;
				}
				if (month == 10)
				{
					fall = true;
					return;
				}
				if (month == 11)
				{
					fall = true;
					return;
				}
				if (month == 12 && day <= 20)
				{
					fall = true;
					return;
				}
			}
			fall = false;
		}
    	
    	public static void checkWinter()
		{
			DateTime now = DateTime.Now;
			int day = now.Day;
			int month = now.Month;
			if (month == 12 || (month >= 1 && month <= 3))
			{
				if (month == 12 && day >= 21)
				{
					winter = true;
					return;
				}
				if (month == 1)
				{
					winter = true;
					return;
				}
				if (month == 2)
				{
					winter = true;
					return;
				}
				if (month == 3 && day <= 19)
				{
					winter = true;
					return;
				}
			}
			winter = false;
		}
		
		public static void dropAstralMeteor()
		{
			Mod mod = ModLoader.GetMod("CalamityModClassic1Point2");
			bool flag = true;
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				return;
			}
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					flag = false;
					break;
				}
			}
			int num = 0;
			float num2 = (float)(Main.maxTilesX / 4200);
			int num3 = (int)(400f * num2);
			for (int j = 5; j < Main.maxTilesX - 5; j++)
			{
				int num4 = 5;
				while ((double)num4 < Main.worldSurface)
				{
					if (Main.tile[j, num4].HasTile && Main.tile[j, num4].TileType == mod.Find<ModTile>("AstralOre").Type)
					{
						num++;
						if (num > num3)
						{
							return;
						}
					}
					num4++;
				}
			}
			float num5 = 600f;
			while (!flag)
			{
				float num6 = (float)Main.maxTilesX * 0.08f;
				int num7 = Main.rand.Next(150, Main.maxTilesX - 150);
				while ((float)num7 > (float)Main.spawnTileX - num6 && (float)num7 < (float)Main.spawnTileX + num6)
				{
					num7 = Main.rand.Next(150, Main.maxTilesX - 150);
				}
				int k = (int)(Main.worldSurface * 0.3);
				while (k < Main.maxTilesY)
				{
					if (Main.tile[num7, k].HasTile && Main.tileSolid[(int)Main.tile[num7, k].TileType])
					{
						int num8 = 0;
						int num9 = 15;
						for (int l = num7 - num9; l < num7 + num9; l++)
						{
							for (int m = k - num9; m < k + num9; m++)
							{
								if (WorldGen.SolidTile(l, m))
								{
									num8++;
									if (Main.tile[l, m].TileType == 189 || Main.tile[l, m].TileType == 202)
									{
										num8 -= 100;
									}
								}
								else if (Main.tile[l, m].LiquidAmount > 0)
								{
									num8--;
								}
							}
						}
						if ((float)num8 < num5)
						{
							num5 -= 0.5f;
							break;
						}
						flag = CalamityWorld.astralMeteor(num7, k);
						if (flag)
						{
							break;
						}
						break;
					}
					else
					{
						k++;
					}
				}
				if (num5 < 100f)
				{
					return;
				}
			}
		}
		
		public static bool astralMeteor(int i, int j)
		{
			Mod mod = ModLoader.GetMod("CalamityModClassic1Point2");
			if (i < 50 || i > Main.maxTilesX - 50)
			{
				return false;
			}
			if (j < 50 || j > Main.maxTilesY - 50)
			{
				return false;
			}
			int num = 35;
			Rectangle rectangle = new Rectangle((i - num) * 16, (j - num) * 16, num * 2 * 16, num * 2 * 16);
			for (int k = 0; k < 255; k++)
			{
				if (Main.player[k].active)
				{
					Rectangle value = new Rectangle((int)(Main.player[k].position.X + (float)(Main.player[k].width / 2) - (float)(NPC.sWidth / 2) - (float)NPC.safeRangeX), (int)(Main.player[k].position.Y + (float)(Main.player[k].height / 2) - (float)(NPC.sHeight / 2) - (float)NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
					if (rectangle.Intersects(value))
					{
						return false;
					}
				}
			}
			for (int l = 0; l < 200; l++)
			{
				if (Main.npc[l].active)
				{
					Rectangle value2 = new Rectangle((int)Main.npc[l].position.X, (int)Main.npc[l].position.Y, Main.npc[l].width, Main.npc[l].height);
					if (rectangle.Intersects(value2))
					{
						return false;
					}
				}
			}
			for (int m = i - num; m < i + num; m++)
			{
				for (int n = j - num; n < j + num; n++)
				{
					if (Main.tile[m, n].HasTile && Main.tile[m, n].TileType == 21)
					{
						return false;
					}
				}
			}
			num = WorldGen.genRand.Next(17, 23);
			for (int num2 = i - num; num2 < i + num; num2++)
			{
				for (int num3 = j - num; num3 < j + num; num3++)
				{
					if (num3 > j + Main.rand.Next(-2, 3) - 5)
					{
						float num4 = (float)Math.Abs(i - num2);
						float num5 = (float)Math.Abs(j - num3);
						float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
						if ((double)num6 < (double)num * 0.9 + (double)Main.rand.Next(-4, 5))
						{
							if (!Main.tileSolid[(int)Main.tile[num2, num3].TileType])
                            {
                                WorldGen.KillTile(num2, num3);
                            }
							Main.tile[num2, num3].TileType = (ushort)mod.Find<ModTile>("AstralOre").Type;
						}
					}
				}
			}
			num = WorldGen.genRand.Next(8, 14);
			for (int num7 = i - num; num7 < i + num; num7++)
			{
				for (int num8 = j - num; num8 < j + num; num8++)
				{
					if (num8 > j + Main.rand.Next(-2, 3) - 4)
					{
						float num9 = (float)Math.Abs(i - num7);
						float num10 = (float)Math.Abs(j - num8);
						float num11 = (float)Math.Sqrt((double)(num9 * num9 + num10 * num10));
						if ((double)num11 < (double)num * 0.8 + (double)Main.rand.Next(-3, 4))
						{
							WorldGen.KillTile(num7, num8);
						}
					}
				}
			}
			num = WorldGen.genRand.Next(25, 35);
			for (int num12 = i - num; num12 < i + num; num12++)
			{
				for (int num13 = j - num; num13 < j + num; num13++)
				{
					float num14 = (float)Math.Abs(i - num12);
					float num15 = (float)Math.Abs(j - num13);
					float num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
					if ((double)num16 < (double)num * 0.7)
					{
						if (Main.tile[num12, num13].TileType == 5 || Main.tile[num12, num13].TileType == 32 || Main.tile[num12, num13].TileType == 352)
						{
							WorldGen.KillTile(num12, num13, false, false, false);
						}
						Main.tile[num12, num13].LiquidAmount = 0;
					}
					if (Main.tile[num12, num13].TileType == (ushort)mod.Find<ModTile>("AstralOre").Type)
					{
						if (!WorldGen.SolidTile(num12 - 1, num13) && !WorldGen.SolidTile(num12 + 1, num13) && !WorldGen.SolidTile(num12, num13 - 1) && !WorldGen.SolidTile(num12, num13 + 1))
                        {
                            WorldGen.KillTile(num12, num13);
                        }
						else if ((Main.tile[num12, num13].IsHalfBlock || Main.tile[num12 - 1, num13].TopSlope) && !WorldGen.SolidTile(num12, num13 + 1))
                        {
                            WorldGen.KillTile(num12, num13);
                        }
					}
					WorldGen.SquareTileFrame(num12, num13, true);
					WorldGen.SquareWallFrame(num12, num13, true);
				}
			}
			num = WorldGen.genRand.Next(23, 32);
			for (int num17 = i - num; num17 < i + num; num17++)
			{
				for (int num18 = j - num; num18 < j + num; num18++)
				{
					if (num18 > j + WorldGen.genRand.Next(-3, 4) - 3 && Main.tile[num17, num18].HasTile && Main.rand.NextBool(10))
					{
						float num19 = (float)Math.Abs(i - num17);
						float num20 = (float)Math.Abs(j - num18);
						float num21 = (float)Math.Sqrt((double)(num19 * num19 + num20 * num20));
						if ((double)num21 < (double)num * 0.8)
						{
							if (Main.tile[num17, num18].TileType == 5 || Main.tile[num17, num18].TileType == 32 || Main.tile[num17, num18].TileType == 352)
							{
								WorldGen.KillTile(num17, num18, false, false, false);
							}
							Main.tile[num17, num18].TileType = (ushort)mod.Find<ModTile>("AstralOre").Type;
							WorldGen.SquareTileFrame(num17, num18, true);
						}
					}
				}
			}
			num = WorldGen.genRand.Next(30, 38);
			for (int num22 = i - num; num22 < i + num; num22++)
			{
				for (int num23 = j - num; num23 < j + num; num23++)
				{
					if (num23 > j + WorldGen.genRand.Next(-2, 3) && Main.tile[num22, num23].HasTile && Main.rand.NextBool(20))
					{
						float num24 = (float)Math.Abs(i - num22);
						float num25 = (float)Math.Abs(j - num23);
						float num26 = (float)Math.Sqrt((double)(num24 * num24 + num25 * num25));
						if ((double)num26 < (double)num * 0.85)
						{
							if (Main.tile[num22, num23].TileType == 5 || Main.tile[num22, num23].TileType == 32 || Main.tile[num22, num23].TileType == 352)
							{
								WorldGen.KillTile(num22, num23, false, false, false);
							}
							Main.tile[num22, num23].TileType = (ushort)mod.Find<ModTile>("AstralOre").Type;
							WorldGen.SquareTileFrame(num22, num23, true);
						}
					}
				}
			}
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				NetMessage.SendTileSquare(-1, i, j, 40, TileChangeType.None);
			}
			return true;
		}
		
		public override void PostUpdateWorld()
		{
			if (Main.player[Main.myPlayer].GetModPlayer<CalamityPlayer>().stress > 6000 && NPC.MoonLordCountdown == 0)
			{
				float stress = MathHelper.Clamp((float)Math.Sin((double)((float)(Main.player[Main.myPlayer].GetModPlayer<CalamityPlayer>().stress - 6000) / 60f * 0.5f)) * 1.75f, 0f, 0.875f);
				stress *= 0.75f * ((float)(Main.player[Main.myPlayer].GetModPlayer<CalamityPlayer>().stress - 6000) / 4000f);
				if (!Filters.Scene["MoonLordShake"].IsActive())
				{
					Filters.Scene.Activate("MoonLordShake", Main.player[Main.myPlayer].position, new object[0]);
				}
				Filters.Scene["MoonLordShake"].GetShader().UseIntensity(stress);
			}
			else if (Filters.Scene["MoonLordShake"].IsActive() && NPC.MoonLordCountdown == 0)
			{
				Filters.Scene.Deactivate("MoonLordShake", new object[0]);
			}
			if (Main.netMode != NetmodeID.Server)
			{
				if (Main.time == 0.0)
				{
					runCheck = 0;
				}
				if (runCheck == 0)
				{
					checkSpring();
					checkSummer();
					checkFall();
					checkWinter();
					runCheck++;
				}
			}
			for (int playerIndex = 0; playerIndex < 255; playerIndex++)
			{
				if (Main.player[playerIndex].active && Main.player[playerIndex].GetModPlayer<CalamityPlayer>().bloodflareSet)
				{
					if (Main.time == 1.0)
					{
						if (!Main.dayTime && !Main.bloodMoon && Main.rand.NextBool(3))
						{
							if (!Main.IsFastForwardingTime()/* tModPorter Note: Removed. Suggestion: IsFastForwardingTime(), fastForwardTimeToDawn or fastForwardTimeToDusk */)
							{
								if (Main.netMode != NetmodeID.MultiplayerClient)
								{
									Main.bloodMoon = true;
									string key = "The Blood Moon is rising...";
									Color messageColor = Color.Crimson;
									if (Main.netMode == NetmodeID.SinglePlayer)
									{
										Main.NewText((key), messageColor);
									}
									else if (Main.netMode == NetmodeID.Server)
									{
										ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
									}
								}
							}
						}
					}
				}
				if (Main.netMode == NetmodeID.SinglePlayer)
				{
					break;
				}
			}
		}

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            Mod mod = ModLoader.GetMod("CalamityModClassic1Point2");
            UIHandler.ModifyInterfaceLayers(mod, layers);
        }

        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + (" Silt"), new int[]
            {
                424,
                1103
            });
            RecipeGroup.RegisterGroup("SiltGroup", group);
            group = new RecipeGroup(() => Lang.misc[37] + (" Lunar Pickaxe"), new int[]
            {
                2776,
                2781,
                2786,
                3466
            });
            RecipeGroup.RegisterGroup("LunarPickaxe", group);
            group = new RecipeGroup(() => Lang.misc[37] + (" Lunar Axe"), new int[]
            {
                3522,
                3523,
                3524,
                3525
            });
            RecipeGroup.RegisterGroup("LunarAxe", group);
            group = new RecipeGroup(() => Lang.misc[37] + (" Wings"), new int[]
            {
                492,
                493,
                665,
                749,
                761,
                785,
                786,
                821,
                822,
                823,
                948,
                1162,
                1165,
                1515,
                1583,
                1584,
                1585,
                1586,
                1797,
                1830,
                1871,
                2280,
                2494,
                2609,
                2770,
                3468,
                3469,
                3470,
                3471,
                3580,
                3582,
                3588,
                3592,
                3883,
ModContent.ItemType<SkylineWings>(),
ModContent.ItemType<StarlightWings>(),
ModContent.ItemType<AureateWings>(),
ModContent.ItemType<DiscordianWings>(),
ModContent.ItemType<TarragonWings>(),
ModContent.ItemType<XerocWings>()
            });
            RecipeGroup.RegisterGroup("WingsGroup", group);
        }

        public override void AddRecipes()/* tModPorter Note: Removed. Use ModSystem.AddRecipes */
        {
            Recipe recipe = Recipe.Create(ItemID.SkyMill);
            recipe.AddIngredient(ItemID.SunplateBlock, 10);
            recipe.AddIngredient(ItemID.Cloud, 5);
            recipe.AddIngredient(ItemID.RainCloud, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = Recipe.Create(ItemID.IceSkates);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddIngredient(ItemID.Leather, 5);
            recipe.AddIngredient(ItemID.IronBar, 5);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
            recipe = Recipe.Create(ItemID.IceSkates);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddIngredient(ItemID.Leather, 5);
            recipe.AddIngredient(ItemID.LeadBar, 5);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
            recipe = Recipe.Create(ItemID.BlackLens);
            recipe.AddIngredient(ItemID.Lens);
            recipe.AddIngredient(ItemID.BlackDye);
            recipe.AddTile(TileID.DyeVat);
            recipe.Register();
            recipe = Recipe.Create(ItemID.Leather);
            recipe.AddIngredient(ItemID.Vertebrae, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            recipe = Recipe.Create(ItemID.IceMachine);
            recipe.AddIngredient(ItemID.IceBlock, 25);
            recipe.AddIngredient(ItemID.SnowBlock, 15);
            recipe.AddIngredient(ItemID.IronBar, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = Recipe.Create(ItemID.IceMachine);
            recipe.AddIngredient(ItemID.IceBlock, 25);
            recipe.AddIngredient(ItemID.SnowBlock, 15);
            recipe.AddIngredient(ItemID.LeadBar, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = Recipe.Create(ItemID.Starfury);
            recipe.AddIngredient(ItemID.GoldBroadsword);
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient(null, "VictoryShard", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = Recipe.Create(ItemID.Starfury);
            recipe.AddIngredient(ItemID.PlatinumBroadsword);
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient(null, "VictoryShard", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = Recipe.Create(ItemID.CloudinaBottle);
            recipe.AddIngredient(ItemID.Feather, 2);
            recipe.AddIngredient(ItemID.Bottle);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = Recipe.Create(ItemID.BlizzardinaBottle);
            recipe.AddIngredient(ItemID.Feather, 4);
            recipe.AddIngredient(ItemID.Bottle);
            recipe.AddIngredient(ItemID.SnowBlock, 50);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = Recipe.Create(ItemID.SandstorminaBottle);
            recipe.AddIngredient(null, "DesertFeather", 10);
            recipe.AddIngredient(ItemID.Feather, 6);
            recipe.AddIngredient(ItemID.Bottle);
            recipe.AddIngredient(ItemID.SandBlock, 70);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = Recipe.Create(ItemID.ShinyRedBalloon);
            recipe.AddIngredient(ItemID.WhiteString);
            recipe.AddIngredient(ItemID.Gel, 80);
            recipe.AddIngredient(ItemID.Cloud, 40);
            recipe.AddTile(TileID.Solidifier);
            recipe.Register();
            recipe = Recipe.Create(ItemID.WaterWalkingBoots);
            recipe.AddIngredient(ItemID.Leather, 5);
            recipe.AddIngredient(ItemID.WaterWalkingPotion, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = Recipe.Create(ItemID.LavaCharm);
            recipe.AddIngredient(ItemID.LavaBucket, 5);
            recipe.AddIngredient(ItemID.Obsidian, 25);
            recipe.AddIngredient(ItemID.IronBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = Recipe.Create(ItemID.LavaCharm);
            recipe.AddIngredient(ItemID.LavaBucket, 5);
            recipe.AddIngredient(ItemID.Obsidian, 25);
            recipe.AddIngredient(ItemID.LeadBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = Recipe.Create(ItemID.FrostHelmet);
            recipe.AddIngredient(null, "CryoBar", 6);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
            recipe = Recipe.Create(ItemID.FrostBreastplate);
            recipe.AddIngredient(null, "CryoBar", 10);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
            recipe = Recipe.Create(ItemID.FrostLeggings);
            recipe.AddIngredient(null, "CryoBar", 8);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
            recipe = Recipe.Create(ItemID.CobaltShield);
            recipe.AddIngredient(ItemID.CobaltBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = Recipe.Create(ItemID.CobaltShield);
            recipe.AddIngredient(ItemID.PalladiumBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = Recipe.Create(ItemID.Muramasa);
            recipe.AddIngredient(ItemID.CobaltBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = Recipe.Create(ItemID.Muramasa);
            recipe.AddIngredient(ItemID.PalladiumBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = Recipe.Create(ItemID.TerraBlade);
            recipe.AddIngredient(null, "TrueBloodyEdge");
            recipe.AddIngredient(ItemID.TrueExcalibur);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = Recipe.Create(ItemID.Ectoplasm);
            recipe.AddIngredient(null, "Ectoblood", 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = Recipe.Create(ItemID.RocketI, 5);
            recipe.AddIngredient(ItemID.EmptyBullet);
            recipe.AddIngredient(ItemID.ExplosivePowder, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = Recipe.Create(ItemID.EnchantedSword);
            recipe.AddIngredient(null, "VictoryShard", 10);
            recipe.AddIngredient(ItemID.SoulofLight, 15);
            recipe.AddIngredient(ItemID.UnicornHorn, 3);
            recipe.AddIngredient(ItemID.LightShard);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = Recipe.Create(ItemID.WormholePotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.WarmthPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.CratePotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.SonarPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.FishingPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.TeleportationPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.StinkPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.LovePotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.WrathPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.InfernoPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.RagePotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.EndurancePotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.LifeforcePotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.AmmoReservationPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.TrapsightPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.SummoningPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.FlipperPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.TitanPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.BuilderPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.CalmingPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.HeartreachPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.MiningPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.GenderChangePotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.GravitationPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.HunterPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.ArcheryPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.WaterWalkingPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.ThornsPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.BattlePotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.NightOwlPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.ShinePotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.InvisibilityPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.SpelunkerPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.FeatherfallPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.MagicPowerPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.ManaRegenerationPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.IronskinPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.GillsPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.SwiftnessPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.RegenerationPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.ObsidianSkinPotion);
            recipe.AddIngredient(null, "BloodOrb", 10);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = Recipe.Create(ItemID.FlyingCarpet);
            recipe.AddIngredient(ItemID.AncientCloth, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = Recipe.Create(ItemID.LihzahrdPowerCell);
            recipe.AddIngredient(ItemID.LihzahrdBrick, 15);
            recipe.AddIngredient(null, "CoreofCinder");
            recipe.AddTile(TileID.LihzahrdFurnace);
            recipe.Register();
            recipe = Recipe.Create(ItemID.TruffleWorm);
            recipe.AddIngredient(ItemID.GlowingMushroom, 15);
            recipe.AddIngredient(ItemID.Worm);
            recipe.AddTile(TileID.Autohammer);
            recipe.Register();
        }
    }
}
