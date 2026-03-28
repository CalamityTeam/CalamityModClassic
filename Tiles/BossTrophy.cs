using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityModClassicPreTrailer.Tiles
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
			TileID.Sets.DisableSmartCursor[Type] = true;
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Trophy");
 			AddMapEntry(new Color(120, 85, 60), name);
		}

		/*public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			int item = 0;
			switch (frameX / 54)
			{
				case 0:
					item = Mod.Find<ModItem>("DesertScourgeTrophy").Type;
					break;
				case 1:
					item = Mod.Find<ModItem>("PerforatorTrophy").Type;
					break;
				case 2:
					item = Mod.Find<ModItem>("SlimeGodTrophy").Type;
					break;
				case 3:
					item = Mod.Find<ModItem>("CryogenTrophy").Type;
					break;
				case 4:
					item = Mod.Find<ModItem>("PlaguebringerGoliathTrophy").Type;
					break;
				case 5:
					item = Mod.Find<ModItem>("LeviathanTrophy").Type;
					break;
				case 6:
					item = Mod.Find<ModItem>("ProvidenceTrophy").Type;
					break;
				case 7:
					item = Mod.Find<ModItem>("CalamitasTrophy").Type;
					break;
				case 8:
					item = Mod.Find<ModItem>("HiveMindTrophy").Type;
					break;
				case 9:
					item = Mod.Find<ModItem>("CrabulonTrophy").Type;
					break;
				case 10:
					item = Mod.Find<ModItem>("YharonTrophy").Type;
					break;
				case 11:
					item = Mod.Find<ModItem>("SignusTrophy").Type;
					break;
				case 12:
					item = Mod.Find<ModItem>("WeaverTrophy").Type;
					break;
				case 13:
					item = Mod.Find<ModItem>("CeaselessVoidTrophy").Type;
					break;
				case 14:
					item = Mod.Find<ModItem>("DevourerofGodsTrophy").Type;
					break;
				case 15:
					item = Mod.Find<ModItem>("CatastropheTrophy").Type;
					break;
				case 16:
					item = Mod.Find<ModItem>("CataclysmTrophy").Type;
					break;
                case 17:
                    item = Mod.Find<ModItem>("PolterghastTrophy").Type;
                    break;
                case 18:
                    item = Mod.Find<ModItem>("BumblebirbTrophy").Type;
                    break;
                case 19:
                    item = Mod.Find<ModItem>("AstrageldonTrophy").Type;
                    break;
                case 20:
                    item = Mod.Find<ModItem>("AstrumDeusTrophy").Type;
                    break;
                case 21:
                    item = Mod.Find<ModItem>("BrimstoneElementalTrophy").Type;
                    break;
                case 22:
                    item = Mod.Find<ModItem>("RavagerTrophy").Type;
                    break;
            }
			if (item > 0)
			{
				Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 48, item);
			}
		}*/
	}
}