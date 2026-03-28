using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class ArmorShattering : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Armor Shattering");
			// Description.SetDefault("Melee and rogue attacks break enemy armor");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayerPreTrailer>().armorShattering = true;
		}
	}
}