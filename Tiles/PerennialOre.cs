using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Tiles
{
	public class PerennialOre : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			DustType = ModContent.DustType<Dusts.CESparkle>();
			RegisterItemDrop(ModContent.ItemType<Items.PerennialOre>());
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Perennial Ore");
 			AddMapEntry(new Color(255, 51, 204));
			MineResist = 3f;
			MinPick = 199;
			HitSound = Terraria.ID.SoundID.Tink;
			Main.tileSpelunker[Type] = true;
		}
		
		public override void RandomUpdate(int i, int j)
		{
			Main.tileOreFinderPriority[Type] = (short)(Main.hardMode ? 690 : 0);
		}
		
		public override bool CanExplode(int i, int j)
		{
			return NPC.downedPlantBoss;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}