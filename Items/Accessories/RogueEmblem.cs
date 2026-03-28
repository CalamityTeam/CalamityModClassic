using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
    public class RogueEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rogue Emblem");
            // Tooltip.SetDefault("15% increased rogue damage");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = 100000;
            Item.rare = 4;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityCustomThrowingDamagePlayer modPlayer2 = CalamityCustomThrowingDamagePlayer.ModPlayer(player);
            modPlayer2.throwingDamage += 0.15f;
        }
    }
}