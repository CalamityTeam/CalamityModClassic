using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.AbyssItems
{
    public class DepthCells : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Depth Cells");
            // Tooltip.SetDefault("The cells of abyssal creatures");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 2, 0, 0);
			Item.rare = 3;
        }
    }
}