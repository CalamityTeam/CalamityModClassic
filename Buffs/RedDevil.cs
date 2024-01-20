using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class RedDevil : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Red Devil");
			//Description.SetDefault("The red devil will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("RedDevil").Type] > 0)
			{
				modPlayer.rDevil = true;
			}
			if (!modPlayer.rDevil)
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