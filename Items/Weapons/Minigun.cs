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
	public class Minigun : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Minigun");
			// Tooltip.SetDefault("80% chance to not consume ammo");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 390;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 72;
	        Item.height = 34;
	        Item.useTime = 3;
	        Item.useAnimation = 3;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item41;
	        Item.autoReuse = true;
	        Item.shoot = 10;
	        Item.shootSpeed = 22f;
	        Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float SpeedX = velocity.X + (float) Main.rand.Next(-15, 16) * 0.05f;
		    float SpeedY = velocity.Y + (float) Main.rand.Next(-15, 16) * 0.05f;
		    Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    return false;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 80)
	    		return false;
	    	return true;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.ChainGun);
	        recipe.AddIngredient(null, "CosmiliteBar", 5);
	        recipe.AddIngredient(null, "Phantoplasm", 5);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}