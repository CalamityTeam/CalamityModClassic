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
    public class ReaperTooth : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reaper Tooth");
            // Tooltip.SetDefault("Sharp enough to cut diamonds");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 6, 0, 0);
			Item.rare = 10;
        }
    }
}