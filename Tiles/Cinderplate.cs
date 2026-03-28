using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Tiles
{
    public class Cinderplate : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
			Main.tileOreFinderPriority[Type] = 750;

			LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Cinderplate");
            AddMapEntry(new Color(97, 22, 57), name);

            base.SetStaticDefaults();
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 2 : 4;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.19f;
            g = 0.043f;
            b = 0.111f;
        }
    }
}
