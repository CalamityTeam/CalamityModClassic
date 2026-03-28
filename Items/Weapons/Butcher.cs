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
	public class Butcher : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Butcher");
			// Tooltip.SetDefault("Fires faster and more accurately the longer you hold the trigger");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 17;
	        Item.width = 20;
	        Item.height = 12;
	        Item.useTime = 40;
	        Item.useAnimation = 40;
	        Item.useStyle = 5;
	        Item.rare = 5;
	        Item.knockBack = 1f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.UseSound = SoundID.Item38;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.DamageType = DamageClass.Ranged;
			Item.channel = true;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("Butcher").Type;
	        Item.shootSpeed = 12f;
	        Item.useAmmo = 97;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Butcher").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.Shotgun);
	        recipe.AddIngredient(null, "EssenceofChaos", 4);
	        recipe.AddIngredient(null, "EssenceofEleum", 4);
	        recipe.AddIngredient(ItemID.IllegalGunParts);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}