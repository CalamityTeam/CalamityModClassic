using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class Valkyrie : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Valkyrie");
			// Description.SetDefault("The valkyrie will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
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