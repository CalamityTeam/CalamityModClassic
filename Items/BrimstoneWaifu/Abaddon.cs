using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.BrimstoneWaifu
{
    [AutoloadEquip(EquipType.Head)]
    public class Abaddon : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abaddon");
            // Tooltip.SetDefault("Makes you immune to Brimstone Flames");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = 5;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
        }
    }
}