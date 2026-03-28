using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class SilvaCrystal : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Silva Crystal");
			// Description.SetDefault("The crystal will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SilvaCrystal").Type] > 0)
			{
				modPlayer.sCrystal = true;
			}
			if (!modPlayer.sCrystal)
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