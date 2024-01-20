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
public class AuricTeslaBodyArmor : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 9250000;
        Item.defense = 48;
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
    	CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
		modPlayer.fBarrier = true;
    	player.statLifeMax2 += 400;
        player.statManaMax2 += 400;
        player.moveSpeed += 0.25f;
        player.GetCritChance(DamageClass.Melee) += 15;
		player.GetCritChance(DamageClass.Magic) += 15;
		player.GetCritChance(DamageClass.Ranged) += 15;
		player.GetCritChance(DamageClass.Throwing) += 15;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "SilvaArmor");
        recipe.AddIngredient(null, "GodSlayerChestplate");
        recipe.AddIngredient(null, "BloodflareBodyArmor");
        recipe.AddIngredient(null, "TarragonBreastplate");
        recipe.AddIngredient(null, "EndothermicEnergy", 400);
        recipe.AddIngredient(null, "NightmareFuel", 400);
        recipe.AddIngredient(null, "Phantoplasm", 140);
        recipe.AddIngredient(null, "DarksunFragment", 60);
        recipe.AddIngredient(null, "BarofLife", 40);
        recipe.AddIngredient(null, "CoreofCalamity", 30);
        recipe.AddIngredient(null, "GalacticaSingularity", 20);
        recipe.AddIngredient(null, "FrostBarrier");
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
    }
}}