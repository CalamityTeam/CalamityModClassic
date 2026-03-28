using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Walls
{
    public class AstralStoneWall : ModWall
    {
        /*
        public override bool IsLoadingEnabled(Mod mod)
        {
            Mod.AddWall("AstralStoneWallUnsafe", this, texture);
            return base.IsLoadingEnabled(ref name, ref texture);
        }
        */
        public override void SetStaticDefaults()
        {
            DustType = DustID.Shadowflame; //TODO
            WallID.Sets.Conversion.Stone[Type] = true;

            AddMapEntry(new Color(15, 26, 31));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
