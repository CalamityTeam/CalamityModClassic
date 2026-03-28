using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class GuardianDefense : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Defensive Guardian");
			// Description.SetDefault("The defender will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("MiniGuardianDefense").Type] > 0)
			{
				modPlayer.gDefense = true;
			}
			if (!modPlayer.gDefense)
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