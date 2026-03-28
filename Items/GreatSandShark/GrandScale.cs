using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.GreatSandShark
{
    public class GrandScale : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Grand Scale");
            // Tooltip.SetDefault("Large scale of an apex predator");
        }

        public override void SetDefaults()
        {
            Item.width = 15;
            Item.height = 12;
            Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 4, 50, 0);
			Item.rare = 7;
        }
    }
}