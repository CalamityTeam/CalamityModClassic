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
	public class Spyker : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Spyker");
			// Tooltip.SetDefault("Fires spikes that stick to enemies/tiles and explode into shrapnel that also stick to enemies/tiles and explode");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 170;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 44;
	        Item.height = 26;
	        Item.useTime = 13;
	        Item.useAnimation = 13;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 6f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item108;
	        Item.autoReuse = true;
	        Item.shootSpeed = 9f;
	        Item.shoot = Mod.Find<ModProjectile>("Spyker").Type;
	        Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Spyker").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "Needler");
	        recipe.AddIngredient(ItemID.Stynger);
	        recipe.AddIngredient(null, "UeliaceBar", 5);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}