using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Waters
{
    public class SunkenSeaWater : ModWaterStyle
    {
        public override int ChooseWaterfallStyle() => ModContent.Find<ModWaterfallStyle>("CalamityModClassicPreTrailer/AstralWaterflow").Slot;

        public override int GetSplashDust() => 33;

        public override int GetDropletGore() => 713;

        public override Color BiomeHairColor() => Color.Blue;
    }
}