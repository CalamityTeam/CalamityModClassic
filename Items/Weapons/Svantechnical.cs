using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class Svantechnical : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Svantechnical");
        }

        public override void SetDefaults()
        {
            Item.damage = 625;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 60;
            Item.height = 26;
            Item.useAnimation = 24;
            Item.reuseDelay = 10;
            Item.useTime = 2;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 3.5f;
            Item.value = Item.buyPrice(5, 0, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item31;
            Item.autoReuse = true;
            Item.shootSpeed = 12f;
            Item.shoot = 10;
            Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 16;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) < 70)
                return false;
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int i = Main.myPlayer;
            float num72 = Item.shootSpeed;
            int num73 = damage;
            float num74 = Item.knockBack;
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
            float num208 = num78;
            float num209 = num79;
            num208 += (float)Main.rand.Next(-1, 2) * 0.5f;
            num209 += (float)Main.rand.Next(-1, 2) * 0.5f;
            if (Collision.CanHitLine(player.Center, 0, 0, vector2 + new Vector2(num208, num209) * 2f, 0, 0))
            {
                vector2 += new Vector2(num208, num209);
            }
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y - player.gravDir * 4f, num208, num209, type, num73, num74, i, 0f, (float)Main.rand.Next(12) / 6f);
            int num6 = Main.rand.Next(2, 4);
            for (int index = 0; index < num6; ++index)
            {
                float SpeedX = velocity.X + (float)Main.rand.Next(-60, 61) * 0.05f;
                float SpeedY = velocity.Y + (float)Main.rand.Next(-60, 61) * 0.05f;
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SDMG);
            recipe.AddIngredient(null, "ShadowspecBar", 5);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddIngredient(ItemID.SoulofSight, 10);
            recipe.AddIngredient(ItemID.SoulofFright, 10);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}