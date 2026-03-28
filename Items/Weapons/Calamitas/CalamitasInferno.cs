using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Calamitas
{
    public class CalamitasInferno : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lashes of Chaos");
            // Tooltip.SetDefault("Watch the world burn...");
        }

        public override void SetDefaults()
        {
            Item.damage = 98;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 20;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 7.5f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("BrimstoneHellfireballFriendly").Type;
            Item.shootSpeed = 16f;
        }
    }
}