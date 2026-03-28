using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class ArmorCrunch : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Armor Crunch");
			// Description.SetDefault("Your armor is shredded");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
		
		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<CalamityGlobalNPC>().aCrunch = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<CalamityPlayerPreTrailer>().aCrunch = true;
        }
    }
}