using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Tiles.SunkenSea
{
	public class EutrophicSand : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			DustType = 108;
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Eutrophic Sand");
 			AddMapEntry(new Color(100, 100, 150), name);
			MineResist = 2f;
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return CalamityWorldPreTrailer.downedDesertScourge;
		}

		public override bool CanExplode(int i, int j)
		{
			return CalamityWorldPreTrailer.downedDesertScourge;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}