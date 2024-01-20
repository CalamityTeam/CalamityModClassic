using CalamityModClassic1Point2;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.BiomeManagers
{
	// Shows setting up two basic biomes. For a more complicated example, please request.
	public class AstralMeteorBiome : ModBiome
    {
        public override int Music => MusicID.Space;
        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
        public override string BestiaryIcon => "CalamityModClassic1Point2/BiomeManagers/AstralMeteorBiomeIcon";
        public override string BackgroundPath => "CalamityModClassic1Point2/BiomeManagers/AstralMap";

        public override bool IsBiomeActive(Player player)
        {
            return CalamityWorld1Point2.astralTiles > 50;
        }
        public override void OnInBiome(Player player)
        {
            player.GetModPlayer<CalamityPlayer1Point2>().ZoneAstral = true;
        }
        public override void OnLeave(Player player)
        {
            player.GetModPlayer<CalamityPlayer1Point2>().ZoneAstral = false;
        }
    }
}