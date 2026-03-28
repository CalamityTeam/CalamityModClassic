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
    public class ExodiumOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Exodium Ore");
            AddMapEntry(new Color(51, 48, 68), name);
			MineResist = 5f;
			MinPick = 224;
			HitSound = SoundID.Tink;
			Main.tileOreFinderPriority[Type] = 760;
            Main.tileSpelunker[Type] = true;
            base.SetStaticDefaults();
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 2 : 4;
        }
    }
}
