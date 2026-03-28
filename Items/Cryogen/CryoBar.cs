using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Cryogen
{
    public class CryoBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Frigid Bar");
            // Tooltip.SetDefault("Cold to the touch");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 3, 0, 0);
			Item.rare = 5;
        }
    }
}