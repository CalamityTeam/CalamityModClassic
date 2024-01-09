using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class BrimstoneWaifu : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Brimstone Waifu");
			//Description.SetDefault("The brimstone elemental will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BigBustyRose").Type] > 0)
			{
				modPlayer.bWaifu = true;
			}
			if (!modPlayer.bWaifu)
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