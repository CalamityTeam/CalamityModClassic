using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalamityModClassicPreTrailer.Tiles.AstralDesert;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;

namespace CalamityModClassicPreTrailer.Tiles
{
    public class AstralPalmTree : ModPalmTree
    {
        public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings
        {
            UseSpecialGroups = true,
            SpecialGroupMinimalHueValue = 11f / 72f,
            SpecialGroupMaximumHueValue = 0.25f,
            SpecialGroupMinimumSaturationValue = 0.88f,
            SpecialGroupMaximumSaturationValue = 1f
        };
        public override void SetStaticDefaults()
        {
            // Grows on astral sand
            GrowsOnTileId = new int[1] { ModContent.TileType<AstralSand>() };
        }
        
        public override Asset<Texture2D> GetTexture() => ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Tiles/Astral/AstralPalmTree");

        public override Asset<Texture2D> GetTopTextures() => ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Tiles/Astral/AstralPalmTree_Tops");
        
        public override Asset<Texture2D> GetOasisTopTextures() => ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Tiles/Astral/AstralPalmTree_OasisTops");
        
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
