using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.GreatSandShark
{
	public class Sandstorm : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sandstorm");
			// Tooltip.SetDefault("Fires sand bullets that explode");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 73;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 62;
	        Item.height = 26;
	        Item.useTime = 15;
	        Item.useAnimation = 15;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
	        Item.UseSound = SoundID.Item11;
	        Item.autoReuse = true;
	        Item.shootSpeed = 12f;
	        Item.shoot = Mod.Find<ModProjectile>("SandstormBullet").Type;
	        Item.useAmmo = AmmoID.Sand;
	    }
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("SandstormBullet").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.Sandgun);
	        recipe.AddIngredient(null, "GrandScale");
	        recipe.AddIngredient(ItemID.Amber, 5);
            recipe.AddIngredient(ItemID.SandBlock, 50);
            recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}