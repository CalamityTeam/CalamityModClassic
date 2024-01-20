using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Buffs
{
	public class CosmicFreeze : ModBuff
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cosmic Freeze");
			//Description.SetDefault("You feel as cold as the empty expanse of outer space");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayer1Point2>().cFreeze = true;
		}
	}
}