using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs.Fabsol
{
	public class CinnamonRoll : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cinnamon Roll");
			// Description.SetDefault("Mana regen rate and fire weapon damage boosted, defense reduced");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayerPreTrailer>().cinnamonRoll = true;
		}
	}
}