using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class Corroslime : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Corroslime");
			//Description.SetDefault("The corroslime will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Corroslime").Type] > 0)
			{
				modPlayer.cSlime = true;
			}
			if (!modPlayer.cSlime)
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