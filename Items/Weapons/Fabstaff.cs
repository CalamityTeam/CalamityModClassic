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
	public class Fabstaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Fabstaff");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 999;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 50;
	        Item.width = 84;
	        Item.height = 84;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 5f;
	        Item.value = 100000000;
	        Item.UseSound = SoundID.Item60;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("FabRay").Type;
	        Item.shootSpeed = 6f;
	    }
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(Main.DiscoR, 100, 255);
	            }
	        }
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "Phantoplasm", 100);
	        recipe.AddIngredient(null, "ShadowspecBar", 50);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}