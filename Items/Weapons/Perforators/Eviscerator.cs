using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Perforators
{
	public class Eviscerator : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eviscerator");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 65;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 58;
	        Item.height = 22;
	        Item.crit += 25;
	        Item.useTime = 60;
	        Item.useAnimation = 60;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 7.5f;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
	        Item.UseSound = SoundID.Item40;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("BloodClotFriendly").Type;
	        Item.shootSpeed = 22f;
	        Item.useAmmo = 97;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("BloodClotFriendly").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
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