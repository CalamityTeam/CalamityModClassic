using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Buffs
{
	public class BrittleStar : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("Brittle Star");
			//Description.SetDefault("The brittle star will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer1Point1 modPlayer = player.GetModPlayer<CalamityPlayer1Point1>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BrittleStar").Type] > 0)
			{
				modPlayer.bStar = true;
			}
			if (!modPlayer.bStar)
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