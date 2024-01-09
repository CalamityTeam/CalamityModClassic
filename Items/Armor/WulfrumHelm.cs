﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Head)]
public class WulfrumHelm : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Wulfrum Helm");
        //Tooltip.SetDefault("3% increased melee damage");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 20000;
        Item.rare = ItemRarityID.Blue;
        Item.defense = 3; //8
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("WulfrumArmor").Type && legs.type == Mod.Find<ModItem>("WulfrumLeggings").Type;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "+3 defense\n" +
        	"+5 defense when below 50% life";
        player.statDefense += 3; //11
        if(player.statLife <= (player.statLifeMax2 * 0.5f))
    	{
        	player.statDefense += 5; //16
        }
    }
    
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Melee) += 0.03f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "WulfrumShard", 10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}