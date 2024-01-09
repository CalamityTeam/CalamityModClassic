using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0;
using CalamityModClassic1Point0.NPCs;

namespace CalamityModClassic1Point0.Buffs
{
	public class BrimstoneFlames : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//Main.buffName[this.Type] = "Brimstone Flames");
			//Main.buffTip[this.Type] = "Rapid health loss");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<CalamityGlobalNPC>().bFlames = true;
		}
	}
}