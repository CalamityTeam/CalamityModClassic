using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityModClassicPreTrailer.Tiles
{
	public class PinkCandle : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.addTile(Type);
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Vigorous Candle");
			AddMapEntry(new Color(238, 145, 105), name);
			AnimationFrameHeight = 34;
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter >= 6)
			{
				frame = (frame + 1) % 6;
				frameCounter = 0;
			}
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			if (!player.dead && player.active)
				player.AddBuff(Mod.Find<ModBuff>("PinkHealthCandle").Type, 20);
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.75f;
			g = 0.35f;
			b = 0.65f;
		}

		/*public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 32, Mod.Find<ModItem>("PinkCandle").Type);
		}*/
	}
}
