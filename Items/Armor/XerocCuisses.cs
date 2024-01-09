using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Legs)]
public class XerocCuisses : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 375000;
        Item.rare = ItemRarityID.Cyan;
        Item.defense = 24;
    }

    public override void UpdateEquip(Player player)
    {
    	player.GetCritChance(DamageClass.Melee) += 7;
    	player.GetCritChance(DamageClass.Ranged) += 7;
    	player.GetCritChance(DamageClass.Throwing) += 7;
    	player.GetCritChance(DamageClass.Magic) += 7;
    	player.moveSpeed += 0.2f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "MeldiateBar", 11);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}}