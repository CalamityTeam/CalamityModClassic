using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Tiles.AstralDesert
{
	public class AstralSand : ModTile
	{
		public override void SetStaticDefaults()
		{
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileSand[Type] = true;
            Main.tileBrick[Type] = true;

            DustType = 108;
			
            AddMapEntry(new Color(149, 156, 155));

            TileID.Sets.Suffocate[Type] = true;
            TileID.Sets.Conversion.Sand[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            if (j < Main.maxTilesY && !Main.tile[i, j + 1].HasTile)
            {
                Main.tile[i, j].Get<TileWallWireStateData>().HasTile = false;
                Projectile.NewProjectile(new EntitySource_TileBreak(i, j), new Vector2(i * 16f + 8f, j * 16f + 8f), Vector2.Zero, Mod.Find<ModProjectile>("AstralFallingSand").Type, 15, 0f);
                WorldGen.SquareTileFrame(i, j);
                return false;
            }
            CustomTileFraming.FrameTileForCustomMerge(i, j, Type, Mod.Find<ModTile>("AstralDirt").Type);
            return false;
        }

        public override bool HasWalkDust()
        {
            return Main.rand.Next(3) == 0;
        }

        public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color)
        {
            dustType = 108;
        }
    }

    public class AstralCactus : ModCactus
    {
        public override void SetStaticDefaults()
        {
            // Grows on astral sand
            GrowsOnTileId = new int[1] { ModContent.TileType<AstralSand>() };
        }
        public override Asset<Texture2D> GetTexture() => ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/Tiles/AstralCactus");

        public override Asset<Texture2D> GetFruitTexture() => null;
    }
}