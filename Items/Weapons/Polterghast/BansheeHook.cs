using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Polterghast
{
    public class BansheeHook : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Banshee Hook");
            // Tooltip.SetDefault("Swings a banshee hook that explodes on contact");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.damage = 155;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 21;
            Item.useStyle = 5;
            Item.useTime = 21;
            Item.knockBack = 8.5f;
            Item.UseSound = SoundID.DD2_GhastlyGlaivePierce;
            Item.autoReuse = true;
            Item.height = 16;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("BansheeHook").Type;
            Item.shootSpeed = 42f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float num82 = (float)Main.mouseX + Main.screenPosition.X - position.X;
            float num83 = (float)Main.mouseY + Main.screenPosition.Y - position.Y;
            if (player.gravDir == -1f)
            {
                num83 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - position.Y;
            }
            float num84 = (float)Math.Sqrt((double)(num82 * num82 + num83 * num83));
            float num85 = num84;
            if ((float.IsNaN(num82) && float.IsNaN(num83)) || (num82 == 0f && num83 == 0f))
            {
                num82 = (float)player.direction;
                num83 = 0f;
                num84 = Item.shootSpeed;
            }
            else
            {
                num84 = Item.shootSpeed / num84;
            }
            num82 *= num84;
            num83 *= num84;
            float ai4 = Main.rand.NextFloat() * Item.shootSpeed * 0.75f * (float)player.direction;
            Vector2 velocity2 = new Vector2(num82, num83);
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity2.X, velocity2.Y, Mod.Find<ModProjectile>("BansheeHook").Type, damage, knockback, player.whoAmI, ai4, 0.0f);
            return false;
        }
    }
}
