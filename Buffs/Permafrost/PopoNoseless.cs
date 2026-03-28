using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Buffs.Permafrost
{
	public class PopoNoseless : ModBuff
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Noseless Popo");
            // Description.SetDefault("Your nose has been stolen!");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
		{
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            if (modPlayer.snowmanPrevious)
            {
                modPlayer.snowmanPower = true;
                modPlayer.snowmanNoseless = true;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
	}
}