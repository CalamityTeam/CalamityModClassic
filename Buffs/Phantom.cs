using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class Phantom : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Phantom");
			//Description.SetDefault("The phantom will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("PhantomGuy").Type] > 0)
			{
				modPlayer.pGuy = true;
			}
			if (!modPlayer.pGuy)
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