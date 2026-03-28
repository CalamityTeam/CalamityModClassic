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
    public class RedtideSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Redtide Sword");
			// Tooltip.SetDefault("Throws short-range whirlpools");
		}

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.damage = 20;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 19;
            Item.useStyle = 1;
            Item.useTime = 19;
            Item.useTurn = true;
            Item.knockBack = 4;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 42;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 2;
			Item.shoot = Mod.Find<ModProjectile>("Whirlpool").Type;
			Item.shootSpeed = 6f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage / 2, knockback, player.whoAmI, 0f, 0f);
			return false;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "VictideBar", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
