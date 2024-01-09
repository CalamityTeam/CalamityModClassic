using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class DaedalusCrystal : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Daedalus Crystal");
			//Description.SetDefault("The daedalus crystal will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DaedalusCrystal").Type] > 0)
			{
				modPlayer.dCrystal = true;
			}
			if (!modPlayer.dCrystal)
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