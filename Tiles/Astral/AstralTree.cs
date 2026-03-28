using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalamityModClassicPreTrailer.Tiles.Astral;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;

namespace CalamityModClassicPreTrailer.Tiles
{
    public class AstralTree : ModTree
    {
        public override void SetStaticDefaults()
        {
            // Grows on astral grass
            GrowsOnTileId = new int[1] { ModContent.TileType<AstralGrass>() };
        }
        
        public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings
        {
            UseSpecialGroups = true,
            SpecialGroupMinimalHueValue = 11f / 72f,
            SpecialGroupMaximumHueValue = 0.25f,
            SpecialGroupMinimumSaturationValue = 0.88f,
            SpecialGroupMaximumSaturationValue = 1f
        };

        public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY,
            ref int topTextureFrameWidth, ref int topTextureFrameHeight)
        {
            // me when i declare a method just to fill in the class type's requirements trollface.png
        }

        public override Asset<Texture2D> GetTopTextures() => ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Tiles/Astral/AstralTree_Tops");

        public override Asset<Texture2D> GetBranchTextures() => ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Tiles/Astral/AstralTree_Branches");
        

        public override Asset<Texture2D> GetTexture() =>  ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Tiles/Astral/AstralTree");

        public override int DropWood()
        {
            return CalamityModClassicPreTrailer.Instance.Find<ModItem>("AstralMonolith").Type;
        }

        public override int CreateDust()
        {
            return CalamityModClassicPreTrailer.Instance.Find<ModDust>("AstralBasic").Type;
        }

        public override int TreeLeaf()
        {
            return -1;
        }
    }
}
