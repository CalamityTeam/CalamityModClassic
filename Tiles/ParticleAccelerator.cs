using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using CalamityModClassic1Point0.Tiles;
using CalamityModClassic1Point0.Items.Placeables;

namespace CalamityModClassic1Point0.Tiles
{
	public class ParticleAccelerator : ModTile
	{

		public override void SetStaticDefaults()
		{
			Main.tileSolidTop[Type] = false;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.CoordinateHeights = new int[]{ 18 };
			TileObjectData.addTile(Type);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			//AddMapEntry(new Color(200, 200, 200), "Particle Accelerator");
			AdjTiles = new int[]{ TileID.WorkBenches };
		}
	}
}