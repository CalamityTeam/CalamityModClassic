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
    public class CheatTestThing : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("lul");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = 1;
            Item.rare = 1;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            bool canUse = (player.name == "Fabsol" || player.name == "Totalbiscuit") && player.townNPCs <= 1;
            if (canUse)
            {
                modPlayer.lol = true;
            }
            else if (!player.immune)
            {
                player.KillMe(PlayerDeathReason.ByOther(12), 1000.0, 0, false);
            }
        }
    }
}