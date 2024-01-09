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
	public class T1000 : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("T1000");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 223;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 12;
	        Item.width = 20;
	        Item.height = 12;
	        Item.useTime = 12;
	        Item.useAnimation = 12;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.noUseGraphic = true;
			Item.channel = true;
	        Item.knockBack = 4f;
	        Item.value = 10000000;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("T1000").Type;
	        Item.shootSpeed = 24f;
	    }
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(43, 96, 222);
	            }
	        }
	    }
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Purge");
			recipe.AddIngredient(null, "Phantoplasm", 5);
	        recipe.AddIngredient(null, "CosmiliteBar", 5);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("T1000").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}