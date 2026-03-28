using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.SlimeGod
{
    public class ManaOverloader : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mana Overloader");
            /* Tooltip.SetDefault("Increases max mana by 50 and magic damage by 6%\n" +
                               "Life regen lowered by 5 if mana is above 50% of its maximum\n" +
                               "Magic healing if mana is below 5% of its maximum"); */
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = 5;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += 50;
            player.GetDamage(DamageClass.Magic) += 0.06f;
            if (player.statMana >= (player.statManaMax2 * 0.5f))
            {
                player.lifeRegen -= 5;
            }
            if (player.statMana < (player.statManaMax2 * 0.05f))
            {
                player.ghostHeal = true;
            }
        }
    }
}