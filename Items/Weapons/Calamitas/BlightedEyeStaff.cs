using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Calamitas
{
    public class BlightedEyeStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Entropy's Vigil");
            // Tooltip.SetDefault("Summons Calamitas and her brothers to protect you");
        }

        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.mana = 10;
            Item.width = 62;
            Item.height = 62;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item82;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Calamitamini").Type;
            Item.shootSpeed = 10f;
            Item.DamageType = DamageClass.Summon;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int i = Main.myPlayer;
            float num72 = Item.shootSpeed;
            float num74 = knockback;
            num74 = player.GetWeaponKnockback(Item, num74);
            player.itemTime = Item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (player.gravDir == -1f)
            {
                num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
            }
            float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
            float num81 = num80;
            if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
            {
                num78 = (float)player.direction;
                num79 = 0f;
                num80 = num72;
            }
            else
            {
                num80 = num72 / num80;
            }
            num78 *= num80;
            num79 *= num80;
            vector2.X = (float)Main.mouseX + Main.screenPosition.X;
            vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
            Vector2 spinningpoint = new Vector2(num78, num79);
            spinningpoint = spinningpoint.RotatedBy(1.5707963705062866, default(Vector2));
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X + spinningpoint.X, vector2.Y + spinningpoint.Y, spinningpoint.X, spinningpoint.Y, Mod.Find<ModProjectile>("Calamitamini").Type, damage, num74, i, 0f, 0f);
            spinningpoint = spinningpoint.RotatedBy(-3.1415927410125732, default(Vector2));
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X + spinningpoint.X, vector2.Y + spinningpoint.Y, spinningpoint.X, spinningpoint.Y, Mod.Find<ModProjectile>("Catastromini").Type, damage, num74, i, 0f, 0f);
            spinningpoint = spinningpoint.RotatedBy(-5.1415927410125732, default(Vector2));
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X + spinningpoint.X, vector2.Y + spinningpoint.Y, spinningpoint.X, spinningpoint.Y, Mod.Find<ModProjectile>("Cataclymini").Type, damage, num74, i, 0f, 0f);
            return false;
        }
    }
}