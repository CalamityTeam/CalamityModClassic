using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class CosmicEnergy : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Energy");
			// Description.SetDefault("The cosmic energy will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("CosmicEnergy").Type] > 0)
			{
				modPlayer.cEnergy = true;
			}
			if (!modPlayer.cEnergy)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}