using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityModClassicPreTrailer.Tiles.MusicBoxes
{
	public class CultistMusicbox : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[(int)Type] = true;
			Main.tileObsidianKill[(int)Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleLineSkip = 2;
			TileObjectData.addTile((int)Type);
			TileID.Sets.DisableSmartCursor[0] = true;
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Music Box");
			AddMapEntry(new Color(200, 200, 200), name);
		}

		/*public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			string? source = "Source_Tile";
			IEntitySource entity = new Terraria.DataStructures.AEntitySource_Tile(i *16, j * 16,source);
			Item.NewItem(entity, i * 16, j * 16, 16, 48, Mod.Find<ModItem>("CultistMusicbox").Type, 1, false, 0, false, false);
		}*/
		
		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = Mod.Find<ModItem>("CultistMusicbox").Type;
		}
	}
}
