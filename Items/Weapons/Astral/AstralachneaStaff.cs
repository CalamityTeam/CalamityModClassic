using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Astral
{
    public class AstralachneaStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Astralachnea Staff");
            // Tooltip.SetDefault("Fires a spread of homing astral spider fangs");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 19;
            Item.width = 52;
            Item.height = 52;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 6f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item46;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("AstralachneaFang").Type;
            Item.shootSpeed = 13f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            int i = Main.myPlayer;
            float num72 = Item.shootSpeed;
            int num73 = damage;
            float num74 = knockback;
            float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
            float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
            int spikeAmount = 4;
            if (Main.rand.Next(3) == 0)
            {
                spikeAmount++;
            }
            if (Main.rand.Next(4) == 0)
            {
                spikeAmount++;
            }
            if (Main.rand.Next(5) == 0)
            {
                spikeAmount += 2;
            }
            for (int num131 = 0; num131 < spikeAmount; num131++)
            {
                float num132 = num78;
                float num133 = num79;
                float num134 = 0.05f * (float)num131;
                num132 += (float)Main.rand.Next(-400, 400) * num134;
                num133 += (float)Main.rand.Next(-400, 400) * num134;
                num80 = (float)Math.Sqrt((double)(num132 * num132 + num133 * num133));
                num80 = num72 / num80;
                num132 *= num80;
                num133 *= num80;
                float x2 = vector2.X;
                float y2 = vector2.Y;
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), x2, y2, num132, num133, Mod.Find<ModProjectile>("AstralachneaFang").Type, num73, num74, i, 0f, 0f);
            }
            return false;
        }
    }
}