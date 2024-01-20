using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Buffs
{
	public class BrimstoneFlames : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Brimstone Flames");
			//Description.SetDefault("Rapid health loss");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayer1Point2>().bFlames = true;
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<CalamityGlobalNPC1Point2>().bFlames = true;
		}
	}
}