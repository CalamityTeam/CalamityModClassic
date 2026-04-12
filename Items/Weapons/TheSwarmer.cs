using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class TheSwarmer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Swarmer");
        }

        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 7;
            Item.width = 74;
            Item.height = 36;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 95, 0, 0);
            Item.rare = 9;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = 189;
            Item.shootSpeed = 12f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15, -5);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentVortex, 20);
            recipe.AddIngredient(ItemID.BeeGun);
            recipe.AddIngredient(ItemID.WaspGun);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i <= 3; i++)
            {
                float SpeedX = velocity.X + (float)Main.rand.Next(-35, 36) * 0.05f;
                float SpeedY = velocity.Y + (float)Main.rand.Next(-35, 36) * 0.05f;
                int wasps = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, 0f, player.whoAmI, 0.0f, 0.0f);
                Main.projectile[wasps].penetrate = 1;
				Main.projectile[wasps].GetGlobalProjectile<CalamityGlobalProjectile>().forceMagic = true;
			}
            for (int i = 0; i <= 3; i++)
            {
                float SpeedX2 = velocity.X + (float)Main.rand.Next(-35, 36) * 0.05f;
                float SpeedY2 = velocity.Y + (float)Main.rand.Next(-35, 36) * 0.05f;
                int bees = Projectile.NewProjectile(source, position.X, position.Y, SpeedX2, SpeedY2, player.beeType(), player.beeDamage(Item.damage), player.beeKB(0f), player.whoAmI, 0.0f, 0.0f);
                Main.projectile[bees].penetrate = 1;
				Main.projectile[bees].GetGlobalProjectile<CalamityGlobalProjectile>().forceMagic = true;
			}
            return false;
        }
    }
}