using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Walls
{
    public class EutrophicSandWallSafe : ModWall
    {
        public override void SetStaticDefaults()
        {
			Main.wallHouse[Type] = true;
			DustType = 108; 
            LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Eutrophic Sand Wall Safe");
			AddMapEntry(new Color(100, 100, 150), name);
		}

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}