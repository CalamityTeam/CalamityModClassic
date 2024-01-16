using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1;
using CalamityModClassic1Point1.NPCs;

namespace CalamityModClassic1Point1.Buffs
{
	public class ExtremeGravity : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("Icarus' Folly");
			//Description.SetDefault("Your wing time is reduced by 75%, infinite flight is disabled");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type]/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayer>().eGravity = true;
		}
	}
}