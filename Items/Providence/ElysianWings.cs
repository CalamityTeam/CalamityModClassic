using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Providence
{
    [AutoloadEquip(EquipType.Wings)]
    public class ElysianWings : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Elysian Wings");
            /* Tooltip.SetDefault("Horizontal speed: 9.75\n" +
                "Acceleration multiplier: 2.5\n" +
                "Great vertical speed\n" +
                "Flight time: 180"); */
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(180, 9.75f, 2.5f);
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 45, 0, 0);
            Item.expert = true;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            player.moveSpeed += 0.4f;
            player.lavaMax += 240;
            modPlayer.elysianFire = true;
            if (hideVisual)
            {
                modPlayer.elysianFire = false;
            }
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }
    }
}