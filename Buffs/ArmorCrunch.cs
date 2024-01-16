using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1;
using CalamityModClassic1Point1.NPCs;

namespace CalamityModClassic1Point1.Buffs
{
	public class ArmorCrunch : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("Armor Crunch");
			//Description.SetDefault("Your armor is shredded");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type]/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = true;
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<CalamityGlobalNPC>().aCrunch = true;
		}
	}
}