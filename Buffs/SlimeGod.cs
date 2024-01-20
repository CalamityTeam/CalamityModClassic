using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class SlimeGod : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Baby Slime God");
			//Description.SetDefault("The slime god will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SlimeGodAlt").Type] > 0)
			{
				modPlayer.sGod = true;
			}
			else if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SlimeGod").Type] > 0)
			{
				modPlayer.sGod = true;
			}
			if (!modPlayer.sGod)
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