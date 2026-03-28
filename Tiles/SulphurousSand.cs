using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Tiles
{
	public class SulphurousSand : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			DustType = 32;
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Sulphurous Sand");
			AddMapEntry(new Color(150, 100, 50), name);
			MineResist = 1f;
			MinPick = 54;
			HitSound = SoundID.Dig;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (i < 250 && i > 150)
			{
				if (!closer)
				{
					if (Main.tile[i - 1, j] != null)
					{
						if (!Main.tile[i - 1, j].HasTile)
						{
							if (Main.tile[i - 1, j].LiquidAmount <= 128)
							{
								Main.tile[i - 1, j].LiquidAmount = 255;
							}
						}
					}
					if (Main.tile[i - 2, j] != null)
					{
						if (!Main.tile[i - 2, j].HasTile)
						{
							if (Main.tile[i - 2, j].LiquidAmount <= 128)
							{
								Main.tile[i - 2, j].LiquidAmount = 255;
							}
						}
					}
					if (Main.tile[i - 3, j] != null)
					{
						if (!Main.tile[i - 3, j].HasTile)
						{
							if (Main.tile[i - 3, j].LiquidAmount <= 128)
							{
								Main.tile[i - 3, j].LiquidAmount = 255;
							}
						}
					}
				}
			}
			else if (i > Main.maxTilesX - 250 && i < Main.maxTilesX - 150)
			{
				if (!closer)
				{
					if (Main.tile[i + 1, j] != null)
					{
						if (!Main.tile[i + 1, j].HasTile)
						{
							if (Main.tile[i + 1, j].LiquidAmount <= 128)
							{
								Main.tile[i + 1, j].LiquidAmount = 255;
							}
						}
					}
					if (Main.tile[i + 2, j] != null)
					{
						if (!Main.tile[i + 2, j].HasTile)
						{
							if (Main.tile[i + 2, j].LiquidAmount <= 128)
							{
								Main.tile[i + 2, j].LiquidAmount = 255;
							}
						}
					}
					if (Main.tile[i + 3, j] != null)
					{
						if (!Main.tile[i + 3, j].HasTile)
						{
							if (Main.tile[i + 3, j].LiquidAmount <= 128)
							{
								Main.tile[i + 3, j].LiquidAmount = 255;
							}
						}
					}
				}
			}
		}

		public override void RandomUpdate(int i, int j)
		{
			int tileLocationY = j - 1;
			if (Main.tile[i, tileLocationY] != null)
			{
				if (!Main.tile[i, tileLocationY].HasTile)
				{
					if (Main.tile[i, tileLocationY].LiquidAmount == 255 && Main.tile[i, tileLocationY - 1].LiquidAmount == 255 &&
						Main.tile[i, tileLocationY - 2].LiquidAmount == 255 && Main.netMode != 1)
					{
						Projectile.NewProjectile(new EntitySource_TileBreak(i, j), (float)(i * 16 + 16), (float)(tileLocationY * 16 + 16), 0f, -0.1f, Mod.Find<ModProjectile>("SulphuricAcidCannon").Type, 0, 2f, Main.myPlayer, 0f, 0f);
					}
					if (i < 250 || i > Main.maxTilesX - 250)
					{
						if (Main.rand.Next(400) == 0)
						{
							if (Main.tile[i, tileLocationY].LiquidAmount == 255)
							{
								int num13 = 7;
								int num14 = 6;
								int num15 = 0;
								for (int l = i - num13; l <= i + num13; l++)
								{
									for (int m = tileLocationY - num13; m <= tileLocationY + num13; m++)
									{
										if (Main.tile[l, m].HasTile && Main.tile[l, m].TileType == 81)
										{
											num15++;
										}
									}
								}
								if (num15 < num14 && Main.tile[i, tileLocationY - 1].LiquidAmount == 255 &&
									Main.tile[i, tileLocationY - 2].LiquidAmount == 255 && Main.tile[i, tileLocationY - 3].LiquidAmount == 255 &&
									Main.tile[i, tileLocationY - 4].LiquidAmount == 255)
								{
									WorldGen.PlaceTile(i, tileLocationY, 81, true, false, -1, 0);
									if (Main.netMode == 2 && Main.tile[i, tileLocationY].HasTile)
									{
										NetMessage.SendTileSquare(-1, i, tileLocationY, 1, TileChangeType.None);
									}
								}
							}
							else if (Main.tile[i, tileLocationY].LiquidAmount == 0)
							{
								int num13 = 7;
								int num14 = 6;
								int num15 = 0;
								for (int l = i - num13; l <= i + num13; l++)
								{
									for (int m = tileLocationY - num13; m <= tileLocationY + num13; m++)
									{
										if (Main.tile[l, m].HasTile && Main.tile[l, m].TileType == 324)
										{
											num15++;
										}
									}
								}
								if (num15 < num14)
								{
									WorldGen.PlaceTile(i, tileLocationY, 324, true, false, -1, Main.rand.Next(2));
									if (Main.netMode == 2 && Main.tile[i, tileLocationY].HasTile)
									{
										NetMessage.SendTileSquare(-1, i, tileLocationY, 1, TileChangeType.None);
									}
								}
							}
						}
					}
				}
			}
		}
	}
}