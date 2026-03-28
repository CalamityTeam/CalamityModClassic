using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class ScarfCooldown : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scarf Cooldown");
			// Description.SetDefault("Your dodge is recharging");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayerPreTrailer>().scarfCooldown = true;
		}
	}
}