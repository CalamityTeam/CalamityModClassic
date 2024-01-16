using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Tiles
{
	public class ChaoticOre : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			DustType = ModContent.DustType<Dusts.DWSparkle>();
			RegisterItemDrop(ModContent.ItemType<Items.ChaoticOre>());
			AddMapEntry(new Color(255, 255, 0));
			MineResist = 4f;
			MinPick = 199;
			HitSound = SoundID.Tink;
			Main.tileSpelunker[Type] = true;
		}
		
		public override bool CanExplode(int i, int j)
		{
			if (Main.tile[i, j].TileType == Mod.Find<ModTile>("ChaoticOre").Type)
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