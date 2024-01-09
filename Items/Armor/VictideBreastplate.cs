using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Body)]
public class VictideBreastplate : ModItem
{

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 38000;
        Item.rare = ItemRarityID.Green;
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