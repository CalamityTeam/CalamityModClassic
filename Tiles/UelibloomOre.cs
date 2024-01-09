using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Tiles
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
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Uelibloom Ore");
 			AddMapEntry(new Color(0, 255, 0));
			MineResist = 5f;
			MinPick = 249;
			HitSound = Terraria.ID.SoundID.Tink;
			Main.tileSpelunker[Type] = true;
		}
		
		public override void RandomUpdate(int i, int j)
		{
			Main.tileOreFinderPriority[Type] = (short)(Main.hardMode ? 805 : 0);
		}
		
		public override bool CanExplode(int i, int j)
		{
			return NPC.downedMoonlord;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}