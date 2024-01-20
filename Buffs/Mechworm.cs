using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Buffs
{
	public class Mechworm : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("Mechworm");
			//Description.SetDefault("The mechworm will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer1Point1 modPlayer = player.GetModPlayer<CalamityPlayer1Point1>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("MechwormHead").Type] > 0)
			{
				modPlayer.mWorm = true;
			}
			if (!modPlayer.mWorm)
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