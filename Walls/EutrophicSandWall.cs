using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Walls
{
    public class EutrophicSandWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            DustType = 108;
        }

		public override void RandomUpdate(int i, int j)
		{
			if (Main.tile[i, j].LiquidAmount <= 0)
			{
				Main.tile[i, j].LiquidAmount = 255;
				
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}