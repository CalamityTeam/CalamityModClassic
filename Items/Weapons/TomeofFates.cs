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
    public class TomeofFates : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tome of Fates");
            // Tooltip.SetDefault("Casts cosmic tentacles to spear your enemies\nCan randomly fire a brimstone tentacle for immense damage");
        }

        public override void SetDefaults()
        {
            Item.damage = 79;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
            Item.width = 28;
            Item.crit = 3;
            Item.height = 30;
            Item.useTime = 5;
            Item.useAnimation = 20;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 3.5f;
            Item.value = Item.buyPrice(0, 95, 0, 0);
            Item.rare = 9;
            Item.UseSound = SoundID.Item103;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("CosmicTentacle").Type;
            Item.shootSpeed = 17f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "MeldiateBar", 9);
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
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
            int projChoice = Main.rand.Next(7);
            float num91 = (float)Main.rand.Next(10, 160) * 0.001f;
            if (Main.rand.Next(2) == 0)
            {
                num91 *= -1f;
            }
            float num92 = (float)Main.rand.Next(10, 160) * 0.001f;
            if (Main.rand.Next(2) == 0)
            {
                num92 *= -1f;
            }
            if (projChoice == 0)
            {
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, value2.X, value2.Y, Mod.Find<ModProjectile>("BrimstoneTentacle").Type, (int)((double)num73 * 1.5f), num74, i, num92, num91);
            }
            else
            {
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, value2.X, value2.Y, Mod.Find<ModProjectile>("CosmicTentacle").Type, num73, num74, i, num92, num91);
            }
            return false;
        }
    }
}