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
    public class EldritchTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eldritch Tome");
            // Tooltip.SetDefault("Casts eldritch tentacles to spear your enemies");
        }

        public override void SetDefaults()
        {
            Item.damage = 32;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 7;
            Item.width = 28;
            Item.crit = 3;
            Item.height = 30;
            Item.useTime = 7;
            Item.useAnimation = 21;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 3.5f;
            Item.value = Item.buyPrice(0, 12, 0, 0);
            Item.rare = 4;
            Item.UseSound = SoundID.Item103;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("EldritchTentacle").Type;
            Item.shootSpeed = 12f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int i = Main.myPlayer;
            int num73 = damage;
            float num74 = Item.knockBack;
            num74 = player.GetWeaponKnockback(Item, num74);
            player.itemTime = Item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
            Vector2 value2 = new Vector2(num78, num79);
            value2.Normalize();
            Vector2 value3 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
            value3.Normalize();
            value2 = value2 * 4f + value3;
            value2.Normalize();
            value2 *= Item.shootSpeed;
            float num91 = (float)Main.rand.Next(10, 80) * 0.001f;
            if (Main.rand.Next(2) == 0)
            {
                num91 *= -1f;
            }
            float num92 = (float)Main.rand.Next(10, 80) * 0.001f;
            if (Main.rand.Next(2) == 0)
            {
                num92 *= -1f;
            }
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, value2.X, value2.Y, Mod.Find<ModProjectile>("EldritchTentacle").Type, num73, num74, i, num92, num91);
            return false;
        }
    }
}