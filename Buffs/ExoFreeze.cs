using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Buffs
{
	public class ExoFreeze : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Exo Freeze");
			//Description.SetDefault("Cannot move");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<CalamityGlobalNPC1Point2>().eFreeze = true;
		}
	}
}