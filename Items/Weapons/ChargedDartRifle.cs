using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using Terraria.Audio;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class ChargedDartRifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Charged Dart Blaster");
			// Tooltip.SetDefault("Right click to fire an exploding energy blast that bounces");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 126;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 60;
	        Item.height = 24;
	        Item.useTime = 25;
	        Item.useAnimation = 25;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 7f;
            Item.value = Item.buyPrice(0, 95, 0, 0);
            Item.rare = 9;
	        Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/LaserCannon");
	        Item.autoReuse = true;
	        Item.shootSpeed = 22f;
	        Item.shoot = Mod.Find<ModProjectile>("ChargedBlast").Type;
	        Item.useAmmo = 283;
	    }
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool AltFunctionUse(Player player)
		{
			return true;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	if (player.altFunctionUse == 2)
	    	{
	    		Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("ChargedBlast3").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
	    	else
	    	{
	    		Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("ChargedBlast").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.DartRifle);
	        recipe.AddIngredient(ItemID.MartianConduitPlating, 25);
	        recipe.AddIngredient(null, "CoreofEleum", 3);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	        recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.DartPistol);
	        recipe.AddIngredient(ItemID.MartianConduitPlating, 25);
	        recipe.AddIngredient(null, "CoreofEleum", 3);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}