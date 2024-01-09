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
	public class ElementalRay : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Elemental Ray");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 230;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 18;
	        Item.width = 62;
	        Item.height = 62;
	        Item.useTime = 16;
	        Item.useAnimation = 16;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 7.5f;
	        Item.value = 10000000;
	        Item.UseSound = SoundID.Item60;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("ElementRay").Type;
	        Item.shootSpeed = 6f;
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
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "GalacticaSingularity", 5);
	        recipe.AddIngredient(ItemID.LunarBar, 5);
	        recipe.AddIngredient(null, "TerraRay");
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}