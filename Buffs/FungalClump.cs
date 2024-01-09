using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Buffs
{
	public class FungalClump : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Fungal Clump");
			//Description.SetDefault("The fungal clump will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("FungalClump").Type] > 0)
			{
				modPlayer.fClump = true;
			}
			if (!modPlayer.fClump)
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