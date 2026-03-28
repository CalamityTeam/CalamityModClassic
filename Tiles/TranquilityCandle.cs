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
    public class TranquilityCandle : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
            TileObjectData.addTile(Type);
	        LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Tranquility Candle");
            AddMapEntry(new Color(238, 145, 105), name);
            AnimationFrameHeight = 20;
        }
		
		public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter >= 4)
            {
                frame = (frame + 1) % 10;
                frameCounter = 0;
            }
        }
		
		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = Mod.Find<ModItem>("TranquilityCandle").Type;
		}
		
		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			if (!player.dead && player.active)
				player.AddBuff(Mod.Find<ModBuff>("TranquilityCandle").Type, 20);
		}
		
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
			r = 0.55f;
            g = 0.055f;
			b = 0.55f;
        }
		
		/*public override void RightClick(int i, int j)
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 8, 8, mod.ItemType("TranquilityCandle"));
			?? KillTile( i, j);
		}*/
    }
}