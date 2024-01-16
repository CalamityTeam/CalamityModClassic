using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1;
using CalamityModClassic1Point1.NPCs;

namespace CalamityModClassic1Point1.Buffs
{
	public class GodSlayerInferno : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("God Slayer Inferno");
			//Description.SetDefault("Your flesh is burning off");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type]/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = true;
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<CalamityGlobalNPC>().gsInferno = true;
		}
	}
}