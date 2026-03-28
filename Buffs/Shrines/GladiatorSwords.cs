using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs.Shrines
{
	public class GladiatorSwords : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Gladiator Swords");
			// Description.SetDefault("The gladiator swords will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("GladiatorSword").Type] > 0)
			{
				modPlayer.glSword = true;
			}
			if (!modPlayer.glSword)
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