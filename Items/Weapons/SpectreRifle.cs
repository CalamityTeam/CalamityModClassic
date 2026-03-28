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
    public class SpectreRifle : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spectre Rifle");
        }

        public override void SetDefaults()
        {
            Item.damage = 150;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 88;
            Item.height = 30;
            Item.crit += 22;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 7;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
            Item.UseSound = SoundID.Item40;
            Item.autoReuse = false;
            Item.shoot = 297;
            Item.shootSpeed = 24f;
            Item.useAmmo = 97;
        }

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int proj = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, 297, damage, knockback, player.whoAmI, 0f, 0f);
			Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceRanged = true;
			return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpectreBar, 7);
            recipe.AddIngredient(null, "CoreofEleum", 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}