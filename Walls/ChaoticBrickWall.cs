using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Walls
{
    public class ChaoticBrickWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Chaotic Brick Wall");
            AddMapEntry(new Color(255, 0, 0), name);
            DustType = 105;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 2;
        }
    }
}