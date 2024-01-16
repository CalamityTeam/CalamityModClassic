using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Buffs
{
	public class CloudyWaifu : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("Cloudy Waifu");
			//Description.SetDefault("The cloud elemental will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("CloudyWaifu").Type] > 0)
			{
				modPlayer.cWaifu = true;
			}
			if (!modPlayer.cWaifu)
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