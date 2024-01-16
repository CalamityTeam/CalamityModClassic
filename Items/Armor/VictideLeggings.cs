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
    [AutoloadEquip(EquipType.Legs)]
    public class VictideLeggings : ModItem
{
    
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Victide Leggings");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("Movement speed increased by 8%\nSpeed greatly increased while submerged in liquid");
        Item.value = 37500;
        Item.rare = 2;
        Item.defense = 3; //9
    }

    public override void UpdateEquip(Player player)
    {
    	player.moveSpeed += 0.08f;
    	if(player.wet == true || player.honeyWet == true || player.lavaWet == true)
    	{
        	player.moveSpeed += 0.5f;
    	}
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VictideBar", 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}