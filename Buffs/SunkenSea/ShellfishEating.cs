using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs.SunkenSea
{
	public class ShellfishEating : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shellfish Claps");
			// Description.SetDefault("Clamfest");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<CalamityGlobalNPC>().shellfishVore = true;
		}
	}
}