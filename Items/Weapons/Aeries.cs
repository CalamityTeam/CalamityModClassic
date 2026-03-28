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
	public class Aeries : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aeries");
			// Tooltip.SetDefault("Their lives are yours");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 35;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 50;
	        Item.height = 32;
	        Item.useTime = 10;
	        Item.useAnimation = 10;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5.5f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
	        Item.UseSound = SoundID.Item41;
	        Item.autoReuse = true;
	        Item.shootSpeed = 24f;
	        Item.shoot = Mod.Find<ModProjectile>("ShockblastRound").Type;
	        Item.useAmmo = 97;
	    }
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("ShockblastRound").Type, damage, knockback, player.whoAmI, 0f, 0f);
	    	return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.SpectreBar, 5);
	        recipe.AddIngredient(null, "CursedCapper");
	        recipe.AddIngredient(ItemID.ShroomiteBar, 5);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}