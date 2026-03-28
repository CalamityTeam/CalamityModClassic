using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.AquaticScourge
{
    public class AquaticEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Aquatic Emblem");
            /* Tooltip.SetDefault("Most ocean enemies become friendly and provides waterbreathing\n" +
                "Being underwater slowly boosts your defense over time but also slows movement speed\n" +
                "The defense boost and movement speed reduction slowly vanish while outside of water\n" +
                "Maximum defense boost is 30, maximum movement speed reduction is 5%\n" +
				"Provides a small amount of light in the abyss\n" +
				"Moderately reduces breath loss in the abyss"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = 5;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.aquaticEmblem = true;
            player.npcTypeNoAggro[65] = true;
            player.npcTypeNoAggro[220] = true;
            player.npcTypeNoAggro[64] = true;
            player.npcTypeNoAggro[67] = true;
            player.npcTypeNoAggro[221] = true;
            player.gills = true;
        }
    }
}