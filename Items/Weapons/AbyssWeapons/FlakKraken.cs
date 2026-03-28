using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using Terraria.Audio;

namespace CalamityModClassicPreTrailer.Items.Weapons.AbyssWeapons
{
	public class FlakKraken : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flak Kraken");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 54;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 16;
	        Item.height = 16;
	        Item.useTime = 10;
	        Item.useAnimation = 10;
            Item.reuseDelay = 5;
            Item.useStyle = 5;
	        Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.knockBack = 0f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7; 
	        Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/LaserCannon");
	        Item.shoot = Mod.Find<ModProjectile>("FlakKrakenGun").Type;
	        Item.shootSpeed = 30f; //30
	    }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
		    Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("FlakKrakenGun").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    return false;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DepthCells", 30);
            recipe.AddIngredient(null, "Lumenite", 10);
            recipe.AddIngredient(null, "Tenebris", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}