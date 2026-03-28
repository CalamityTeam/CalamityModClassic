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
    public class Floodtide : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Floodtide");
			// Tooltip.SetDefault("Launches sharks, because sharks are awesome!");
		}

        public override void SetDefaults()
        {
            Item.damage = 89;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 60;
            Item.height = 64;
            Item.useTime = 23;
            Item.useAnimation = 23;
			Item.useTurn = true;
            Item.useStyle = 1;
            Item.knockBack = 5.5f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = 408;
            Item.shootSpeed = 11f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int proj = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0f, 0f);
			Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
			return false;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 217);
            }
        }
        
        public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "VictideBar", 5);
	        recipe.AddIngredient(ItemID.SharkFin, 2);
	        recipe.AddIngredient(ItemID.AdamantiteBar, 5);
            recipe.AddIngredient(null, "DepthCells", 10);
            recipe.AddIngredient(null, "Lumenite", 10);
            recipe.AddIngredient(null, "Tenebris", 5);
            recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	        recipe = CreateRecipe();
	        recipe.AddIngredient(null, "VictideBar", 5);
	        recipe.AddIngredient(ItemID.SharkFin, 2);
	        recipe.AddIngredient(ItemID.TitaniumBar, 5);
            recipe.AddIngredient(null, "DepthCells", 10);
            recipe.AddIngredient(null, "Lumenite", 10);
            recipe.AddIngredient(null, "Tenebris", 5);
            recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
    }
}