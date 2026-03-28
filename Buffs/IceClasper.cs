using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class IceClasper : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ice Clasper");
			// Description.SetDefault("The ice clasper will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("IceClasper").Type] > 0)
			{
				modPlayer.iClasper = true;
			}
			if (!modPlayer.iClasper)
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