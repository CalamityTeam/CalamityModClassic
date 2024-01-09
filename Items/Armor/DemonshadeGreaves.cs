using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Armor 
{
	[AutoloadEquip(EquipType.Legs)]
	public class DemonshadeGreaves : ModItem
	{
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Demonshade Greaves");
            //Tooltip.SetDefault("Shadow speed");
        }

	    public override void SetDefaults()
	    {
	        Item.width = 18;
	        Item.height = 18;
	        Item.value = 10000000;
	        Item.defense = 57; //15
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
	
	    public override void UpdateEquip(Player player)
	    {
	    	CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
    		modPlayer.shadowSpeed = true;
	        player.moveSpeed += 1f;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "ShadowspecBar", 45);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}