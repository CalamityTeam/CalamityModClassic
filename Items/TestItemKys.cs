using CalamityModClassic1Point1.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items
{
    public class TestItemKys : ModItem
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Calamity Biome Spawner");
            Item.width = 20;
            Item.height = 20;
            ////Tooltip.SetDefault("Right Click to use\nOnly use if you don't have the Calamity biome in your world\nDon't panic if your game freezes, you need to wait while the biome generates");
            Item.maxStack = 30;
        }
        
        public override bool CanRightClick()
        {
            return true;
        }
        
        public override void RightClick(Player player)
		{
            //clearing space
            int SpaceOutX = Main.rand.Next(130, 155);
            int SpaceOutY = Main.maxTilesY - Main.rand.Next(30, 40);
            WorldMethods1Point1.RoundHole(SpaceOutX, SpaceOutY, 100, 125, 55, true);

            WorldGen.digTunnel(SpaceOutX, SpaceOutY, 0, 0, 55, 55, false);
            for (int rotation2 = 0; rotation2 < 350; rotation2++)
            {
                int DistX = (int)(0 - (Math.Sin(rotation2) * 100));
                int DistY = (int)(0 - (Math.Cos(rotation2) * 125));
                WorldGen.digTunnel(SpaceOutX + DistX, SpaceOutY + DistY, 0, 0, 55, 55, false);
            }

            //Generating random spikes layer 1-
            for (int J = 20; J < (SpaceOutX * 2) + 10; J++)
            {
                WorldMethods1Point1.TileRunner(J, Main.maxTilesY - 108, (double)Main.rand.Next(12, 15), 1, (ushort)ModContent.TileType<BrimstoneSlag>(), true, 0f, 0f, false, true);
                WorldMethods1Point1.TileRunner(J, Main.maxTilesY - 54, (double)102, 1, (ushort)ModContent.TileType<BrimstoneSlag>(), true, 0f, 0f, false, true);
                WorldMethods1Point1.TileRunner(J + 10, Main.maxTilesY - 27, (double)75, 1, (ushort)ModContent.TileType<BrimstoneSlag>(), true, 0f, 0f, false, true);
                WorldMethods1Point1.TileRunner(J + 10, Main.maxTilesY - 78, (double)75, 1, (ushort)ModContent.TileType<BrimstoneSlag>(), true, 0f, 0f, false, true);
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
            }
            int Position = Main.rand.Next(183, 234);
            WorldMethods1Point1.CragSpike(SpaceOutX, Main.maxTilesY - Position, 1, Main.rand.Next(145, 167), (ushort)ModContent.TileType<BrimstoneSlag>(), (float)4, (float)4);
            WorldGen.digTunnel(SpaceOutX + 3, (Main.maxTilesY - Position) + 30, 0, 0, 6, 6, false);
            WorldGen.digTunnel(SpaceOutX - 3, (Main.maxTilesY - Position) + 30, 0, 0, 6, 6, false);
            for (int TunnelPlace = (Main.maxTilesY - Position) + 30; TunnelPlace < Main.maxTilesY - 95; TunnelPlace++)
            {
                WorldGen.digTunnel(SpaceOutX, TunnelPlace, 0, 0, 3, 3, false);
            }

            WorldMethods1Point1.RoundHole(SpaceOutX, (Main.maxTilesY - 72), 80, 27, 4, false);

            for (int rotation3 = 0; rotation3 < 350; rotation3++)
            {
                int DistX = (int)(0 - (Math.Sin(rotation3) * 80));
                int DistY = (int)(0 - (Math.Cos(rotation3) * 27));

                WorldGen.digTunnel(SpaceOutX + DistX, (Main.maxTilesY - 72) + DistY, 0, 0, 4, 4, false);
            }

            WorldGen.digTunnel(SpaceOutX, Main.maxTilesY - 72, 0, 0, 5, 5, false);
            WorldMethods1Point1.RoundHole(SpaceOutX, (Main.maxTilesY - 72), 12, 6, 4, false);

            for (int OreGen = 0; OreGen < 150; OreGen++)
            {
                int why = (Main.maxTilesY - 108) + Main.rand.Next(-102, 75);
                int ex = SpaceOutX + Main.rand.Next(-102, 155);
                if (Main.tile[ex, why].TileType == (ushort)ModContent.TileType<BrimstoneSlag>())
                {
                    WorldGen.TileRunner(ex, why, (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), (ushort)ModContent.TileType<BrimstoneCragRock>(), false, 0f, 0f, false, true);
                }                  // A = x, B = y.
            }

            for (int rotation3 = 0; rotation3 < 350; rotation3++)
            {
                int DistX = (int)(0 - (Math.Sin(rotation3) * 12));
                int DistY = (int)(0 - (Math.Cos(rotation3) * 6));

                WorldGen.digTunnel(SpaceOutX + DistX, (Main.maxTilesY - 72) + DistY, 0, 0, 4, 4, false);
            }
        }
    }
}
