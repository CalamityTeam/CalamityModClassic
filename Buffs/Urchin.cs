using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class Urchin : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Urchin");
			// Description.SetDefault("The urchin will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Urchin").Type] > 0)
			{
				modPlayer.vUrchin = true;
			}
			if (!modPlayer.vUrchin)
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