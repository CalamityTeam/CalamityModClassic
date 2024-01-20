using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class SirenLure : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Siren Lure");
			//Description.SetDefault("The siren lure will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SirenLure").Type] > 0)
			{
				modPlayer.slWaifu = true;
			}
			if (!modPlayer.slWaifu)
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