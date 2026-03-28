using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class GuardianOffense : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Offensive Guardian");
			// Description.SetDefault("The attacker will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("MiniGuardianAttack").Type] > 0)
			{
				modPlayer.gOffense = true;
			}
			if (!modPlayer.gOffense)
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