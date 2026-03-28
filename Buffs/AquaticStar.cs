using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class AquaticStar : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aquatic Star");
			// Description.SetDefault("The aquatic star will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("AquaticStar").Type] > 0)
			{
				modPlayer.aStar = true;
			}
			if (!modPlayer.aStar)
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