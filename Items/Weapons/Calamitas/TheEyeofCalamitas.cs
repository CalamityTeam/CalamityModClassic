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
    public class TheEyeofCalamitas : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oblivion");
            // Tooltip.SetDefault("Fires brimstone lasers when enemies are near");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.TheEyeOfCthulhu);
            Item.damage = 41;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 5;
            Item.channel = true;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("TheEyeofCalamitasProjectile").Type;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}