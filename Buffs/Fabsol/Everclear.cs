using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs.Fabsol
{
	public class Everclear : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Everclear");
			// Description.SetDefault("Damage and critical strike chance boosted, life regen and defense reduced");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayerPreTrailer>().everclear = true;
		}
	}
}