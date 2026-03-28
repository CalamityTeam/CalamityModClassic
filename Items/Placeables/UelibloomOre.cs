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
    public class UelibloomOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Uelibloom Ore");
        }

        public override void SetDefaults()
        {
            Item.createTile = Mod.Find<ModTile>("UelibloomOre").Type;
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.width = 10;
            Item.height = 10;
            Item.maxStack = 999;
			Item.rare = 10;
			Item.value = Item.buyPrice(0, 0, 10, 0);
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(0, 255, 200);
                }
            }
        }
    }
}