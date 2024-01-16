using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1;
using CalamityModClassic1Point1.NPCs;

namespace CalamityModClassic1Point1.Buffs
{
	public class BurningBlood : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("Burning Blood");
			//Description.SetDefault("Your blood is on fire");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type]/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayer>().bBlood = true;
		}
	}
}