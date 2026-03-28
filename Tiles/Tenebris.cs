using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Tiles
{
	public class Tenebris : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			DustType = 44;
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Tenebris");
 			AddMapEntry(new Color(0, 100, 100), name);
			MineResist = 3f;
			MinPick = 199;
			HitSound = SoundID.Tink;
		}

        public override bool CanExplode(int i, int j)
        {
            return NPC.downedPlantBoss || CalamityWorldPreTrailer.downedCalamitas;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (!closer && j < Main.maxTilesY - 205)
			{
				if (Main.tile[i, j].LiquidAmount <= 0)
				{
					Main.tile[i, j].LiquidAmount = 255;
					
				}
			}
		}

		public override void RandomUpdate(int i, int j)
        {
            if (NPC.downedPlantBoss || CalamityWorldPreTrailer.downedCalamitas)
            {
                int random = WorldGen.genRand.Next(4);
                if (random == 0)
                {
                    i++;
                }
                if (random == 1)
                {
                    i--;
                }
                if (random == 2)
                {
                    j++;
                }
                if (random == 3)
                {
                    j--;
                }
                if (Main.tile[i, j] != null)
                {
                    if (Main.tile[i, j].HasTile)
                    {
                        if (Main.tile[i, j].TileType == Mod.Find<ModTile>("PlantyMush").Type && WorldGen.genRand.Next(5) == 0 && !(Main.tile[i, j].LiquidType == LiquidID.Lava))
                        {
                            Main.tile[i, j].TileType = (ushort)Mod.Find<ModTile>("Tenebris").Type;
                            WorldGen.SquareTileFrame(i, j, true);
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
                            }
                        }
                    }
                }
            }
        }
    }
}