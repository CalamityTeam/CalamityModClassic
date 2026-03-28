using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Armor 
{
	[AutoloadEquip(EquipType.Legs)]
	public class DemonshadeGreaves : ModItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Demonshade Greaves");
            // Tooltip.SetDefault("Shadow speed");
        }

	    public override void SetDefaults()
	    {
	        Item.width = 18;
	        Item.height = 18;
			Item.value = Item.buyPrice(3, 0, 0, 0);
			Item.defense = 50; //15
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 16;
		}
	
	    public override void UpdateEquip(Player player)
	    {
	    	CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
    		modPlayer.shadowSpeed = true;
	        player.moveSpeed += 1f;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "ShadowspecBar", 11);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}