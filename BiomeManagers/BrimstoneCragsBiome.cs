using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.BiomeManagers
{
    public class BrimstoneCragsBiome : ModBiome
    {
        public override int Music => MusicID.Eerie;
        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
        public override string BestiaryIcon => "CalamityModClassic1Point2/BiomeManagers/BrimstoneCragsIcon";
        public override string BackgroundPath => "Terraria/Images/MapBG3";

        public override bool IsBiomeActive(Player player)
        {
            return CalamityWorld.calamityTiles > 50;
        }
        public override void OnInBiome(Player player)
        {
            player.GetModPlayer<CalamityPlayer>().ZoneCalamity = true;
        }
        public override void OnLeave(Player player)
        {
            player.GetModPlayer<CalamityPlayer>().ZoneCalamity = false;
        }
    }
}
