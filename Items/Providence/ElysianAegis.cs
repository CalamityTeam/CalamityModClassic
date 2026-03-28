using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Providence
{
    [AutoloadEquip(EquipType.Shield)]
    public class ElysianAegis : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Elysian Aegis");
            /* Tooltip.SetDefault("Grants immunity to fire blocks and knockback\n" +
                               "+40 max life and increased life regen\n" +
                               "Grants a supreme holy flame dash\n" +
                               "Can be used to ram enemies\n" +
                               "Press N to activate buffs to all damage, crit chance, and defense\n" +
                               "Activating this buff will reduce your movement speed and increase enemy aggro\n" +
                               "Toggle visibility of this accessory to enable/disable the dash"); */
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 42;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.expert = true;
            Item.rare = 9;
            Item.defense = 8;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            if (!hideVisual) { modPlayer.dashMod = 3; }
            modPlayer.elysianAegis = true;
            player.noKnockback = true;
            player.fireWalk = true;
            player.lifeRegen += 2;
            player.statLifeMax2 += 40;
        }
    }
}