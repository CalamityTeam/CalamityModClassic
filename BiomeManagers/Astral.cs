using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace CalamityModClassicPreTrailer.BiomeManagers
{
	public class Astral : ModBiome
	{
		public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Astral");

		public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

		public override ModWaterStyle WaterStyle =>
			ModContent.Find<ModWaterStyle>("CalamityModClassicPreTrailer/AstralWater");

		public override string BestiaryIcon => "CalamityModClassicPreTrailer/BiomeManagers/AstralInfectionIcon";

		public override string MapBackground => "CalamityModClassicPreTrailer/Backgrounds/MapBackgrounds/AstralBG";
		
		public override string BackgroundPath => "CalamityModClassicPreTrailer/Backgrounds/MapBackgrounds/AstralBG";
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Astral Infection");
		}

		public override bool IsBiomeActive(Player player) => !player.ZoneDungeon &&
		                                                     (CalamityWorldPreTrailer.astralTiles > 950 ||
		                                                      (player.ZoneSnow && CalamityWorldPreTrailer.astralTiles >
			                                                      300));
		
		public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle
		{
			get
			{
				if (Main.LocalPlayer.ZoneSnow)
				{
					return ModContent.Find<ModSurfaceBackgroundStyle>(
						"CalamityModClassicPreTrailer/AstralSnowSurfaceBGStyle");
				}
				else if (Main.LocalPlayer.ZoneDesert && !Main.LocalPlayer.ZoneSnow)
				{
					return ModContent.Find<ModSurfaceBackgroundStyle>(
						"CalamityModClassicPreTrailer/AstralDesertSurfaceBGStyle");
				}
				else
				{
					return ModContent.Find<ModSurfaceBackgroundStyle>(
						"CalamityModClassicPreTrailer/AstralSurfaceBGStyle");
				}
			}
		}

		public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle 
		{ 
			get
			{
				if (Main.LocalPlayer.ZoneSnow)
				{
					return ModContent.Find<ModUndergroundBackgroundStyle>("CalamityModClassicPreTrailer/AstralUndergroundBGStyle"); // Could use its own unique background
				}
				return ModContent.Find<ModUndergroundBackgroundStyle>("CalamityModClassicPreTrailer/AstralUndergroundBGStyle");
			}
		}
}
}