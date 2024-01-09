using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class VictideLeggings : ModItem
{
    
    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Victide Leggings");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("Movement speed increased");
        //AddTooltip2("Speed greatly increased while submerged in liquid");
        Item.value = 37500;
        Item.rare = 2;
        Item.defense = 4;
    }

    public override void UpdateEquip(Player player)
    {
    	player.moveSpeed += 0.15f;
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