using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Tiles
{
	public class CryonicBrick : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			HitSound = SoundID.Tink;
			MinPick = 100;
			AddMapEntry(new Color(99, 131, 199));
			AnimationFrameHeight = 270;
		}

		public override bool CreateDust(int i, int j, ref int type)
		{
			Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, 176, 0f, 0f, 1, new Color(255, 255, 255), 1f);
			return false;
		}

		public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
		{
			int xPos = i % 2;
			int yPos = j % 4;
			frameXOffset = xPos * 288;
			frameYOffset = yPos * AnimationFrameHeight;
		}
	}
}