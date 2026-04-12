using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Leviathan
{
    public class SirensSong : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Siren's Song");
            // Tooltip.SetDefault("Casts slow-moving treble clefs that confuse enemies");
        }

        public override void SetDefaults()
        {
            Item.damage = 77;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 7;
            Item.width = 56;
            Item.height = 50;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 6.5f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("SirensSong").Type;
            Item.shootSpeed = 13f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "IOU");
            recipe.AddIngredient(null, "LivingShard");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float SpeedX = velocity.X + (float)Main.rand.Next(-15, 16) * 0.05f;
            float SpeedY = velocity.Y + (float)Main.rand.Next(-15, 16) * 0.05f;
			float soundPitch = (Main.rand.NextFloat() - 0.5f) * 0.5f;
            Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, soundPitch, 0f);
            return false;
        }
    }
}