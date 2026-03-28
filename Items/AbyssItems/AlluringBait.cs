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
    public class AlluringBait : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Alluring Bait");
            /* Tooltip.SetDefault("30% increased fishing power during the day\n" +
                "45% increased fishing power during the night\n" +
                "60% increased fishing power during a solar eclipse"); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 9, 0, 0);
            Item.rare = 3;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Main.eclipse) { player.fishingSkill += 60; }
            else if (!Main.dayTime) { player.fishingSkill += 45; }
            else { player.fishingSkill += 30; }
        }
    }
}