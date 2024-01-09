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
public class DaedalusLeggings : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Daedalus Leggings");
        //Tooltip.SetDefault("4% increased critical strike chance\n10% increased movement speed");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 262500;
        Item.rare = ItemRarityID.Pink;
        Item.defense = 13; //41
    }

    public override void UpdateEquip(Player player)
    {
    	player.GetCritChance(DamageClass.Melee) += 4;
		player.GetCritChance(DamageClass.Magic) += 4;
		player.GetCritChance(DamageClass.Ranged) += 4;
		player.GetCritChance(DamageClass.Throwing) += 4;
    	player.moveSpeed += 0.1f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VerstaltiteBar", 10);
		recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}