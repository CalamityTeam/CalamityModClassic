using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Tiles.Astral
{
	public class AstralStone : ModTile
	{
		public override void SetStaticDefaults()
		{
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileBrick[Type] = true;

            DustType = ModContent.DustType<AstralBasic>();
            HitSound = SoundID.Tink;

            AddMapEntry(new Color(45, 36, 63));

            TileID.Sets.Stone[Type] = true;
            TileID.Sets.Conversion.Stone[Type] = true;
            TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        }
        
        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            CustomTileFraming.FrameTileForCustomMerge(i, j, Type, Mod.Find<ModTile>("AstralDirt").Type);
            return false;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}