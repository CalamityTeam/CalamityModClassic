using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Buffs
{
	public class ElementalAxe : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("Elemental Axe");
			//Description.SetDefault("The elemental axe will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ElementalAxeG").Type] > 0)
			{
				modPlayer.eAxe = true;
			}
			else if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ElementalAxeR").Type] > 0)
			{
				modPlayer.eAxe = true;
			}
			else if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ElementalAxeO").Type] > 0)
			{
				modPlayer.eAxe = true;
			}
			else if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ElementalAxeY").Type] > 0)
			{
				modPlayer.eAxe = true;
			}
			else if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ElementalAxeB").Type] > 0)
			{
				modPlayer.eAxe = true;
			}
			else if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ElementalAxeI").Type] > 0)
			{
				modPlayer.eAxe = true;
			}
			else if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ElementalAxeV").Type] > 0)
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