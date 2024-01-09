using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Tiles
{
	public class CryonicOre : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			DustType = ModContent.DustType<Dusts.MSparkle>();
			RegisterItemDrop(ModContent.ItemType<Items.CryonicOre>());
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Cryonic Ore");
 			AddMapEntry(new Color(0, 0, 150));
			MineResist = 3f;
			MinPick = 179;
			HitSound = Terraria.ID.SoundID.Tink;
			Main.tileSpelunker[Type] = true;
		}
		
		public override void RandomUpdate(int i, int j)
		{
			Main.tileOreFinderPriority[Type] = (short)(Main.hardMode ? 675 : 0);
		}
		
		public override bool CanExplode(int i, int j)
		{
			return CalamityWorld.downedCryogen;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}