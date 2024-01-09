using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Legs)]
public class AtaxiaSubligar : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 412500;
        Item.rare = ItemRarityID.Yellow;
        Item.defense = 15;
    }

    public override void UpdateEquip(Player player)
    {
    	player.GetCritChance(DamageClass.Melee) += 7;
		player.GetCritChance(DamageClass.Magic) += 7;
		player.GetCritChance(DamageClass.Ranged) += 7;
		player.GetCritChance(DamageClass.Throwing) += 7;
    	player.moveSpeed += 0.15f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CruptixBar", 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}