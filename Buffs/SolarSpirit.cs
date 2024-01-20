using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class SolarSpirit : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Solar Spirit");
			//Description.SetDefault("The solar spirit will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SolarPixie").Type] > 0)
			{
				modPlayer.SP = true;
			}
			if (!modPlayer.SP)
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