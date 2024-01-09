using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items.Armor;

namespace CalamityModClassic1Point0.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class TarragonLeggings : ModItem
    { 

    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Tarragon Leggings");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("Provides multiple movement stat boosts; greater boost if health is lower");
        //AddTooltip2("Leggings of a fabled explorer");
        Item.value = 462500;
        Item.rare = 8;
        Item.defense = 18;
    }

    public override void UpdateEquip(Player player)
    {
    	player.moveSpeed += 0.25f;
    	if(player.statLife <= (player.statLifeMax2 * 0.5f))
        {
        	player.moveSpeed += 0.15f;
    	}
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "UeliaceBar", 11);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
    }
}}