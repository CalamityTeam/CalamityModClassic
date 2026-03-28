using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class AbyssalDivingSuitPlates : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Abyssal Diving Suit Plates");
			// Description.SetDefault("The plates will absorb 15% damage");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayerPreTrailer>().abyssalDivingSuitPlates = true;
		}
	}
}