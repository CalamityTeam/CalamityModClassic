using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.ShrineItems
{
    class TundraLeash : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tundra Leash");
            // Tooltip.SetDefault("Summons an angry dog mount");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 4;
			Item.rare = 3;
			Item.value = Item.buyPrice(0, 9, 0, 0);
            Item.UseSound = SoundID.NPCHit56;
            Item.noMelee = true;
            Item.mountType = Mod.Find<ModMount>("AngryDog").Type;
		}
	}
}
