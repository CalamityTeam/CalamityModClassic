using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class Mushy : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushy");
			// Description.SetDefault("Increased defense and life regen by 5");
			Main.buffNoTimeDisplay[Type] = true;
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayerPreTrailer>().mushy = true;
		}
	}
}