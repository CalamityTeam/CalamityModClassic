using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Walls
{
    public class BrimstoneSlagWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true; 
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Brimstone Slag Wall");
            AddMapEntry(new Color(20, 20, 20), name);
            DustType = 53;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}