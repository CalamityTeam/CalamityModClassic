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
    public class Lumenite : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lumenyl");
            // Tooltip.SetDefault("A shard of lumenous energy");
        }

        public override void SetDefaults()
        {
            Item.createTile = Mod.Find<ModTile>("LumenylCrystals").Type;
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.width = 26;
            Item.height = 26;
            Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 2, 0, 0);
			Item.rare = 3;
        }
    }
}