using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using CalamityModClassic1Point1.Items.Placeables;

namespace CalamityModClassic1Point1.Tiles
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
			TileID.Sets.DisableSmartCursor[Type]/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
			AddMapEntry(new Color(120, 85, 60));
			RegisterItemDrop(0);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			int item = 0;
			switch (frameX / 54)
			{
				case 0:
					item = ModContent.ItemType<DesertScourgeTrophy>();
					break;
				case 1:
					item = ModContent.ItemType<HiveMindTrophy>();
					break;
				case 2:
					item = ModContent.ItemType<PerforatorTrophy>();
					break;
				case 3:
					item = ModContent.ItemType<SlimeGodTrophy>();
					break;
				case 4:
					item = ModContent.ItemType<CryogenTrophy>();
					break;
				case 5:
					item = ModContent.ItemType<CalamitasTrophy>();
					break;
				case 6:
					item = ModContent.ItemType<DevourerofGodsTrophy>();
					break;
				case 7:
					item = ModContent.ItemType<PlaguebringerGoliathTrophy>();
					break;
				case 8:
					item = ModContent.ItemType<YharonTrophy>();
					break;
				case 9:
					item = ModContent.ItemType<LeviathanTrophy>();
					break;
			}
			if (item > 0)
			{
				Item.NewItem(new EntitySource_WorldGen(), i * 16, j * 16, 48, 48, item);
			}
		}
	}
}