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
    public class DaedalusLeggings : ModItem
{

    public override void SetDefaults()
    {
        //Item.name = "Daedalus Leggings");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("10% increased ranged critical strike chance and 17% increased movement speed");
        Item.value = 262500;
        Item.rare = 5;
        Item.defense = 12;
    }

    public override void UpdateEquip(Player player)
    {
    	player.GetCritChance(DamageClass.Ranged) += 10;
    	player.moveSpeed += 0.17f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VerstaltiteBar", 10);
		recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}