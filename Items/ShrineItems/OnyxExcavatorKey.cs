using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.ShrineItems
{
    class OnyxExcavatorKey : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Onyx Excavator Key");
            /* Tooltip.SetDefault("Summons a drill to drill through the world so you can destroy all the neat world generation\n" +
				"with complete disregard for all the creatures that inhabit these lands. I am sure the EPA and PETA would like\n" +
				"to have a word with you afterwards."); */
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
            Item.UseSound = SoundID.Item23;
            Item.noMelee = true;
            Item.mountType = Mod.Find<ModMount>("OnyxExcavator").Type;
		}
	}
}
