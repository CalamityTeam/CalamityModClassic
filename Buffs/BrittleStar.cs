using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class BrittleStar : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brittle Star");
			// Description.SetDefault("The brittle star will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BrittleStar").Type] > 0)
			{
				modPlayer.bStar = true;
			}
			if (!modPlayer.bStar)
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