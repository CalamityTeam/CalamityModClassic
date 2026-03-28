using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Walls
{
    public class NavystoneWallSafe : ModWall
    {
		public override void SetStaticDefaults()
        {
			Main.wallHouse[Type] = true;
			DustType = 96;
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Navystone Wall Safe");
			AddMapEntry(new Color(0, 50, 50), name);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}