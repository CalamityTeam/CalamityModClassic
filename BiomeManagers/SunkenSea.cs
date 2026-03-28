using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.BiomeManagers
{
	public class SunkenSea : ModBiome
	{ 
		public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/SunkenSea");
		public override string BestiaryIcon => "CalamityModClassicPreTrailer/BiomeManagers/SunkenSeaIcon";
		public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
		public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("CalamityModClassicPreTrailer/SunkenSeaWater");
		public override string BackgroundPath => "CalamityModClassicPreTrailer/Backgrounds/MapBackgrounds/AbyssMap1";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sunken Sea");
		}
		public override bool IsBiomeActive(Player player)
		{
		bool inSunkenSea = false;
		int playerPosX = (int)player.Center.X / 16;
		int playerPosY = (int)player.Center.Y / 16;
		Tile tile = Framing.GetTileSafely(playerPosX, playerPosY);
			if (tile.WallType == Mod.Find<ModWall>("EutrophicSandWall").Type || tile.WallType == Mod.Find<ModWall>("NavystoneWall").Type)
		{
			inSunkenSea = true;
		}
		return CalamityWorldPreTrailer.sunkenSeaTiles > 300 || inSunkenSea;
		}
	}
}