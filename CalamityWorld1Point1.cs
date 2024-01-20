using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using CalamityModClassic1Point1.Tiles;
using CalamityModClassic1Point1.NPCs;
using CalamityModClassic1Point1.NPCs.DesertScourge;
using CalamityModClassic1Point1.NPCs.HiveMind;
using CalamityModClassic1Point1.NPCs.Perforator;
using CalamityModClassic1Point1.NPCs.SlimeGod;
using CalamityModClassic1Point1.NPCs.Cryogen;
using CalamityModClassic1Point1.NPCs.Calamitas;
using CalamityModClassic1Point1.NPCs.Leviathan;
using CalamityModClassic1Point1.NPCs.TheDevourerofGods;
using CalamityModClassic1Point1.NPCs.PlaguebringerGoliath;
using System.Linq;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace CalamityModClassic1Point1
{
	public class CalamityWorld1Point1 : ModSystem
	{
		private const int saveVersion = 0;
		public static int calamityTiles = 0;
		public static bool stopAerialite = false;
		public static bool stopCryonic = false;
		public static bool stopPerennial = false;
		public static bool stopChaotic = false;
		public static bool stopUelibloom = false;
		public static bool downedDesertScourge = false;
		public static bool downedHiveMind = false;
		public static bool downedPerforator = false;
		public static bool downedSlimeGod = false;
		public static bool downedCryogen = false;
		public static bool downedCalamitas = false;
		public static bool downedLeviathan = false;
		public static bool downedDoG = false;
		public static bool downedPlaguebringer = false;
		public static bool downedYharon = false;
		public static bool downedProvidence = false;
		public static bool downedGuardians = false;
		public static bool downedSentinel = false;
		public static bool downedSCal = false;

		public override void OnWorldLoad()
		{
			stopAerialite = false;
			stopCryonic = false;
			stopPerennial = false;
			stopChaotic = false;
			stopUelibloom = false;
			downedDesertScourge = false;
			downedHiveMind = false;
			downedPerforator = false;
			downedSlimeGod = false;
			downedCryogen = false;
			downedCalamitas = false;
			downedLeviathan = false;
			downedDoG = false;
			downedPlaguebringer = false;
			downedGuardians = false;
			downedProvidence = false;
			downedSentinel = false;
			downedYharon = false;
			downedSCal = false;
        }
        public override void OnWorldUnload()
        {
            stopAerialite = false;
            stopCryonic = false;
            stopPerennial = false;
            stopChaotic = false;
            stopUelibloom = false;
            downedDesertScourge = false;
            downedHiveMind = false;
            downedPerforator = false;
            downedSlimeGod = false;
            downedCryogen = false;
            downedCalamitas = false;
            downedLeviathan = false;
            downedDoG = false;
            downedPlaguebringer = false;
            downedGuardians = false;
            downedProvidence = false;
            downedSentinel = false;
            downedYharon = false;
            downedSCal = false;
        }

        public override void SaveWorldData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
		{
			if (downedDesertScourge) tag["desertScourge"] = true;
			if (downedHiveMind) tag["hiveMind"] = true;
			if (downedPerforator) tag["perforator"] = true;
			if (downedSlimeGod) tag["slimeGod"] = true;
			if (downedCryogen) tag["cryogen"] = true;
			if (downedCalamitas) tag["calamitas"] = true;
			if (downedLeviathan) tag["leviathan"] = true;
			if (downedDoG) tag["devourerOfGods"] = true;
			if (downedPlaguebringer) tag["plaguebringerGoliath"] = true;
			if (downedGuardians) tag["guardians"] = true;
			if (downedProvidence) tag["providence"] = true;
			if (downedSentinel) tag["sentinel"] = true;
			if (downedYharon) tag["yharon"] = true;
			if (downedSCal) tag["supremecal"] = true;
		}

		public override void LoadWorldData(TagCompound tag)
		{
			downedDesertScourge = tag.ContainsKey("desertScourge");
			downedHiveMind = tag.ContainsKey("hiveMind");
			downedPerforator = tag.ContainsKey("perforator");
			downedSlimeGod = tag.ContainsKey("slimeGod");
			downedCryogen = tag.ContainsKey("cryogen");
			downedCalamitas = tag.ContainsKey("calamitas");
			downedLeviathan = tag.ContainsKey("leviathan");
			downedDoG = tag.ContainsKey("devourerOfGods");
			downedPlaguebringer = tag.ContainsKey("plaguebringerGoliath");
			downedGuardians = tag.ContainsKey("guardians");
			downedProvidence = tag.ContainsKey("providence");
			downedSentinel = tag.ContainsKey("sentinel");
			downedYharon = tag.ContainsKey("yharon");
			downedSCal = tag.ContainsKey("supremecal");
		}
		
		public override void NetSend(BinaryWriter writer)
		{
			BitsByte flags = new BitsByte();
			flags[0] = downedDesertScourge;
			flags[1] = downedHiveMind;
			flags[2] = downedPerforator;
			flags[3] = downedSlimeGod;
			flags[4] = downedCryogen;
			flags[5] = downedCalamitas;
			flags[6] = downedLeviathan;
			flags[7] = downedDoG;
			flags[8] = downedPlaguebringer;
			flags[9] = downedGuardians;
			flags[10] = downedProvidence;
			flags[11] = downedSentinel;
			flags[12] = downedYharon;
			flags[13] = downedSCal;
			flags[14] = stopUelibloom;
			flags[15] = stopChaotic;
			flags[16] = stopPerennial;
			flags[17] = stopCryonic;
			flags[18] = stopAerialite;
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			downedDesertScourge = flags[0];
			downedHiveMind = flags[1];
			downedPerforator = flags[2];
			downedSlimeGod = flags[3];
			downedCryogen = flags[4];
			downedCalamitas = flags[5];
			downedLeviathan = flags[6];
			downedDoG = flags[7];
			downedPlaguebringer = flags[8];
			downedGuardians = flags[9];
			downedProvidence = flags[10];
			downedSentinel = flags[11];
			downedYharon = flags[12];
			downedSCal = flags[13];
			stopUelibloom = flags[14];
			stopChaotic = flags[15];
			stopPerennial = flags[16];
			stopCryonic = flags[17];
			stopAerialite = flags[18];
		}
		
		public override void ResetNearbyTileEffects()
		{
			CalamityPlayer1Point1 modPlayer = Main.player[Main.myPlayer].GetModPlayer<CalamityPlayer1Point1>();
			calamityTiles = 0;
		}
		
		public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
		{
			calamityTiles = tileCounts[Mod.Find<ModTile>("BrimstoneCragRock").Type] + tileCounts[ModContent.TileType<BrimstoneSlag>()];
		}
		
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) //comment out this method if not done before release of new version!
		{
			int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
			if (ShiniesIndex == -1) 
			{
				return;
			}
			tasks.Insert(ShiniesIndex + 1, new PassLegacy("HellCrag", (progress, config) =>
			{
				progress.Message = "Generating Profaned Crags";
				
				//clearing space
				int SpaceOutX = Main.rand.Next(130, 155);
				int SpaceOutY = Main.maxTilesY - Main.rand.Next(30, 40);
                double prog = WorldMethods1Point1.RoundHole(SpaceOutX, SpaceOutY, 100, 125, 55, true, ref progress);
				
				WorldGen.digTunnel(SpaceOutX, SpaceOutY, 0, 0, 55, 55, false);
				for (int rotation2 = 0; rotation2 < 350; rotation2++)
				    {
				    int DistX = (int)(0 - (Math.Sin(rotation2)* 100));
				    int DistY = (int)(0 - (Math.Cos(rotation2)* 125));
			        WorldGen.digTunnel(SpaceOutX + DistX, SpaceOutY + DistY, 0, 0, 55, 55, false);
                    progress.Set(prog + 350 / 612500 * rotation2);
                    if (rotation2 == 349)
                    {
                        prog += 350 / 612500 * rotation2 * 2;
                    }
                }
			
				//Generating random spikes layer 1-
				for (int J = 20; J < (SpaceOutX * 2) + 10; J++) 
				{
					WorldMethods1Point1.TileRunner(J, Main.maxTilesY - 108, (double)Main.rand.Next(12, 15), 1, ModContent.TileType<BrimstoneSlag>(), true, 0f, 0f, false, true);
					WorldMethods1Point1.TileRunner(J, Main.maxTilesY - 54, (double)102, 1, ModContent.TileType<BrimstoneSlag>(), true, 0f, 0f, false, true);
					WorldMethods1Point1.TileRunner(J + 10, Main.maxTilesY - 27, (double)75, 1, ModContent.TileType<BrimstoneSlag>(), true, 0f, 0f, false, true);
					WorldMethods1Point1.TileRunner(J + 10, Main.maxTilesY - 78, (double)75, 1, ModContent.TileType<BrimstoneSlag>(), true, 0f, 0f, false, true);
					if (J > 30) 
					{
						if (Main.rand.Next(12) == 1) 
						{
							WorldMethods1Point1.CragSpike(J, Main.maxTilesY - Main.rand.Next(125, 158), 1, Main.rand.Next(85, 114), (ushort)ModContent.TileType<BrimstoneSlag>(), (float)(Main.rand.Next(5, 12)), (float)(Main.rand.Next(5, 12)));
						}
						if (Main.rand.Next(40) == 1) 
						{
							WorldMethods1Point1.CragSpike(J, Main.maxTilesY - Main.rand.Next(158, 204), 1, Main.rand.Next(124, 154), (ushort)ModContent.TileType<BrimstoneSlag>(), (float)(Main.rand.Next(6, 13)), (float)(Main.rand.Next(6, 13)));
						}
                    }
                    progress.Set(prog + 0.001f * 2);
                    prog += 0.002f;
                }
                progress.Set(prog);
                int Position = Main.rand.Next(183, 234);
				WorldMethods1Point1.CragSpike(SpaceOutX, Main.maxTilesY - Position, 1, Main.rand.Next(145, 167), (ushort)ModContent.TileType<BrimstoneSlag>(), (float)4, (float)4);
                prog += 0.1f;
                progress.Set(prog);
                WorldGen.digTunnel(SpaceOutX + 3, (Main.maxTilesY - Position) + 30, 0, 0, 6, 6, false);
				WorldGen.digTunnel(SpaceOutX - 3, (Main.maxTilesY - Position) + 30, 0, 0, 6, 6, false);
                prog += 0.1f;
                progress.Set(prog);
                for (int TunnelPlace = (Main.maxTilesY - Position) + 30; TunnelPlace < Main.maxTilesY - 95; TunnelPlace++) 
				{
					WorldGen.digTunnel(SpaceOutX, TunnelPlace, 0, 0, 3, 3, false);
				}

                prog = WorldMethods1Point1.RoundHole(SpaceOutX, (Main.maxTilesY - 72), 80, 27, 4, false, ref progress);
				
				for (int rotation3 = 0; rotation3 < 350; rotation3++)
				{
				int DistX = (int)(0 - (Math.Sin(rotation3)* 80));
				int DistY = (int)(0 - (Math.Cos(rotation3)* 27));
				
			    WorldGen.digTunnel(SpaceOutX + DistX, (Main.maxTilesY - 72) + DistY, 0, 0, 4, 4, false);
				}
				
				WorldGen.digTunnel(SpaceOutX, Main.maxTilesY - 72, 0, 0, 5, 5, false);
                prog = WorldMethods1Point1.RoundHole(SpaceOutX, (Main.maxTilesY - 72), 12, 6, 4, false, ref progress);
				
				for (int OreGen = 0; OreGen < 150; OreGen++) 
				{
					int why = (Main.maxTilesY - 108) + Main.rand.Next(-102, 75);
					int ex = SpaceOutX + Main.rand.Next(-102, 155);
					if (Main.tile[ex, why].TileType == ModContent.TileType<BrimstoneSlag>()) 
					{
						WorldGen.TileRunner(ex, why, (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), ModContent.TileType<BrimstoneCragRock>(), false, 0f, 0f, false, true);
					}				   // A = x, B = y.
				}
				
				for (int rotation3 = 0; rotation3 < 350; rotation3++)
				{
				int DistX = (int)(0 - (Math.Sin(rotation3)* 12));
				int DistY = (int)(0 - (Math.Cos(rotation3)* 6));
				
			    WorldGen.digTunnel(SpaceOutX + DistX, (Main.maxTilesY - 72) + DistY, 0, 0, 4, 4, false);
				}
				
			}));
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.SkyMill);
            recipe.AddIngredient(ItemID.SunplateBlock, 10);
            recipe.AddIngredient(ItemID.Cloud, 5);
            recipe.AddIngredient(ItemID.RainCloud, 3);
            recipe.AddTile(TileID.Anvils);
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
            recipe.AddIngredient(ItemID.Gel, 50);
            recipe.AddIngredient(ItemID.Cloud, 20);
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
            recipe.AddIngredient(ItemID.IronBar, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = Recipe.Create(ItemID.LavaCharm);
            recipe.AddIngredient(ItemID.LavaBucket, 5);
            recipe.AddIngredient(ItemID.Obsidian, 25);
            recipe.AddIngredient(ItemID.LeadBar, 2);
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
            recipe = Recipe.Create(ItemID.RocketI);
            recipe.AddIngredient(ItemID.EmptyBullet);
            recipe.AddIngredient(ItemID.ExplosivePowder, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override void AddRecipeGroups()/* tModPorter Note: Removed. Use ModSystem.AddRecipeGroups */
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
				CalamityModClassic1Point1.Instance.Find<ModItem>("SkylineWings").Type,
                CalamityModClassic1Point1.Instance.Find<ModItem>("StarlightWings").Type,
                CalamityModClassic1Point1.Instance.Find<ModItem>("AureateWings").Type,
                CalamityModClassic1Point1.Instance.Find<ModItem>("DiscordianWings").Type,
                CalamityModClassic1Point1.Instance.Find<ModItem>("TarragonWings").Type,
                CalamityModClassic1Point1.Instance.Find<ModItem>("XerocWings").Type
            });
            RecipeGroup.RegisterGroup("WingsGroup", group);
        }
    }
}
