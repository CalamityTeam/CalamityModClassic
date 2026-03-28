using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Tiles
{
	public class BrimstoneSlag : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			MineResist = 6f;
			MinPick = 179;
			DustType = 53;
			HitSound = SoundID.Tink;
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Brimstone Slag");
 			AddMapEntry(new Color(20, 20, 20), name);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return false;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.50f;
			g = 0.00f;
			b = 0.00f;
		}
    }
}