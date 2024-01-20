using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class DrewsSandyWaifu : ModBuff
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
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DrewsSandyWaifu").Type] > 0)
			{
				modPlayer.dWaifu = true;
			}
			if (!modPlayer.dWaifu)
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