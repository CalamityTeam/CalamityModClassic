using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using CalamityModClassicPreTrailer.Tiles;

namespace CalamityModClassicPreTrailer
{
	public static class CustomTileFraming
	{
		private static int[][] PlantCheckAgainst;
		private static Dictionary<ushort, ushort> VineToGrass;
		public static bool[] tileMergeAstralDirt;

		static CustomTileFraming()
		{
			Setup();
		}

		private static void Setup()
		{
			Mod mod = CalamityModClassicPreTrailer.Instance;

			int size = CalamityGlobalTile.PlantTypes.Length;
			PlantCheckAgainst = new int[TileLoader.TileCount][];

			PlantCheckAgainst[TileID.Plants] = new int[3] { TileID.Grass, TileID.PlanterBox, TileID.ClayPot };
			PlantCheckAgainst[TileID.CorruptPlants] = new int[1] { TileID.CorruptGrass };
			PlantCheckAgainst[TileID.JunglePlants] = new int[1] { TileID.JungleGrass };
			PlantCheckAgainst[TileID.MushroomPlants] = new int[1] { TileID.MushroomGrass };
			PlantCheckAgainst[TileID.Plants2] = new int[3] { TileID.Grass, TileID.PlanterBox, TileID.ClayPot };
			PlantCheckAgainst[TileID.JunglePlants2] = new int[1] { TileID.JungleGrass };
			PlantCheckAgainst[TileID.HallowedPlants] = new int[1] { TileID.HallowedGrass };
			PlantCheckAgainst[TileID.HallowedPlants2] = new int[1] { TileID.HallowedGrass };
			PlantCheckAgainst[TileID.CrimsonPlants] = new int[1] { TileID.CrimsonPlants };
			PlantCheckAgainst[CalamityModClassicPreTrailer.Instance.Find<ModTile>("AstralShortPlants").Type] = new int[1] { mod.Find<ModTile>("AstralGrass").Type };
			PlantCheckAgainst[CalamityModClassicPreTrailer.Instance.Find<ModTile>("AstralTallPlants").Type] = new int[1] { mod.Find<ModTile>("AstralGrass").Type };

			VineToGrass = new Dictionary<ushort, ushort>();
			VineToGrass[TileID.Vines] = TileID.Grass;
			VineToGrass[TileID.CrimsonVines] = TileID.CrimsonGrass;
			VineToGrass[TileID.HallowedVines] = TileID.HallowedGrass;
			VineToGrass[(ushort)mod.Find<ModTile>("AstralVines").Type] = (ushort)mod.Find<ModTile>("AstralGrass").Type;

			tileMergeAstralDirt = new bool[TileLoader.TileCount];
			//TODO: REST OF THESE
			tileMergeAstralDirt[mod.Find<ModTile>("AstralOre").Type] = true;
			tileMergeAstralDirt[mod.Find<ModTile>("AstralStone").Type] = true;
			tileMergeAstralDirt[mod.Find<ModTile>("AstralSand").Type] = true;
		}

		private static bool PlantNeedsUpdate(int plantType, int checkType)
		{
			if (PlantCheckAgainst[plantType] == null) return false;
			int size = PlantCheckAgainst[plantType].Length;
			for (int i = 0; i < size; i++)
			{
				if (PlantCheckAgainst[plantType][i] == checkType)
				{
					return false;
				}
			}
			return true;
		}

