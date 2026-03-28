using Terraria;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Buffs.Shrines
{
    class TundraLeashBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Angry Dog");
            // Description.SetDefault("You are riding an angry dog");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<Items.Mounts.AngryDog>(), player);
            player.buffTime[buffIndex] = 10;
            player.GetModPlayer<CalamityPlayerPreTrailer>().angryDog = true;
        }
    }
}
