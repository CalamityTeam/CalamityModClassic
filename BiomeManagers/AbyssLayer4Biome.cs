using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.BiomeManagers
{
    public class AbyssLayer4Biome : ModBiome
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TheVoid");
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override string BestiaryIcon => "CalamityModClassicPreTrailer/BiomeManagers/AbyssIcon";
        public override string BackgroundPath => "CalamityModClassicPreTrailer/Backgrounds/MapBackgrounds/AbyssMap4";
        public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("CalamityModClassicPreTrailer/AbyssWater");
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fourth Layer of the Abyss");
        }
        public bool MeetsBaseAbyssRequirement(Player player)
        {
            Point point = player.Center.ToTileCoordinates();
            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            int genLimit = x / 2;
            int abyssChasmY = y - 250;
            int abyssChasmX = (CalamityWorldPreTrailer.abyssSide ? genLimit - (genLimit - 135) : genLimit + (genLimit - 135));
            bool abyssPosX = false;
            bool abyssPosY = (point.Y <= abyssChasmY);
            if (CalamityWorldPreTrailer.abyssSide)
            {
                if (point.X < abyssChasmX + 80)
                {
                    abyssPosX = true;
                }
            }
            else
            {
                if (point.X > abyssChasmX - 80)
                {
                    abyssPosX = true;
                }
            }

            return (((double)point.Y > (Main.rockLayer - (double)y * 0.05)) &&
                    !player.lavaWet &&
                    !player.honeyWet &&
                    abyssPosY &&
                    abyssPosX);
        }

        public override bool IsBiomeActive(Player player)
        {
            Point point = player.Center.ToTileCoordinates();
            int y = Main.maxTilesY;
            return MeetsBaseAbyssRequirement(player) && (double)point.Y > (Main.rockLayer + (double)y * 0.26);
        }
    }
}