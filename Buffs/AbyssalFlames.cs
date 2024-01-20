using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Buffs
{
	public class AbyssalFlames : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Abyssal Flames");
			//Description.SetDefault("Your soul is being consumed");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayer1Point2>().aFlames = true;
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<CalamityGlobalNPC1Point2>().aFlames = true;
		}
	}
}