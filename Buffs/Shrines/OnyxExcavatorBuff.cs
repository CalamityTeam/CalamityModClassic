using Terraria;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Buffs.Shrines
{
    class OnyxExcavatorBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Onyx Excavator");
            // Description.SetDefault("Drill");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<Items.Mounts.OnyxExcavator>(), player);
            player.buffTime[buffIndex] = 10;
            player.GetModPlayer<CalamityPlayerPreTrailer>().onyxExcavator = true;
        }
    }
}
