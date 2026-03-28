using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class BloodClot : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blood Clot");
			// Description.SetDefault("The blood clot will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BloodClotMinion").Type] > 0)
			{
				modPlayer.bClot = true;
			}
			if (!modPlayer.bClot)
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