using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Buffs
{
	public class Corroslime : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("Corro slime");
			//Description.SetDefault("The corro slime will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer1Point1 modPlayer = player.GetModPlayer<CalamityPlayer1Point1>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Corroslime").Type] > 0)
			{
				modPlayer.cSlime = true;
			}
			if (!modPlayer.cSlime)
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