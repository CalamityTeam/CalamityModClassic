using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class SirenWaterSpeed : ModBuff
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Siren Speed");
            // Description.SetDefault("15% increased max speed and acceleration");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<CalamityPlayerPreTrailer>().sirenWaterBuff = true;
        }
	}
}