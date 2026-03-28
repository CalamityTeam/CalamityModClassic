using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class GuardianHealer : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Healer Guardian");
			// Description.SetDefault("The guardian will heal you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("MiniGuardianHealer").Type] > 0)
			{
				modPlayer.gHealer = true;
			}
			if (!modPlayer.gHealer)
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