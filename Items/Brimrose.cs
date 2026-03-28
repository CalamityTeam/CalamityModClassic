using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CalamityModClassicPreTrailer.Items
{
    class Brimrose : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Brimrose");
            // Tooltip.SetDefault("Summons a brimrose mount");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 1;
			Item.value = Item.buyPrice(1, 50, 0, 0);
			Item.rare = 10;
			Item.expert = true;
            Item.UseSound = SoundID.Item3;
            Item.noMelee = true;
            Item.mountType = Mod.Find<ModMount>("PhuppersChair").Type;
        }
    }
}
