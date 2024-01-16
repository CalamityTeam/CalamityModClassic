using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Buffs
{
	public class CalamitasEyes : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("Blighted Eyes");
			//Description.SetDefault("Calamitas and her brothers will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Calamitamini").Type] > 0)
			{
				modPlayer.cEyes = true;
			}
			if (!modPlayer.cEyes)
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