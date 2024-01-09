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
	public class Azathoth : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Azathoth");
			//Tooltip.SetDefault("Destroy the universe in the blink of an eye\nFires cosmic orbs that blast nearby enemies with lasers");
		}

	    public override void SetDefaults()
	    {
	    	Item.CloneDefaults(ItemID.Kraken);
	        Item.damage = 250;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.channel = true;
	        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
	        Item.knockBack = 6f;
	        Item.value = 10000000;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("AzathothProjectile").Type;
	    }
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(255, 0, 255);
	            }
	        }
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	        Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
			return false;
		}
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Terrarian);
	        recipe.AddIngredient(null, "ShadowspecBar", 5);
	        recipe.AddIngredient(null, "CoreofCalamity", 3);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
		}
	}
}