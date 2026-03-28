using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class Trinity : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Trinity");
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.damage = 50;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.useTurn = true;
            Item.useStyle = 1;
            Item.knockBack = 4.5f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 46;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
            Item.shoot = 125;
            Item.shootSpeed = 11f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            switch (Main.rand.Next(6))
            {
                case 1: type = 125; break;
                case 2: type = 123; break;
                case 3: type = 121; break;
                default: break;
            }
            for (int projectiles = 0; projectiles <= 3; projectiles++)
            {
                float SpeedX = velocity.X + (float)Main.rand.Next(-30, 31) * 0.05f;
                float SpeedY = velocity.Y + (float)Main.rand.Next(-30, 31) * 0.05f;
                int proj = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage * 0.6), knockback, Main.myPlayer, 0f, 0f);
				Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
			}
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "VerstaltiteBar", 9);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 73);
            }
        }
    }
}
