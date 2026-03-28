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
	public class Purge : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Nano Purge");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 70;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 6;
	        Item.width = 20;
	        Item.height = 12;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.noUseGraphic = true;
			Item.channel = true;
	        Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 95, 0, 0);
            Item.rare = 9;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("Purge").Type;
	        Item.shootSpeed = 24f;
	    }
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.FragmentVortex, 20);
			recipe.AddIngredient(ItemID.LaserMachinegun);
			recipe.AddIngredient(ItemID.Nanites, 100);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Purge").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}