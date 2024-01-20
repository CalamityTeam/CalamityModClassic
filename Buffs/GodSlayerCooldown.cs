﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1;
using CalamityModClassic1Point1.NPCs;

namespace CalamityModClassic1Point1.Buffs
{
	public class GodSlayerCooldown : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("God Slayer Cooldown");
			//Description.SetDefault("10% increase to all damage; godslayer effect is recharging");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type]/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = false;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayer1Point1>().godSlayerCooldown = true;
		}
	}
}