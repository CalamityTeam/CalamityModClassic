using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Tiles
{
	public class PlantyMush : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			DustType = 2;
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Planty Mush");
			AddMapEntry(new Color(0, 120, 0), name);
			MineResist = 1f;
			MinPick = 199;
			HitSound = SoundID.Dig;
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
			int num8 = WorldGen.genRand.Next((int)Main.rockLayer, (int)(Main.rockLayer + (double)Main.maxTilesY * 0.143));
			if (Main.tile[i, j + 1] != null)
			{
				if (!Main.tile[i, j + 1].HasTile && Main.tile[i, j + 1].TileType != (ushort)Mod.Find<ModTile>("ViperVines").Type)
				{
					if (Main.tile[i, j + 1].LiquidAmount == 255 &&
						(Main.tile[i, j + 1].WallType == (ushort)Mod.Find<ModWall>("MossyGravelWall").Type ||
						Main.tile[i, j + 1].WallType == (ushort)Mod.Find<ModWall>("AbyssGravelWall").Type) &&
						!(Main.tile[i, j + 1].LiquidType == LiquidID.Lava))
					{
						bool flag13 = false;
						for (int num52 = num8; num52 > num8 - 10; num52--)
						{
							if (Main.tile[i, num52].BottomSlope)
							{
								flag13 = false;
								break;
							}
							if (Main.tile[i, num52].HasTile && !Main.tile[i, num52].BottomSlope)
							{
								flag13 = true;
								break;
							}
						}
						if (flag13)
						{
							int num53 = i;
							int num54 = j + 1;
							Main.tile[num53, num54].TileType = (ushort)Mod.Find<ModTile>("ViperVines").Type;
							Main.tile[num53, num54].Get<TileWallWireStateData>().HasTile = true;
							WorldGen.SquareTileFrame(num53, num54, true);
							if (Main.netMode == 2)
							{
								NetMessage.SendTileSquare(-1, num53, num54, 3, TileChangeType.None);
							}
						}
					}
				}
			}
		}
	}
}