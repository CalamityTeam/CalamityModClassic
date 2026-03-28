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
	public class OnyxChainBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Onyx Chain Blaster");
			// Tooltip.SetDefault("50% chance to not consume ammo");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 58;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 64;
	        Item.height = 32;
	        Item.useTime = 10;
	        Item.useAnimation = 10;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 4.5f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item36;
	        Item.autoReuse = true;
	        Item.shoot = 10;
	        Item.shootSpeed = 24f;
	        Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float SpeedX = velocity.X + (float) Main.rand.Next(-25, 26) * 0.05f;
		    float SpeedY = velocity.Y + (float) Main.rand.Next(-25, 26) * 0.05f;
		    Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX * 0.9f, SpeedY * 0.9f, 661, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    for (int i = 0; i <= 3; i++)
		    {
		    	float SpeedNewX = velocity.X + (float) Main.rand.Next(-45, 46) * 0.05f;
		    	float SpeedNewY = velocity.Y + (float) Main.rand.Next(-45, 46) * 0.05f;
		    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedNewX, SpeedNewY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    }
		    return false;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 50)
	    		return false;
	    	return true;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.OnyxBlaster);
	        recipe.AddIngredient(ItemID.ChainGun);
	        recipe.AddIngredient(ItemID.LunarBar, 5);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}