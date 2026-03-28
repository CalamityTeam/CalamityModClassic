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
    public class SDFMG : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("SDFMG");
            // Tooltip.SetDefault("It came from the edge of Terraria\n50% chance to not consume ammo");
        }

        public override void SetDefaults()
        {
            Item.damage = 250;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 74;
            Item.height = 34;
            Item.crit += 16;
            Item.useTime = 2;
            Item.useAnimation = 4;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2.75f;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float SpeedX = velocity.X + (float)Main.rand.Next(-5, 6) * 0.05f;
            float SpeedY = velocity.Y + (float)Main.rand.Next(-5, 6) * 0.05f;
            if (Main.rand.Next(5) == 0)
            {
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("FishronRPG").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            }
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) < 50)
                return false;
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SDMG);
            recipe.AddIngredient(ItemID.ShrimpyTruffle);
            recipe.AddIngredient(null, "CosmiliteBar", 4);
            recipe.AddIngredient(null, "Phantoplasm", 4);
            recipe.AddIngredient(null, "DepthCells", 10);
            recipe.AddIngredient(null, "Lumenite", 10);
            recipe.AddIngredient(null, "Tenebris", 5);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}