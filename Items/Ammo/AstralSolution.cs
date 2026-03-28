using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Ammo
{
    public class AstralSolution : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Astral Solution");
            // Tooltip.SetDefault("Used by the Clentaminator.\nSpreads the Astral.");
        }

        public override void SetDefaults()
        {
            Item.ammo = AmmoID.Solution;
            Item.shoot = Mod.Find<ModProjectile>("AstralSpray").Type - ProjectileID.PureSpray;
            Item.width = 10;
            Item.height = 12;
            //item.value = Item.buyPrice(0, 0, 25, 0);
            Item.rare = 3;
            Item.maxStack = 999;
            Item.consumable = true;
            return;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return !(player.itemAnimation < player.HeldItem.useAnimation - 3);
        }
    }
}
