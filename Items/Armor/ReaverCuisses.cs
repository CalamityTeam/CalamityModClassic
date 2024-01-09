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
public class ReaverCuisses : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Reaver Cuisses");
        //Tooltip.SetDefault("5% increased critical strike chance\n12% increased movement speed");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 312500;
        Item.rare = ItemRarityID.LightPurple;
        Item.defense = 14;
    }

    public override void UpdateEquip(Player player)
    {
    	player.GetCritChance(DamageClass.Melee) += 5;
		player.GetCritChance(DamageClass.Magic) += 5;
		player.GetCritChance(DamageClass.Ranged) += 5;
		player.GetCritChance(DamageClass.Throwing) += 5;
    	player.moveSpeed += 0.12f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DraedonBar", 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}