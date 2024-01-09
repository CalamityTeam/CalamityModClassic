using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class ReaverOrb : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Reaver Orb");
			//Description.SetDefault("The reaver orb will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ReaverOrb").Type] > 0)
			{
				modPlayer.rOrb = true;
			}
			if (!modPlayer.rOrb)
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