		public static void CheckPlants(int x, int y)
		{
			int checkType = -1;
			int plantType = Main.tile[x, y].TileType;

			if (y + 1 >= Main.maxTilesY)
				checkType = plantType;

			Tile tile = Main.tile[x, y];
			Tile below = Main.tile[x, y + 1];
			if (y + 1 < Main.maxTilesY && below != null && below.HasUnactuatedTile && !below.IsHalfBlock && below.Slope == 0)
				checkType = below.TileType;

			if (checkType == -1) return;

			//Check if this plant needs an update
			if (PlantNeedsUpdate(plantType, checkType))
			{
				if ((plantType == TileID.Plants || plantType == TileID.Plants2) && checkType != TileID.Grass && tile.TileFrameX >= 162)
				{
					Main.tile[x, y].TileFrameX = 126;
				}
				if (plantType == TileID.JunglePlants2 && checkType != TileID.JungleGrass && tile.TileFrameX >= 162)
				{
					Main.tile[x, y].TileFrameX = 126;
				}

				if (checkType == TileID.CorruptGrass)
				{
					plantType = TileID.CorruptPlants;
					if (tile.TileFrameX >= 162)
					{
						Main.tile[x, y].TileFrameX = 126;
					}
				}
				else if (checkType == TileID.Grass)
				{
					if (plantType == TileID.HallowedPlants2)
					{
						plantType = TileID.Plants2;
					}
					else
					{
						plantType = TileID.Plants;
					}
				}
				else if (checkType == TileID.HallowedGrass)
				{
					if (plantType == TileID.Plants2)
					{
						plantType = TileID.HallowedPlants2;
					}
					else
					{
						plantType = TileID.HallowedPlants;
					}
				}
				else if (checkType == TileID.CrimsonGrass)
				{
					plantType = TileID.CrimsonPlants;
				}
				else if (checkType == TileID.MushroomGrass)
				{
					plantType = TileID.MushroomPlants;
					while (Main.tile[x, y].TileFrameX > 72)
					{
						Main.tile[x, y].TileFrameX -= 72;
					}
				}
				else if (checkType == CalamityModClassicPreTrailer.Instance.Find<ModTile>("AstralGrass").Type) //ASTRAL
				{
					if (plantType == TileID.Plants || plantType == TileID.CorruptPlants ||
						plantType == TileID.CrimsonPlants || plantType == TileID.HallowedPlants ||
						plantType == TileID.MushroomPlants || plantType == TileID.JunglePlants)
					{
						plantType = CalamityModClassicPreTrailer.Instance.Find<ModTile>("AstralShortPlants").Type;
					}
					else
					{
						plantType = CalamityModClassicPreTrailer.Instance.Find<ModTile>("AstralTallPlants").Type;
					}
				}
				if (plantType != Main.tile[x, y].TileType)
				{
					Main.tile[x, y].TileType = (ushort)plantType;
					return;
				}
				WorldGen.KillTile(x, y, false, false, false);
			}
		}

		public static void VineFrame(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			int myType = tile.TileType;
			Tile tile2 = Main.tile[x, y - 1];
			Tile tile3 = Main.tile[x, y + 1];
			Tile tile4 = Main.tile[x - 1, y];
			Tile tile5 = Main.tile[x + 1, y];
			Tile tile6 = Main.tile[x - 1, y + 1];
			Tile tile7 = Main.tile[x + 1, y + 1];
			Tile tile8 = Main.tile[x - 1, y - 1];
			Tile tile9 = Main.tile[x + 1, y - 1];
			int num52 = -1;
			int num53 = -1;
			int num54 = -1;
			int num55 = -1;
			int num56 = -1;
			int num57 = -1;
			int num58 = -1;
			int num59 = -1;
			if (tile4 != null && tile4.HasTile)
			{
				if (Main.tileStone[(int)tile4.TileType])
				{
					num55 = 1;
				}
				else
				{
					num55 = (int)tile4.TileType;
				}
			}
			if (tile5 != null && tile5.HasTile)
			{
				if (Main.tileStone[(int)tile5.TileType])
				{
					num56 = 1;
				}
				else
				{
					num56 = (int)tile5.TileType;
				}
			}
			if (tile2 != null && tile2.HasTile)
			{
				if (Main.tileStone[(int)tile2.TileType])
				{
					num53 = 1;
				}
				else
				{
					num53 = (int)tile2.TileType;
				}
			}
			if (tile3 != null && tile3.HasTile)
			{
				if (Main.tileStone[(int)tile3.TileType])
				{
					num58 = 1;
				}
				else
				{
					num58 = (int)tile3.TileType;
				}
			}
			if (tile8 != null && tile8.HasTile)
			{
				if (Main.tileStone[(int)tile8.TileType])
				{
					num52 = 1;
				}
				else
				{
					num52 = (int)tile8.TileType;
				}
			}
			if (tile9 != null && tile9.HasTile)
			{
				if (Main.tileStone[(int)tile9.TileType])
				{
					num54 = 1;
				}
				else
				{
					num54 = (int)tile9.TileType;
				}
			}
			if (tile6 != null && tile6.HasTile)
			{
				if (Main.tileStone[(int)tile6.TileType])
				{
					num57 = 1;
				}
				else
				{
					num57 = (int)tile6.TileType;
				}
			}
			if (tile7 != null && tile7.HasTile)
			{
				if (Main.tileStone[(int)tile7.TileType])
				{
					num59 = 1;
				}
				else
				{
					num59 = (int)tile7.TileType;
				}
			}

			if (tile2 != null)
			{
				if (!tile2.HasTile)
				{
					num53 = -1;
				}
				else if (tile2.BottomSlope)
				{
					num53 = -1;
				}
				else
				{
					num53 = (int)tile2.TileType;
				}
			}
			else
			{
				num53 = myType;
			}

			ushort[] vines = VineToGrass.Keys.ToArray();

			for (int i = 0; i < vines.Length; i++)
			{
				ushort myGrassType = VineToGrass[(ushort)vines[i]];
				if (myType != vines[i] && (num53 == myGrassType || num53 == vines[i]))
				{
					Main.tile[x, y].TileType = vines[i];
					WorldGen.SquareTileFrame(x, y, true);
					return;
				}
			}

			if (num53 != myType)
			{
				bool flag17 = false;
				if (num53 == -1)
				{
					flag17 = true;
				}
				else
				{
					for (int i = 0; i < vines.Length; i++)
					{
						if (myType != TileID.Vines)
						{
							if (myType == vines[i] && num53 != VineToGrass[vines[i]])
							{
								flag17 = true;
							}
						}
						else if (num53 != TileID.Grass && num53 != TileID.LeafBlock)
						{
							flag17 = true;
						}
					}
				}
				if (flag17)
				{
					WorldGen.KillTile(x, y, false, false, false);
				}
			}
			return;
		}

