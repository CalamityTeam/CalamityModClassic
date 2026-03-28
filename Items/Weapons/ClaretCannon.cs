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
    public class ClaretCannon : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Claret Cannon");
            // Tooltip.SetDefault("Fires bloodfire bullets that drain enemy health");
        }

        public override void SetDefaults()
        {
            Item.damage = 215;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 48;
            Item.height = 30;
            Item.useTime = 3;
            Item.reuseDelay = 10;
            Item.useAnimation = 9;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 5.5f;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item40;
            Item.autoReuse = true;
            Item.shootSpeed = 24f;
            Item.shoot = Mod.Find<ModProjectile>("BloodfireBullet").Type;
            Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, -5);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "BloodstoneCore", 4);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("BloodfireBullet").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}