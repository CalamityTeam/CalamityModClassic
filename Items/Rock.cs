using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items
{
    public class Rock : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rock");
            // Tooltip.SetDefault("The first object Xeroc ever created");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
			Item.value = Item.buyPrice(0, 0, 0, 1);
		}
    }
}