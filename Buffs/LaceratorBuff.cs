using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Buffs
{
    public class LaceratorBuff : ModBuff // The Life Drain weapon was changed to no longer work if you aren't holding the weapon, so an entire custom buff is being made 
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<CalamityGlobalNPC1Point2>().lacerator = true;
        }
    }
}