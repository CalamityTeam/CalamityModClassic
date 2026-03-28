using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class ElementalAxe : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Elemental Axe");
			// Description.SetDefault("The elemental axe will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ElementalAxe").Type] > 0)
			{
				modPlayer.eAxe = true;
			}
			if (!modPlayer.eAxe)
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