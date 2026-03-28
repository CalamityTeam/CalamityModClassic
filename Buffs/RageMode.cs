using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class RageMode : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rage Mode");
			// Description.SetDefault("100% (400% in Death Mode) increased damage.  Can be boosted by other items up to 160% (640% in Death Mode).");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayerPreTrailer>().rageMode = true;
		}
	}
}