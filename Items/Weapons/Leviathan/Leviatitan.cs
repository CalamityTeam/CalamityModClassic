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
    public class Leviatitan : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Leviatitan");
        }

        public override void SetDefaults()
        {
            Item.damage = 80;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 82;
            Item.height = 28;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item92;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("AquaBlast").Type;
            Item.shootSpeed = 18f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15, 0);
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
            float SpeedX = velocity.X + (float)Main.rand.Next(-10, 11) * 0.05f;
            float SpeedY = velocity.Y + (float)Main.rand.Next(-10, 11) * 0.05f;
            if (Main.rand.Next(3) == 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("AquaBlastToxic").Type, (int)((double)damage * 1.5), knockback, player.whoAmI, 0.0f, 0.0f);
            }
            else
            {
                Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("AquaBlast").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
    }
}