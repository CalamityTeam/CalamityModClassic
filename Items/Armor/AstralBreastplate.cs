using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Body)]
public class AstralBreastplate : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 470000;
        Item.rare = ItemRarityID.Lime;
        Item.defense = 23;
    }

    public override void UpdateEquip(Player player)
    {
    	player.statLifeMax2 += 30;
        player.statManaMax2 += 30;
        player.maxMinions += 2;
        player.detectCreature = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AstralBar", 12);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}