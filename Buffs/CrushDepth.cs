using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class CrushDepth : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crush Depth");
			// Description.SetDefault("Aquatic pressure");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<CalamityPlayerPreTrailer>().cDepth = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<CalamityGlobalNPC>().cDepth = true;
		}
	}
}