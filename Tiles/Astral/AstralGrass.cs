using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Reflection;
using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityModClassicPreTrailer.Tiles.Astral
{
	public class AstralGrass : ModTile
	{
		public override void SetStaticDefaults()
		{
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileBrick[Type] = true;

            DustType = ModContent.DustType<AstralBasic>();
 			AddMapEntry(new Color(133, 109, 140));

            TileID.Sets.Grass[Type] = true;
            TileID.Sets.Conversion.Grass[Type] = true;

            //Grass framing (<3 terraria devs)
            TileID.Sets.NeedsGrassFraming[Type] = true;
            TileID.Sets.NeedsGrassFramingDirt[Type] = Mod.Find<ModTile>("AstralDirt").Type;
        }

        public override void NumDust(int i, int j, bool fail, ref int Type)
		{
			Type = fail ? 1 : 3;
		}

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail && !effectOnly)
            {
                Main.tile[i, j].TileType = (ushort)Mod.Find<ModTile>("AstralDirt").Type;
            }
        }
    }
}