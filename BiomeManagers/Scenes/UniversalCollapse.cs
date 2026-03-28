using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace CalamityModClassicPreTrailer.BiomeManagers.Scenes
{
	public class UniversalCollapse : ModSceneEffect
	{
 	    public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/UniversalCollapse");
		public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

		public virtual bool SetSceneEffect(Player player) => CalamityWorldPreTrailer.DoGSecondStageCountdown <= 540 &&
		                                                     CalamityWorldPreTrailer.DoGSecondStageCountdown > 6;
		public override bool IsSceneEffectActive(Player player) => SetSceneEffect(player);
	}
}