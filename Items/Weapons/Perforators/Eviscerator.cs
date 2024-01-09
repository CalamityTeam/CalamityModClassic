using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Perforators
{
	public class Eviscerator : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Eviscerator");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 57;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 58;
	        Item.height = 22;
	        Item.crit += 25;
	        Item.useTime = 60;
	        Item.useAnimation = 60;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 7.5f;
	        Item.value = 80000;
	        Item.rare = ItemRarityID.Orange;
	        Item.UseSound = SoundID.Item40;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("BloodClotFriendly").Type;
	        Item.shootSpeed = 22f;
	        Item.useAmmo = 97;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("BloodClotFriendly").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "BloodSample", 8);
	        recipe.AddIngredient(ItemID.Vertebrae, 4);
	        recipe.AddIngredient(ItemID.CrimtaneBar, 4);
	        recipe.AddTile(TileID.DemonAltar);
	        recipe.Register();
		}
	}
}