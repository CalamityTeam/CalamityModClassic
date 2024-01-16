using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Tiles
{
	public class BrimstoneCragRock : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			RegisterItemDrop(ModContent.ItemType<Items.CharredOre>());
			MineResist = 6f;
			MinPick = 199;
            HitSound = SoundID.Tink;
            DustType = 54;
			AddMapEntry(new Color(128, 0, 0));
		}
		
		public override bool CanExplode(int i, int j)
		{
			if (Main.tile[i, j].TileType == Mod.Find<ModTile>("BrimstoneCragRock").Type)
			{
				return false;
			}
			return false;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 1.00f;
			g = 0.00f;
			b = 0.00f;
		}
    }
}