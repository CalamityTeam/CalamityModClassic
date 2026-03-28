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
    public class LeadCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lead Core");
            // Tooltip.SetDefault("Grants immunity to the acid rain debuff");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.rare = 2;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[Mod.Find<ModBuff>("Irradiated").Type] = true;
        }
    }
}