using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CalamityModClassicPreTrailer.Tiles.SunkenSea
{
    public class TubeCoral : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new[]
			{
				16,
				16,
				16
			};
			TileObjectData.addTile(Type);
			DustType = 253;
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Tube Coral");
			AddMapEntry(new Color(0, 0, 80));
			MineResist = 3f;

			base.SetStaticDefaults();
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