		enum Similarity
		{
			Same,
			MergeLink,
			None
		}

		public static void FrameTileForCustomMerge(int x, int y, int myType, int mergeType, bool forceSameDown = false, bool forceSameUp = false, bool forceSameLeft = false, bool forceSameRight = false, bool resetFrame = true)
		{
			bool tmp;
			FrameTileForCustomMerge(x, y, myType, mergeType, out tmp, out tmp, out tmp, out tmp, forceSameDown, forceSameUp, forceSameLeft, forceSameRight, resetFrame);
		}

		public static void FrameTileForCustomMerge(int x, int y, int myType, int mergeType, out bool mergedUp, out bool mergedLeft, out bool mergedRight, out bool mergedDown, bool forceSameDown = false, bool forceSameUp = false, bool forceSameLeft = false, bool forceSameRight = false, bool resetFrame = true)
		{
			Tile tileLeft = Main.tile[x - 1, y];
			Tile tileRight = Main.tile[x + 1, y];
			Tile tileUp = Main.tile[x, y - 1];
			Tile tileDown = Main.tile[x, y + 1];
			Tile tileTopLeft = Main.tile[x - 1, y - 1];
			Tile tileTopRight = Main.tile[x + 1, y - 1];
			Tile tileBottomLeft = Main.tile[x - 1, y + 1];
			Tile tileBottomRight = Main.tile[x + 1, y + 1];

			//CARDINAL
			Similarity leftSim = GetSimilarity(tileLeft, myType, mergeType);
			if (forceSameLeft) leftSim = Similarity.Same;
			Similarity rightSim = GetSimilarity(tileRight, myType, mergeType);
			if (forceSameRight) rightSim = Similarity.Same;
			Similarity upSim = GetSimilarity(tileUp, myType, mergeType);
			if (forceSameUp) upSim = Similarity.Same;
			Similarity downSim = GetSimilarity(tileDown, myType, mergeType);
			if (forceSameDown) downSim = Similarity.Same;

			//DIAGONAL
			Similarity topLeftSim = GetSimilarity(tileTopLeft, myType, mergeType);
			Similarity topRightSim = GetSimilarity(tileTopRight, myType, mergeType);
			Similarity bottomLeftSim = GetSimilarity(tileBottomLeft, myType, mergeType);
			Similarity bottomRightSim = GetSimilarity(tileBottomRight, myType, mergeType);

			int randomFrame;
			if (resetFrame)
			{
				randomFrame = WorldGen.genRand.Next(3);
				Main.tile[x, y].Get<TileWallWireStateData>().TileFrameNumber = (byte)randomFrame;
			}
			else
			{
				randomFrame = Main.tile[x, y].TileFrameNumber;
			}

			/* DEBUGGING
            if (Player.tileTargetX == x && Player.tileTargetY == y)
            {
                Main.NewText(StringWriter(leftSim, rightSim, upSim, downSim, topLeftSim, topRightSim, bottomLeftSim, bottomRightSim, forceSameDown, forceSameUp, forceSameLeft, forceSameRight));
            }*/

			mergedDown = mergedLeft = mergedRight = mergedUp = false;

			if (leftSim == Similarity.None)
			{
				if (upSim == Similarity.Same)
				{
					if (downSim == Similarity.Same)
					{
						if (rightSim == Similarity.Same)
						{
							SetFrameAt(x, y, 0, 18 * randomFrame);
							return;
						}
						else if (rightSim == Similarity.MergeLink)
						{
							mergedRight = true;
							SetFrameAt(x, y, 234 + 18 * randomFrame, 36);
							return;
						}
						SetFrameAt(x, y, 90, 18 * randomFrame);
						return;
					}
					else if (downSim == Similarity.MergeLink)
					{
						if (rightSim == Similarity.Same)
						{
							mergedDown = true;
							SetFrameAt(x, y, 72, 90 + 18 * randomFrame);
							return;
						}
						else if (rightSim == Similarity.MergeLink)
						{
							SetFrameAt(x, y, 108 + 18 * randomFrame, 54);
							return;
						}
						mergedDown = true;
						SetFrameAt(x, y, 126, 90 + 18 * randomFrame);
						return;
					}
					if (rightSim == Similarity.Same)
					{
						SetFrameAt(x, y, 36 * randomFrame, 72);
						return;
					}
					SetFrameAt(x, y, 108 + 18 * randomFrame, 54);
					return;
				}
				else if (upSim == Similarity.MergeLink)
				{
					if (downSim == Similarity.Same)
					{
						if (rightSim == Similarity.Same)
						{
							mergedUp = true;
							SetFrameAt(x, y, 72, 144 + 18 * randomFrame);
							return;
						}
						else if (rightSim == Similarity.MergeLink)
						{
							SetFrameAt(x, y, 108 + 18 * randomFrame, 0);
							return;
						}
						mergedUp = true;
						SetFrameAt(x, y, 126, 144 + 18 * randomFrame);
						return;
					}
					else if (downSim == Similarity.MergeLink)
					{
						if (rightSim == Similarity.Same)
						{
							SetFrameAt(x, y, 162, 18 * randomFrame);
							return;
						}
						else if (rightSim == Similarity.MergeLink)
						{
							SetFrameAt(x, y, 162 + 18 * randomFrame, 54);
							return;
						}
						mergedUp = true;
						mergedDown = true;
						SetFrameAt(x, y, 108, 216 + 18 * randomFrame);
						return;
					}
					if (rightSim == Similarity.Same)
					{
						SetFrameAt(x, y, 162, 18 * randomFrame);
						return;
					}
					else if (rightSim == Similarity.MergeLink)
					{
						SetFrameAt(x, y, 162 + 18 * randomFrame, 54);
						return;
					}
					mergedUp = true;
					SetFrameAt(x, y, 108, 144 + 18 * randomFrame);
					return;
				}
				if (downSim == Similarity.Same)
				{
					if (rightSim == Similarity.Same)
					{
						SetFrameAt(x, y, 36 * randomFrame, 54);
						return;
					}
					else if (rightSim == Similarity.MergeLink)
					{
						SetFrameAt(x, y, 108 + 18 * randomFrame, 0);
						return;
					}
					SetFrameAt(x, y, 108 + 18 * randomFrame, 0);
					return;
				}
				else if (downSim == Similarity.MergeLink)
				{
					if (rightSim == Similarity.Same)
					{
						SetFrameAt(x, y, 162, 18 * randomFrame);
						return;
					}
					else if (rightSim == Similarity.MergeLink)
					{
						SetFrameAt(x, y, 162 + 18 * randomFrame, 54);
						return;
					}
					mergedDown = true;
					SetFrameAt(x, y, 108, 90 + 18 * randomFrame);
					return;
				}
				if (rightSim == Similarity.Same)
				{
					SetFrameAt(x, y, 162, 18 * randomFrame);
					return;
				}
				else if (rightSim == Similarity.MergeLink)
				{
					mergedRight = true;
					SetFrameAt(x, y, 54 + 18 * randomFrame, 234);
					return;
				}
				SetFrameAt(x, y, 162 + 18 * randomFrame, 54);
				return;
			}
			else if (leftSim == Similarity.MergeLink)
			{
				if (upSim == Similarity.Same)
				{
					if (downSim == Similarity.Same)
					{
						if (rightSim == Similarity.Same)
						{
							mergedLeft = true;
							SetFrameAt(x, y, 162, 126 + 18 * randomFrame);
							return;
						}
						else if (rightSim == Similarity.MergeLink)
						{
							mergedLeft = true;
							mergedRight = true;
							SetFrameAt(x, y, 180, 126 + 18 * randomFrame);
							return;
						}
						mergedLeft = true;
						SetFrameAt(x, y, 234 + 18 * randomFrame, 54);
						return;
					}
					else if (downSim == Similarity.MergeLink)
					{
						if (rightSim == Similarity.Same)
						{
							mergedLeft = mergedDown = true;
							SetFrameAt(x, y, 36, 108 + 36 * randomFrame);
							return;
						}
						else if (rightSim == Similarity.MergeLink)
						{
							mergedLeft = mergedRight = mergedDown = true;
							SetFrameAt(x, y, 198, 144 + 18 * randomFrame);
							return;
						}
						SetFrameAt(x, y, 108 + 18 * randomFrame, 54);
						return;
					}
					if (rightSim == Similarity.Same)
					{
						mergedLeft = true;
						SetFrameAt(x, y, 18 * randomFrame, 216);
						return;
					}
					SetFrameAt(x, y, 108 + 18 * randomFrame, 54);
					return;
				}
				else if (upSim == Similarity.MergeLink)
				{
					if (downSim == Similarity.Same)
					{
						if (rightSim == Similarity.Same)
						{
							mergedUp = mergedLeft = true;
							SetFrameAt(x, y, 36, 90 + 36 * randomFrame);
							return;
						}
						else if (rightSim == Similarity.MergeLink)
						{
							mergedLeft = mergedRight = mergedUp = true;
							SetFrameAt(x, y, 198, 90 + 18 * randomFrame);
							return;
						}
						SetFrameAt(x, y, 108 + 18 * randomFrame, 0);
						return;
					}
					else if (downSim == Similarity.MergeLink)
					{
						if (rightSim == Similarity.Same)
						{
							mergedUp = mergedLeft = mergedDown = true;
							SetFrameAt(x, y, 216, 90 + 18 * randomFrame);
							return;
						}
						else if (rightSim == Similarity.MergeLink)
						{
							mergedDown = mergedLeft = mergedRight = mergedUp = true;
							SetFrameAt(x, y, 108 + 18 * randomFrame, 198);
							return;
						}
						SetFrameAt(x, y, 162 + 18 * randomFrame, 54);
						return;
					}
					if (rightSim == Similarity.Same)
					{
						SetFrameAt(x, y, 162, 18 * randomFrame);
						return;
					}
					SetFrameAt(x, y, 162 + 18 * randomFrame, 54);
					return;
				}
				if (downSim == Similarity.Same)
				{
					if (rightSim == Similarity.Same)
					{
						mergedLeft = true;
						SetFrameAt(x, y, 18 * randomFrame, 198);
						return;
					}
					else if (rightSim == Similarity.MergeLink)
					{
						SetFrameAt(x, y, 108 + 18 * randomFrame, 0);
						return;
					}
					SetFrameAt(x, y, 108 + 18 * randomFrame, 0);
					return;
				}
				else if (downSim == Similarity.MergeLink)
				{
					if (rightSim == Similarity.Same)
					{
						SetFrameAt(x, y, 162, 18 * randomFrame);
						return;
					}
					else if (rightSim == Similarity.MergeLink)
					{
						SetFrameAt(x, y, 162 + 18 * randomFrame, 54);
						return;
					}
					SetFrameAt(x, y, 162 + 18 * randomFrame, 54);
					return;
				}
				if (rightSim == Similarity.Same)
				{
					mergedLeft = true;
					SetFrameAt(x, y, 18 * randomFrame, 252);
					return;
				}
				else if (rightSim == Similarity.MergeLink)
				{
					mergedRight = mergedLeft = true;
					SetFrameAt(x, y, 162 + 18 * randomFrame, 198);
					return;
				}
				mergedLeft = true;
				SetFrameAt(x, y, 18 * randomFrame, 234);
				return;
			}
			if (upSim == Similarity.Same)
			{
				if (downSim == Similarity.Same)
				{
					if (rightSim == Similarity.Same)
					{
						#region FULL TILE STUFF
						if (topLeftSim == Similarity.MergeLink || topRightSim == Similarity.MergeLink || bottomLeftSim == Similarity.MergeLink || bottomRightSim == Similarity.MergeLink)
						{
							if (bottomRightSim == Similarity.MergeLink)
							{
								SetFrameAt(x, y, 0, 90 + 36 * randomFrame);
								return;
							}
							else if (bottomLeftSim == Similarity.MergeLink)
							{
								SetFrameAt(x, y, 18, 90 + 36 * randomFrame);
								return;
							}
							else if (topRightSim == Similarity.MergeLink)
							{
								SetFrameAt(x, y, 0, 108 + 36 * randomFrame);
								return;
							}
							SetFrameAt(x, y, 18, 108 + 36 * randomFrame);
							return;
						}
						if (topLeftSim == Similarity.Same)
						{
							if (topRightSim == Similarity.Same)
							{
								if (bottomLeftSim == Similarity.Same)
								{
									SetFrameAt(x, y, 18 + 18 * randomFrame, 18);
									return;
								}
								if (bottomRightSim == Similarity.Same)
								{
									SetFrameAt(x, y, 18 + 18 * randomFrame, 18);
									return;
								}
								SetFrameAt(x, y, 108 + 18 * randomFrame, 36);
								return;
							}
							if (bottomLeftSim == Similarity.Same)
							{
								if (bottomRightSim == Similarity.Same)
								{
									if (topRightSim == Similarity.MergeLink)
									{
										SetFrameAt(x, y, 0, 108 + 36 * randomFrame);
										return;
									}
									SetFrameAt(x, y, 18 + 18 * randomFrame, 18);
									return;
								}
								SetFrameAt(x, y, 198, 18 * randomFrame);
								return;
							}
						}
						else if (topLeftSim == Similarity.None)
						{
							if (topRightSim == Similarity.Same)
							{
								if (bottomRightSim == Similarity.Same)
								{
									if (bottomLeftSim == Similarity.Same)
									{
										SetFrameAt(x, y, 18 + 18 * randomFrame, 18);
										return;
									}
									SetFrameAt(x, y, 18 + 18 * randomFrame, 18);
									return;
								}
								if (bottomLeftSim == Similarity.Same)
								{
									SetFrameAt(x, y, 18 + 18 * randomFrame, 18);
									return;
								}
							}
							SetFrameAt(x, y, 18 + 18 * randomFrame, 18);
							return;
						}
						SetFrameAt(x, y, 18 + 18 * randomFrame, 18);
						return;
						#endregion
					}
					else if (rightSim == Similarity.MergeLink)
					{
						mergedRight = true;
						SetFrameAt(x, y, 144, 126 + 18 * randomFrame);
						return;
					}
					SetFrameAt(x, y, 72, 18 * randomFrame);
					return;
				}
				else if (downSim == Similarity.MergeLink)
				{
					if (rightSim == Similarity.Same)
					{
						mergedDown = true;
						SetFrameAt(x, y, 144 + 18 * randomFrame, 90);
						return;
					}
					else if (rightSim == Similarity.MergeLink)
					{
						mergedDown = mergedRight = true;
						SetFrameAt(x, y, 54, 108 + 36 * randomFrame);
						return;
					}
					mergedDown = true;
					SetFrameAt(x, y, 90, 90 + 18 * randomFrame);
					return;
				}
				if (rightSim == Similarity.Same)
				{
					SetFrameAt(x, y, 18 + 18 * randomFrame, 36);
					return;
				}
				else if (rightSim == Similarity.MergeLink)
				{
					mergedRight = true;
					SetFrameAt(x, y, 54 + 18 * randomFrame, 216);
					return;
				}
				SetFrameAt(x, y, 18 + 36 * randomFrame, 72);
				return;
			}
			else if (upSim == Similarity.MergeLink)
			{
				if (downSim == Similarity.Same)
				{
					if (rightSim == Similarity.Same)
					{
						mergedUp = true;
						SetFrameAt(x, y, 144 + 18 * randomFrame, 108);
						return;
					}
					else if (rightSim == Similarity.MergeLink)
					{
						mergedRight = mergedUp = true;
						SetFrameAt(x, y, 54, 90 + 36 * randomFrame);
						return;
					}
					mergedUp = true;
					SetFrameAt(x, y, 90, 144 + 18 * randomFrame);
					return;
				}
				else if (downSim == Similarity.MergeLink)
				{
					if (rightSim == Similarity.Same)
					{
						mergedUp = mergedDown = true;
						SetFrameAt(x, y, 144 + 18 * randomFrame, 180);
						return;
					}
					else if (rightSim == Similarity.MergeLink)
					{
						mergedUp = mergedRight = mergedDown = true;
						SetFrameAt(x, y, 216, 144 + 18 * randomFrame);
						return;
					}
					SetFrameAt(x, y, 216, 18 * randomFrame);
					return;
				}
				if (rightSim == Similarity.Same)
				{
					mergedUp = true;
					SetFrameAt(x, y, 234 + 18 * randomFrame, 18);
					return;
				}
				SetFrameAt(x, y, 216, 18 * randomFrame);
				return;
			}
			if (downSim == Similarity.Same)
			{
				if (rightSim == Similarity.Same)
				{
					SetFrameAt(x, y, 18 + 18 * randomFrame, 0);
					return;
				}
				else if (rightSim == Similarity.MergeLink)
				{
					mergedRight = true;
					SetFrameAt(x, y, 54 + 18 * randomFrame, 198);
					return;
				}
				SetFrameAt(x, y, 18 + 36 * randomFrame, 54);
				return;
			}
			else if (downSim == Similarity.MergeLink)
			{
				if (rightSim == Similarity.Same)
				{
					mergedDown = true;
					SetFrameAt(x, y, 234 + 18 * randomFrame, 0);
					return;
				}
				SetFrameAt(x, y, 216, 18 * randomFrame);
				return;
			}
			if (rightSim == Similarity.Same)
			{
				SetFrameAt(x, y, 108 + 18 * randomFrame, 72);
				return;
			}
			else if (rightSim == Similarity.MergeLink)
			{
				mergedRight = true;
				SetFrameAt(x, y, 54 + 18 * randomFrame, 252);
				return;
			}
			SetFrameAt(x, y, 216, 18 * randomFrame);
			return;
		}

		private static void SetFrameAt(int x, int y, int frameX, int frameY)
		{
			Main.tile[x, y].TileFrameX = (short)frameX;
			Main.tile[x, y].TileFrameY = (short)frameY;
		}

		private static Similarity GetSimilarity(Tile check, int myType, int mergeType)
		{
			if (!check.HasTile) return Similarity.None;

			if (check.TileType == myType || Main.tileMerge[myType][check.TileType])
				return Similarity.Same;
			else if (check.TileType == mergeType)
				return Similarity.MergeLink;

			return Similarity.None;
		}

		private static string StringWriter(params object[] strings)
		{
			string s = "";
			for (int i = 0; i < strings.Length; i++)
			{
				s += strings[i].ToString();
				if (i != strings.Length - 1)
				{
					s += ", ";
				}
			}
			return s;
		}
	}
}
