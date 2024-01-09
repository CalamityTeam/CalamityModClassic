using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class Mechworm : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Mechworm");
			//Description.SetDefault("The mechworm will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
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