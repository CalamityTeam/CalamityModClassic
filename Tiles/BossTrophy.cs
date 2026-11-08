using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using CalamityModClassic1Point2.Items.Placeables;

namespace CalamityModClassic1Point2.Tiles
{
	public class BossTrophy : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);
			DustType = 7;
			Terraria.ID.TileID.Sets.DisableSmartCursor[Type] = true;
			RegisterItemDrop(0);
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Trophy");
 			AddMapEntry(new Color(120, 85, 60), name);
		}
	}
}