using Terraria;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Buffs
{
    class GazeOfCrysthamyrBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gaze of Crysthamyr");
            // Description.SetDefault("You are riding a shadow dragon");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<Items.Mounts.Crysthamyr>(), player);
            player.buffTime[buffIndex] = 10;
            player.GetModPlayer<CalamityPlayerPreTrailer>().crysthamyr = true;
        }
    }
}
