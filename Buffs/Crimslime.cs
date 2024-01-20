using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Buffs
{
	public class Crimslime : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("Crim slime");
			//Description.SetDefault("The crim slime will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer1Point1 modPlayer = player.GetModPlayer<CalamityPlayer1Point1>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Crimslime").Type] > 0)
			{
				modPlayer.cSlime2 = true;
			}
			if (!modPlayer.cSlime2)
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