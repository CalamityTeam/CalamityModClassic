using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs.Fabsol
{
	public class Trippy : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Trippy");
			// Description.SetDefault("You see the world for what it truly is...and you also have a 50% increase to all damage");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayerPreTrailer>().trippy = true;
		}
	}
}