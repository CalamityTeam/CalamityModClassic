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
	public class OnyxChainBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Onyx Chain Blaster");
			//Tooltip.SetDefault("50% chance to not consume ammo");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 95;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 64;
	        Item.height = 32;
	        Item.useTime = 10;
	        Item.useAnimation = 10;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 4.5f;
	        Item.value = 1750000;
	        Item.UseSound = SoundID.Item36;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 24f;
	        Item.useAmmo = 97;
	    }
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(0, 255, 200);
	            }
	        }
	    }
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float SpeedX = velocity.X + (float) Main.rand.Next(-25, 26) * 0.05f;
		    float SpeedY = velocity.Y + (float) Main.rand.Next(-25, 26) * 0.05f;
		    Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, 661, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    for (int i = 0; i <= 3; i++)
		    {
		    	float SpeedNewX = velocity.X + (float) Main.rand.Next(-45, 46) * 0.05f;
		    	float SpeedNewY = velocity.Y + (float) Main.rand.Next(-45, 46) * 0.05f;
		    	Projectile.NewProjectile(source, position.X, position.Y, SpeedNewX, SpeedNewY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    }
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
	        recipe.AddIngredient(ItemID.OnyxBlaster);
	        recipe.AddIngredient(ItemID.ChainGun);
	        recipe.AddIngredient(ItemID.LunarBar, 5);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}