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
    public class AstralDirtWall : ModWall
    {   
        /*
        public override bool IsLoadingEnabled(Mod mod)
        {
            Mod.AddWall("AstralDirtWallUnsafe", this, texture);
            return base.IsLoadingEnabled(ref name, ref texture);
        }
        */
        public override void SetStaticDefaults()
        {
            DustType = DustID.Shadowflame; //TODO
            AddMapEntry(new Color(26, 22, 32));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
