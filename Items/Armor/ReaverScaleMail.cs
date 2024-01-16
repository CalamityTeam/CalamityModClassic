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
    public class ReaverScaleMail : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Reaver Scale Mail");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("5% increased melee damage and crit chance\nProvides life regeneration\n+100 max life");
        Item.lifeRegen = 2;
        Item.value = 300000;
        Item.rare = 6;
        Item.defense = 15; //51
    }

    public override void UpdateEquip(Player player)
    {
    	player.statLifeMax2 += 100;
        player.GetDamage(DamageClass.Melee) += 0.05f;
        player.GetCritChance(DamageClass.Melee) += 5;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DraedonBar", 15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}