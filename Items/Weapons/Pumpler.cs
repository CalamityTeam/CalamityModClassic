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
	public class Pumpler : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pumpler");
			// Tooltip.SetDefault("33% chance to not consume ammo");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 9;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 50;
	        Item.height = 28;
	        Item.useTime = 9;
	        Item.useAnimation = 9;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 1.25f;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 2;
	        Item.UseSound = SoundID.Item11;
	        Item.autoReuse = true;
	        Item.shoot = 10;
	        Item.shootSpeed = 11f;
	        Item.useAmmo = 97;
	    }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float SpeedX = velocity.X + (float) Main.rand.Next(-10, 11) * 0.05f;
		    float SpeedY = velocity.Y + (float) Main.rand.Next(-10, 11) * 0.05f;
		    Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    return false;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 33)
	    		return false;
	    	return true;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.Pumpkin, 30);
	        recipe.AddIngredient(ItemID.PumpkinSeed, 5);
	        recipe.AddIngredient(ItemID.IllegalGunParts);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	    }
	}
}