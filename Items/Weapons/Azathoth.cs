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
	public class Azathoth : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Azathoth");
			// Tooltip.SetDefault("Destroy the universe in the blink of an eye\nFires cosmic orbs that blast nearby enemies with lasers");
		}

	    public override void SetDefaults()
	    {
	    	Item.CloneDefaults(ItemID.Kraken);
	        Item.damage = 200;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = 5;
	        Item.channel = true;
	        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
	        Item.knockBack = 6f;
	        Item.value = Item.buyPrice(5, 0, 0, 0);
            Item.rare = 10;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("AzathothProjectile").Type;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 16;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	        Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
			return false;
		}
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Terrarian);
	        recipe.AddIngredient(null, "ShadowspecBar", 5);
	        recipe.AddIngredient(null, "CoreofCalamity", 3);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
		}
	}
}