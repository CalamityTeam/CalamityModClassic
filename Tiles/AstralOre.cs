using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Tiles
{
	public class AstralOre : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileOreFinderPriority[Type] = 700;
			MinPick = 199;
			DustType = 173;
			RegisterItemDrop(ModContent.ItemType<Items.AstralOre>());
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Astral Ore");
 			AddMapEntry(new Color(255, 153, 255));
			MineResist = 5f;
			HitSound = Terraria.ID.SoundID.Tink;
			Main.tileSpelunker[Type] = true;
		}
		
		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return CalamityWorld.downedStarGod;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return CalamityWorld.downedStarGod;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}