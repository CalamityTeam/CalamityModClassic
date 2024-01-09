using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityModClassic1Point2.Tiles
{
	public class DraedonsForge : ModTile
	{
		public override void SetStaticDefaults()
		{
			AnimationFrameHeight = 36;
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new int[]{ 16, 18 };
			TileObjectData.addTile(Type);
			LocalizedText name = CreateMapEntryName();
 			// name.SetDefault("Draedon's Forge");
 			AddMapEntry(new Color(0, 255, 0));
			TileID.Sets.DisableSmartCursor[Type] = true;
			AdjTiles = new int[]{ TileID.AdamantiteForge, TileID.LunarCraftingStation, TileID.MythrilAnvil };
		}
		
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = (float)Main.DiscoR / 255f;
			g = (float)Main.DiscoG / 255f;
			b = (float)Main.DiscoB / 255f;
		}
		
		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 12)
			{
				frameCounter = 0;
				frame++;
				if (frame > 3)
				{
					frame = 0;
				}
			}
		}
	}
}