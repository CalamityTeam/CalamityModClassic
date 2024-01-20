﻿using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Buffs
{
	public class DankCreeper : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("Dank Creeper");
			//Description.SetDefault("The dank creeper will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			CalamityPlayer1Point1 modPlayer = player.GetModPlayer<CalamityPlayer1Point1>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DankCreeper").Type] > 0)
			{
				modPlayer.dCreeper = true;
			}
			if (!modPlayer.dCreeper)
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