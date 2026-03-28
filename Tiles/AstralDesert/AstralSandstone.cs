using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Tiles.AstralDesert
{
	public class AstralSandstone : ModTile
	{
		public override void SetStaticDefaults()
		{
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileBrick[Type] = true;

            DustType = ModContent.DustType<AstralBasic>();
            AddMapEntry(new Color(79, 61, 97));
            
            TileID.Sets.Conversion.Sandstone[Type] = true;
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            CustomTileFraming.FrameTileForCustomMerge(i, j, Type, Mod.Find<ModTile>("HardenedAstralSand").Type, false, false, false, false, resetFrame);
            return false;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}