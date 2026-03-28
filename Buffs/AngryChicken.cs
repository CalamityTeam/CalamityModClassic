using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class AngryChicken : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Son of Yharon");
			// Description.SetDefault("The Son of Yharon will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("AngryChicken").Type] > 0)
			{
				modPlayer.aChicken = true;
			}
			if (!modPlayer.aChicken)
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