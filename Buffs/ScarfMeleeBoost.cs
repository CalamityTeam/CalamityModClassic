using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class ScarfMeleeBoost : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scarf Boost");
			// Description.SetDefault("10% increased damage, 5% increased crit chance, and 10% increased melee speed");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayerPreTrailer>().sMeleeBoost = true;
		}
	}
}