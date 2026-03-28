using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class ArmorCrumbling : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Armor Crumbling");
			// Description.SetDefault("Melee and rogue attacks break enemy armor");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayerPreTrailer>().armorCrumbling = true;
		}
	}
}