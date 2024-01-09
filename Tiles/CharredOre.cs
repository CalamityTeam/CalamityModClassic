using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Tiles
{
	public class CharredOre : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			RegisterItemDrop(ModContent.ItemType<Items.CharredOre>());
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Charred Ore");
 			AddMapEntry(new Color(128, 0, 0));
			MineResist = 6f;
			MinPick = 199;
            HitSound = Terraria.ID.SoundID.Tink;
            DustType = 235;
            Main.tileSpelunker[Type] = true;
		}
		
		public override void RandomUpdate(int i, int j)
		{
			Main.tileOreFinderPriority[Type] = (short)(Main.hardMode ? 710 : 0);
		}
		
		public override bool CanExplode(int i, int j)
		{
			return NPC.downedPlantBoss;
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