using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class Valkyrie : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Valkyrie");
			//Description.SetDefault("The valkyrie will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Valkyrie").Type] > 0)
			{
				modPlayer.aValkyrie = true;
			}
			if (!modPlayer.aValkyrie)
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