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
	public class Scorpion : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scorpio");
			// Tooltip.SetDefault("Rockets\nRight click to change modes");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 95;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 58;
	        Item.height = 26;
	        Item.useTime = 13;
	        Item.useAnimation = 13;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 6.5f;
            Item.value = Item.buyPrice(0, 95, 0, 0);
            Item.rare = 9;
	        Item.UseSound = SoundID.Item11;
	        Item.autoReuse = true;
	        Item.shootSpeed = 20f;
	        Item.shoot = Mod.Find<ModProjectile>("MiniRocket").Type;
	        Item.useAmmo = 771;
	    }
	    
	    public override bool AltFunctionUse(Player player)
		{
			return true;
		}
	    
	    public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.useTime = 39;
	        	Item.useAnimation = 39;
			}
			else
			{
				Item.useTime = 13;
	        	Item.useAnimation = 13;
			}
			return base.CanUseItem(player);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	if (player.altFunctionUse == 2)
	    	{
	    		Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("BigNuke").Type, (int)((double)damage * 3.0), knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
	    	else
	    	{
	    		Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("MiniRocket").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.SnowmanCannon);
	        recipe.AddIngredient(ItemID.GrenadeLauncher);
	        recipe.AddIngredient(ItemID.RocketLauncher);
	        recipe.AddIngredient(ItemID.FragmentVortex, 20);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}