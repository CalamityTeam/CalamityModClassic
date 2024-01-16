using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class VictideBreastplate : ModItem
{
    
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Victide Breastplate");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("5% damage reduction\nDefense increased while submerged in liquid");
        Item.value = 38000;
        Item.rare = 2;
        Item.defense = 4; //9
    }

    public override void UpdateEquip(Player player)
    {
    	player.endurance += 0.05f;
    	if(player.wet == true || player.honeyWet == true || player.lavaWet == true)
    	{
        	player.statDefense += 3;
        	player.endurance += 0.1f;
    	}
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VictideBar", 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}