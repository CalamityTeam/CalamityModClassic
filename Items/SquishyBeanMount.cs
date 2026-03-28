using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items
{
    class SquishyBeanMount : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Suspicious Looking Jelly Bean");
            // Tooltip.SetDefault("JELLY BEAN");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 1;
			Item.rare = 10;
			Item.value = Item.buyPrice(1, 0, 0, 0);
			Item.expert = true;
            Item.UseSound = SoundID.Item3;
            Item.noMelee = true;
            Item.mountType = Mod.Find<ModMount>("SquishyBean").Type;
        }
    }
}
