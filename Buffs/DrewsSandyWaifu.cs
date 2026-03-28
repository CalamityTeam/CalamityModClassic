using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class DrewsSandyWaifu : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rare Sand Elemental");
			// Description.SetDefault("The sand elemental will heal you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DrewsSandyWaifu").Type] > 0)
			{
				modPlayer.dWaifu = true;
			}
			if (!modPlayer.dWaifu)
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