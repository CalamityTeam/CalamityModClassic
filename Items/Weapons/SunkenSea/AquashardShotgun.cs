using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.SunkenSea
{
	public class AquashardShotgun : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aquashard Shotgun");
			// Tooltip.SetDefault("Shoots aquashards which split upon hitting an enemy");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 12;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 62;
	        Item.height = 26;
	        Item.crit += 6;
	        Item.useTime = 25;
	        Item.useAnimation = 25;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5.5f;
			Item.value = Item.buyPrice(0, 2, 0, 0);
			Item.rare = 2;
	        Item.UseSound = SoundID.Item61;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("Aquashard").Type;
	        Item.shootSpeed = 22f;
	    }
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	int num6 = Main.rand.Next(2, 4);
			for (int index = 0; index < num6; ++index)
			{
			    float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
			    float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
			    int projectile = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			    Main.projectile[projectile].timeLeft = 200;
			}
			return false;
		}
	
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Boomstick);
	        recipe.AddIngredient(null, "SeaPrism", 5);
			recipe.AddIngredient(null, "PrismShard", 5);
			recipe.AddIngredient(null, "Navystone", 25);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}