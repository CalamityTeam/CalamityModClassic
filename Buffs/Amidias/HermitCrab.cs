using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs.Amidias
{
	public class HermitCrab : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hermit Crab");
			// Description.SetDefault("The hermit crab will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("HermitCrab").Type] > 0)
			{
				modPlayer.hCrab = true;
			}
			if (!modPlayer.hCrab)
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