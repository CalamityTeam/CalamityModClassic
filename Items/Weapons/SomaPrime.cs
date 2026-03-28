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
    public class SomaPrime : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soma Prime");
        }

        public override void SetDefaults()
        {
            Item.damage = 600;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 94;
            Item.height = 34;
            Item.crit += 40;
            Item.useTime = 1;
            Item.useAnimation = 3;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(5, 0, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item40;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("SlashRound").Type;
            Item.shootSpeed = 30f;
            Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 16;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-25, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ShadowspecBar", 5);
            recipe.AddIngredient(null, "P90");
            recipe.AddIngredient(null, "Minigun");
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float SpeedX = velocity.X + (float)Main.rand.Next(-10, 11) * 0.05f;
            float SpeedY = velocity.Y + (float)Main.rand.Next(-10, 11) * 0.05f;
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("SlashRound").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) < 50)
                return false;
            return true;
        }
    }
}