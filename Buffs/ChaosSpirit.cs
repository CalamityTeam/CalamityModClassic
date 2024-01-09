using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class ChaosSpirit : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Chaos Spirit");
			//Description.SetDefault("The chaos spirit will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ChaosSpirit").Type] > 0)
			{
				modPlayer.cSpirit = true;
			}
			if (!modPlayer.cSpirit)
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