using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    class Onyxia : ModItem
    {
        const int NotConsumeAmmo = 50;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Onyxia");
            // Tooltip.SetDefault(NotConsumeAmmo.ToString() + "% chance to not consume ammo");
        }

        public override void SetDefaults()
        {
            Item.damage = 235;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 84;
            Item.height = 34;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 4.5f;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item36;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 28f;
            Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-11, 3);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Fire the Onyx Shard that is characteristic of the Onyx Blaster
            // The shard deals triple damage and double knockback
            int shardDamage = 3 * damage;
            float shardKB = 2f * Item.knockBack;
            float shardVelocityX = (velocity.X + (float)Main.rand.Next(-25, 26) * 0.05f) * 0.9f;
            float shardVelocityY = (velocity.Y + (float)Main.rand.Next(-25, 26) * 0.05f) * 0.9f;
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, shardVelocityX, shardVelocityY, 661, shardDamage, shardKB, player.whoAmI, 0.0f, 0.0f);

            // Fire three symmetric pairs of bullets alongside it
            Vector2 baseVelocity = new Vector2(velocity.X, velocity.Y);
            for (int i = 0; i < 3; i++)
            {
                float randAngle = Main.rand.NextFloat(0.035f);
                float randVelMultiplier = Main.rand.NextFloat(0.92f, 1.08f);
                Vector2 left = baseVelocity.RotatedBy(-randAngle) * randVelMultiplier;
                Vector2 right = baseVelocity.RotatedBy(randAngle) * randVelMultiplier;
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, left.X, left.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, right.X, right.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) < NotConsumeAmmo)
                return false;
            return true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(null, "OnyxChainBlaster");
            r.AddIngredient(null, "CosmiliteBar", 10);
            r.AddIngredient(null, "DarksunFragment", 10);
            r.AddTile(null, "DraedonsForge");
            r.Register();
        }
    }
}
