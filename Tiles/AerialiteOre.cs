using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Tiles
{
	public class AerialiteOre : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileOreFinderPriority[Type] = 450;
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Aerialite Ore");
 			AddMapEntry(new Color(0, 255, 255), name);
			MineResist = 2f;
			MinPick = 64;
			HitSound = SoundID.Tink;
			Main.tileSpelunker[Type] = true;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return NPC.downedBoss2;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}