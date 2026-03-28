using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs
{
	public class SirenBobs : ModBuff
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Siren");
            // Description.SetDefault("You are a siren now");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
		{
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            if (modPlayer.sirenBoobsPrevious)
            {
                modPlayer.sirenBoobsPower = true;
                player.statDefense += 1 + 
                    (NPC.downedBoss3 ? 4 : 0) +
                    (Main.hardMode ? 5 : 0) +
                    (NPC.downedMoonlord ? 5 : 0);
                player.detectCreature = true;
                player.lifeRegen += 0 + 
                    (NPC.downedBoss3 ? 1 : 0) +
                    (NPC.downedMoonlord ? 1 : 0);
                player.ignoreWater = NPC.downedBoss3;
                player.accFlipper = true;
                player.discountAvailable = Main.hardMode;
                if (player.breath <= player.breathMax + 2 && !modPlayer.ZoneAbyss && NPC.downedBoss3)
                {
                    player.breath = player.breathMax + 3;
                }
                if (Main.myPlayer == player.whoAmI && player.wet && NPC.downedBoss3)
                {
                    player.AddBuff(Mod.Find<ModBuff>("SirenWaterSpeed").Type, 360);
                }
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
	}
}