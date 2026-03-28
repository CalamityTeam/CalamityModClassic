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
	public class YellowCandle : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Spiteful Candle");
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
			{
				player.AddBuff(Mod.Find<ModBuff>("YellowDamageCandle").Type, 20);
				if (Main.netMode != 1)
                {
                    for (int m = 0; m < 200; m++)
					{
						if (Main.npc[m].active && !Main.npc[m].friendly)
						{
							Main.npc[m].buffImmune[Mod.Find<ModBuff>("YellowDamageCandle").Type] = false;
							if (Main.npc[m].type == Mod.Find<ModNPC>("CeaselessVoid").Type || Main.npc[m].type == Mod.Find<ModNPC>("EidolonWyrmHeadHuge").Type)
							{
								Main.npc[m].buffImmune[Mod.Find<ModBuff>("YellowDamageCandle").Type] = true;
							}
							Main.npc[m].AddBuff(Mod.Find<ModBuff>("YellowDamageCandle").Type, 20, false);
						}
					}
				}
			}
		}
		
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
			r = 0.75f;
            g = 0.75f;
			b = 0.35f;
        }
		
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 32, Mod.Find<ModItem>("YellowCandle").Type);
        }
    }
}
