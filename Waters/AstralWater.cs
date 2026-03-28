using CalamityModClassicPreTrailer.Gores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
namespace CalamityModClassicPreTrailer.Waters
{
    public class AstralWater : ModWaterStyle
    {

        public override int ChooseWaterfallStyle() => ModContent.Find<ModWaterfallStyle>("CalamityModClassicPreTrailer/AstralWaterflow").Slot;
        public override int GetSplashDust() => 52;
        public override int GetDropletGore() => ModContent.GoreType<AstralWaterDroplet>();
        public override Color BiomeHairColor() => Color.MediumPurple;
    }
}