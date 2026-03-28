using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Waters
{
    public class AbyssWater : ModWaterStyle
    {
        public override int ChooseWaterfallStyle() => ModContent.Find<ModWaterfallStyle>("CalamityModClassicPreTrailer/AbyssWaterflow").Slot;

        public override int GetSplashDust() => 33;

        public override int GetDropletGore() => 713;

        public override Color BiomeHairColor() => Color.Blue;
    }
}