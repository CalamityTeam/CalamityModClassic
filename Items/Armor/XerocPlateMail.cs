using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items.Armor;

namespace CalamityModClassic1Point1.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class XerocPlateMail : ModItem
{
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Xeroc Plate Mail");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("+80 max mana\n+150 max life\n12% increased movement speed\nArmor of the cosmos");
        Item.value = 425000;
        Item.rare = 9;
        Item.defense = 22; //62
    }

    public override void UpdateEquip(Player player)
    {
    	player.statLifeMax2 += 150;
        player.statManaMax2 += 80;
        player.moveSpeed += 0.12f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "MeldiateBar", 15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}