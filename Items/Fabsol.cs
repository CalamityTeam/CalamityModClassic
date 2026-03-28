using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items
{
    class Fabsol : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fabsol");
            /* Tooltip.SetDefault("Summons an alicorn\n" +
                "Revengeance drop"); */
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 1;
			Item.rare = 10;
			Item.value = Item.buyPrice(3, 0, 0, 0);
			Item.expert = true;
            Item.UseSound = SoundID.Item3;
            Item.noMelee = true;
            Item.mountType = Mod.Find<ModMount>("Fab").Type;
        }
    }
}
