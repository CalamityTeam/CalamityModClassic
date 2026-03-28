using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
    public class HeartofDarkness : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Heart of Darkness");
            /* Tooltip.SetDefault("Gives 10% increased damage while you have the heart attack debuff\n" +
                "Increases your chance of getting the heart attack debuff\n" +
                "Rage mode does more damage\n" +
                "You gain rage over time\n" +
                "Revengeance drop"); */
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = 10;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.heartOfDarkness = true;
        }
    }
}