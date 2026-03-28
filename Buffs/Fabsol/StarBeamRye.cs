using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs.Fabsol
{
	public class StarBeamRye : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Star Beam Rye");
			// Description.SetDefault("Max mana, magic damage, and magic critical strike chance boosted, defense, mana usage, and life regen reduced");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayerPreTrailer>().starBeamRye = true;
		}
	}
}