using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Tiles
{
	public class UelibloomOre : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			DustType = ModContent.DustType<Dusts.TCESparkle>();
			RegisterItemDrop(ModContent.ItemType<Items.UelibloomOre>());
			AddMapEntry(new Color(0, 255, 0));
			MineResist = 5f;
			MinPick = 209;
			HitSound = SoundID.Tink;
			Main.tileSpelunker[Type] = true;
		}
		
		public override bool CanExplode(int i, int j)
		{
			if (Main.tile[i, j].TileType == Mod.Find<ModTile>("UelibloomOre").Type)
			{
				return false;
			}
			return false;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}