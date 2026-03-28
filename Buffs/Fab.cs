using Terraria;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Buffs
{
    class Fab : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Alicorn");
            // Description.SetDefault("You beat DoG while drunk, you are truly fabulous!");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<Items.Mounts.Fab>(), player);
            player.buffTime[buffIndex] = 10;
            player.GetModPlayer<CalamityPlayerPreTrailer>().fab = true;
        }
    }
}
