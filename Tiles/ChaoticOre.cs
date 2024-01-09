using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Tiles
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
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Chaotic Ore");
 			AddMapEntry(new Color(255, 255, 0));
			MineResist = 4f;
			MinPick = 209;
			HitSound = Terraria.ID.SoundID.Tink;
			Main.tileSpelunker[Type] = true;
		}
		
		public override void RandomUpdate(int i, int j)
		{
			Main.tileOreFinderPriority[Type] = (short)(Main.hardMode ? 750 : 0);
		}
		
		public override bool CanExplode(int i, int j)
		{
			return NPC.downedGolemBoss;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}