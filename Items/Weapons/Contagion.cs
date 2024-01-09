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
	public class Contagion : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Contagion");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 800;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 22;
	        Item.height = 50;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.noUseGraphic = true;
			Item.channel = true;
	        Item.knockBack = 5f;
	        Item.value = 10000000;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("Contagion").Type;
	        Item.shootSpeed = 20f;
	        Item.useAmmo = 40;
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
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "ShadowspecBar", 5);
			recipe.AddIngredient(ItemID.Phantasm);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Contagion").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}