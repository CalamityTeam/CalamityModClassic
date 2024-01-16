using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1;
using CalamityModClassic1Point1.NPCs;

namespace CalamityModClassic1Point1.Buffs
{
	public class GlacialState : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("Glacial State");
			//Description.SetDefault("Cannot move and defense is shattered");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type]/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = false;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayer>().gState = true;
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<CalamityGlobalNPC>().gState = true;
		}
	}
}