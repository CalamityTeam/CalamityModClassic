using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Placeables
{
    public class BrimstoneSlag : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Brimstone Slag");
        }

        public override void SetDefaults()
        {
            Item.createTile = Mod.Find<ModTile>("BrimstoneSlag").Type;
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.width = 13;
            Item.height = 10;
            Item.maxStack = 999;
            Item.value = 1000;
            Item.rare = 3;
        }
    }
}