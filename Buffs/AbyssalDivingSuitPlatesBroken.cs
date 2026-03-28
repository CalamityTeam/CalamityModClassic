using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class AbyssalDivingSuitPlatesBroken : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Abyssal Diving Suit Plates Broken");
			// Description.SetDefault("The plates are regenerating");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayerPreTrailer>().abyssalDivingSuitCooldown = true;
		}
	}
}