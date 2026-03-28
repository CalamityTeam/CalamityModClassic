using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.Localization;
using Terraria.ObjectData;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace CalamityModClassicPreTrailer.Tiles.SunkenSea
{
    public class SmallBrainCoral : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(Type);
			DustType = 253;
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Small Brain Coral");
			AddMapEntry(new Color(0, 0, 80));
			MineResist = 3f;

			base.SetStaticDefaults();
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (closer)
			{
				if (Main.rand.Next(300) == 0)
				{
					int tileLocationY = j - 1;
					if (Main.tile[i, tileLocationY] != null)
					{
						if (!Main.tile[i, tileLocationY].HasTile)
						{
							if (Main.tile[i, tileLocationY].LiquidAmount == 255 && Main.tile[i, tileLocationY - 1].LiquidAmount == 255 && Main.tile[i, tileLocationY - 2].LiquidAmount == 255)
							{
								if (Main.netMode != 1)
									Projectile.NewProjectile(new EntitySource_WorldEvent(), (float)(i * 16 + 16), (float)(tileLocationY * 16 + 16), 0f, -0.1f, Mod.Find<ModProjectile>("CoralBubbleSmall").Type, 0, 1f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				}
			}
		}
	}
}
