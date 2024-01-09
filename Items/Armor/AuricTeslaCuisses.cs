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
public class AuricTeslaCuisses : ModItem
{

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 8750000;
        Item.defense = 44;
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
	{
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
            }
        }
    }

    public override void UpdateEquip(Player player)
    {
    	player.moveSpeed += 0.5f;
    	player.carpet = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "SilvaLeggings");
        recipe.AddIngredient(null, "GodSlayerLeggings");
        recipe.AddIngredient(null, "BloodflareCuisses");
        recipe.AddIngredient(null, "TarragonLeggings");
        recipe.AddIngredient(null, "EndothermicEnergy", 300);
        recipe.AddIngredient(null, "NightmareFuel", 300);
        recipe.AddIngredient(null, "Phantoplasm", 105);
        recipe.AddIngredient(null, "DarksunFragment", 45);
        recipe.AddIngredient(null, "BarofLife", 30);
        recipe.AddIngredient(null, "CoreofCalamity", 20);
        recipe.AddIngredient(null, "GalacticaSingularity", 15);
        recipe.AddIngredient(ItemID.FlyingCarpet);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
    }
}}