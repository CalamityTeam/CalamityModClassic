using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class Megalodon : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Megalodon");
			//Tooltip.SetDefault("50% chance to not consume ammo");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 27;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 72;
	        Item.height = 32;
	        Item.useTime = 6;
	        Item.useAnimation = 6;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 2.5f;
	        Item.value = 750000;
	        Item.rare = ItemRarityID.Yellow;
	        Item.UseSound = SoundID.Item11;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 16f;
	        Item.useAmmo = 97;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float SpeedX = velocity.X + (float) Main.rand.Next(-20, 21) * 0.05f;
		    float SpeedY = velocity.Y + (float) Main.rand.Next(-20, 21) * 0.05f;
		    Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    return false;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 50)
	    		return false;
	    	return true;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.Megashark);
	        recipe.AddIngredient(null, "CoreofEleum", 7);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}