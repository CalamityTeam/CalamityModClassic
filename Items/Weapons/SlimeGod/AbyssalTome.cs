using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.SlimeGod
{
    public class AbyssalTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyssal Tome");
            // Tooltip.SetDefault("Casts a slow-moving ball of dark energy");
        }

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 15;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(0, 12, 0, 0);
            Item.rare = 4;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("AbyssBall").Type;
            Item.shootSpeed = 9f;
        }
    }
}