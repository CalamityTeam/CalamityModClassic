using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Waters
{
    public class SulphuricWater : ModWaterStyle
    {
        
        public override int ChooseWaterfallStyle() => ModContent.Find<ModWaterfallStyle>("CalamityModClassicPreTrailer/SulphuricWaterflow").Slot;
        public override int GetSplashDust() => 102;
        public override int GetDropletGore() => 711;
        public override Color BiomeHairColor() => Color.Yellow;
    }
}