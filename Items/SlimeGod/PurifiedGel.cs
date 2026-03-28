using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.SlimeGod
{
    public class PurifiedGel : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Purified Gel");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 14;
            Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 2, 50, 0);
			Item.rare = 4;
        }
    }
}