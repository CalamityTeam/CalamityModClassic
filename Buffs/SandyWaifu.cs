using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class SandyWaifu : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Sandy Waifu");
			//Description.SetDefault("The sand elemental will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SandyWaifu").Type] > 0)
			{
				modPlayer.sWaifu = true;
			}
			if (!modPlayer.sWaifu)
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