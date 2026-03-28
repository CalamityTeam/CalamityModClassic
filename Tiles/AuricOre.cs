using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Tiles
{
	public class AuricOre : ModTile
	{
		public override void SetStaticDefaults()
		{
            Main.tileLighted[Type] = true;
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
            Main.tileOreFinderPriority[Type] = 810;
            DustType = 55;
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Auric Ore");
 			AddMapEntry(new Color(255, 200, 0), name);
			MineResist = 10f;
			MinPick = 274;
			HitSound = SoundID.Tink;
			Main.tileSpelunker[Type] = true;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return CalamityWorldPreTrailer.downedYharon;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.20f;
            g = 0.16f;
            b = 0.00f;
        }
    }
}