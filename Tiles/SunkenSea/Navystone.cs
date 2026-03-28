using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Tiles.SunkenSea
{
	public class Navystone : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			DustType = 96;
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Navystone");
 			AddMapEntry(new Color(0, 50, 50), name);
			MineResist = 2f;
			MinPick = 54;
			HitSound = SoundID.Tink;
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return CalamityWorldPreTrailer.downedDesertScourge;
		}

		public override bool CanExplode(int i, int j)
		{
			return CalamityWorldPreTrailer.downedDesertScourge;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void RandomUpdate(int i, int j)
		{
			if (Main.rand.Next(40) == 0)
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
					if (!Main.tile[i, j].HasTile)
					{
						if (!(Main.tile[i, j].LiquidType == LiquidID.Lava) && Main.tile[i, j].Slope == 0 && !Main.tile[i, j].IsHalfBlock)
						{
							Main.tile[i, j].TileType = (ushort)Mod.Find<ModTile>("SeaPrismCrystals").Type;
							Main.tile[i, j].Get<TileWallWireStateData>().HasTile = true;
							if (Main.tile[i, j + 1].HasTile && Main.tileSolid[Main.tile[i, j + 1].TileType] && Main.tile[i, j + 1].Slope == 0 && !Main.tile[i, j + 1].IsHalfBlock)
							{
								Main.tile[i, j].TileFrameY = (short)(0 * 18);
							}
							else if (Main.tile[i, j - 1].HasTile && Main.tileSolid[Main.tile[i, j - 1].TileType] && Main.tile[i, j - 1].Slope == 0 && !Main.tile[i, j - 1].IsHalfBlock)
							{
								Main.tile[i, j].TileFrameY = (short)(1 * 18);
							}
							else if (Main.tile[i + 1, j].HasTile && Main.tileSolid[Main.tile[i + 1, j].TileType] && Main.tile[i + 1, j].Slope == 0 && !Main.tile[i + 1, j].IsHalfBlock)
							{
								Main.tile[i, j].TileFrameY = (short)(2 * 18);
							}
							else if (Main.tile[i - 1, j].HasTile && Main.tileSolid[Main.tile[i - 1, j].TileType] && Main.tile[i - 1, j].Slope == 0 && !Main.tile[i - 1, j].IsHalfBlock)
							{
								Main.tile[i, j].TileFrameY = (short)(3 * 18);
							}
							Main.tile[i, j].TileFrameX = (short)(WorldGen.genRand.Next(18) * 18);
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