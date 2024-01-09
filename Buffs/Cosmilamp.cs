using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class Cosmilamp : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cosmilamp");
			//Description.SetDefault("The cosmilamp will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Cosmilamp").Type] > 0)
			{
				modPlayer.cLamp = true;
			}
			if (!modPlayer.cLamp)
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