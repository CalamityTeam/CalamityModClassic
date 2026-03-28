using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Tiles
{
	public class CharredOre : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
			Main.tileOreFinderPriority[Type] = 710;
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Charred Ore");
 			AddMapEntry(new Color(128, 0, 0), name);
			MineResist = 6f;
			MinPick = 199;
            HitSound = SoundID.Tink;
            DustType = 235;
            Main.tileSpelunker[Type] = true;
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