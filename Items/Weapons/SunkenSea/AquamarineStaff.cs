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
	public class AquamarineStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aquamarine Staff");
			// Tooltip.SetDefault("Shoots two blue bolts");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 10;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 3;
	        Item.width = 38;
	        Item.height = 38;
	        Item.useTime = 15;
	        Item.useAnimation = 15;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 2.5f;
			Item.value = Item.buyPrice(0, 2, 0, 0);
			Item.rare = 2;
	        Item.UseSound = SoundID.Item43;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("AquamarineBolt").Type;
	        Item.shootSpeed = 9f;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	    	int num6 = Main.rand.Next(2, 3);
			for (int index = 0; index < num6; ++index)
			{
			    float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
			    float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
			    int projectile = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			    Main.projectile[projectile].timeLeft = 180;
			}
			return false;
		}
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.RubyStaff);
			recipe.AddIngredient(null, "SeaPrism", 5);
			recipe.AddIngredient(null, "Navystone", 25);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DiamondStaff);
			recipe.AddIngredient(null, "SeaPrism", 5);
			recipe.AddIngredient(null, "Navystone", 25);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
	    }
	}
}