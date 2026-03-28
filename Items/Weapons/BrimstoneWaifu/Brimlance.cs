using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.BrimstoneWaifu
{
    public class Brimlance : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Brimlance");
            // Tooltip.SetDefault("This spear causes brimstone explosions on enemy hits\nEnemies killed by the spear drop brimstone fire");
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.damage = 75;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 19;
            Item.useStyle = 5;
            Item.useTime = 19;
            Item.knockBack = 7.5f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.height = 56;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
            Item.shoot = Mod.Find<ModProjectile>("Brimlance").Type;
            Item.shootSpeed = 12f;
        }
    }
}
