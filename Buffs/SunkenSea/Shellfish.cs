using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs.SunkenSea
{
	public class Shellfish : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shellfish");
			// Description.SetDefault("The shellfish will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Shellfish").Type] > 0)
			{
				modPlayer.shellfish = true;
			}
			if (!modPlayer.shellfish)
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