using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Tiles
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
			MinPick = 199;
			DustType = 53;
			HitSound = Terraria.ID.SoundID.Tink;
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Brimstone Slag");
 			AddMapEntry(new Color(20, 20, 20));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
		
		public override bool CanExplode(int i, int j)
		{
			if (Main.tile[i, j].TileType == ModContent.TileType<BrimstoneSlag>())
			{
				return false;
			}
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