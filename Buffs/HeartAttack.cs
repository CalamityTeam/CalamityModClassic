using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Buffs
{
	public class HeartAttack : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Heart Attack");
			//Description.SetDefault("Reduces your damage by 30% during the day, you are one with the dark...");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayer1Point2>().hAttack = true;
		}
	}
}