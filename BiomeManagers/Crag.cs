using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.BiomeManagers
{
	public class Crag : ModBiome
	{ 
		public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Crag");
		
		public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
		
		public override string BestiaryIcon => "CalamityModClassicPreTrailer/BiomeManagers/BrimstoneCragsIcon";
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brimstone Crags");
		}
		
		public override bool IsBiomeActive(Player player) => CalamityWorldPreTrailer.calamityTiles > 50;
	}
}