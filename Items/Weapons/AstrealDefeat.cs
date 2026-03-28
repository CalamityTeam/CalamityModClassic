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
	public class AstrealDefeat : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Astreal Defeat");
			/* Tooltip.SetDefault("Fires Astreal Arrows\n" +
	                   "Ethereal bow of the tyrant king's mother\n" +
	                   "The mother strongly discouraged acts of violence throughout her life\n" +
	                   "Though she kept this bow close to protect her family in times of great disaster"); */
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 109;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 44;
	        Item.height = 54;
	        Item.useTime = 3;
	        Item.useAnimation = 15;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5.5f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item102;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("AstrealArrow").Type;
	        Item.shootSpeed = 1f;
	        Item.useAmmo = 40;
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
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("AstrealArrow").Type, (int)((double)damage), knockback, player.whoAmI, 0f, 0f);
	    	return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.SpiritFlame);
	        recipe.AddIngredient(ItemID.ShadowFlameBow);
	        recipe.AddIngredient(null, "GreatbowofTurmoil");
	        recipe.AddIngredient(null, "BladedgeGreatbow");
	        recipe.AddIngredient(null, "DarkechoGreatbow");
	        recipe.AddIngredient(null, "GalacticaSingularity", 5);
	        recipe.AddIngredient(ItemID.LunarBar, 5);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}