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
public class StatigelArmor : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Statigel Armor");
        //Tooltip.SetDefault("5% increased critical strike chance");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 150000;
        Item.rare = ItemRarityID.Pink;
        Item.defense = 10;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetCritChance(DamageClass.Melee) += 5;
		player.GetCritChance(DamageClass.Magic) += 5;
		player.GetCritChance(DamageClass.Ranged) += 5;
		player.GetCritChance(DamageClass.Throwing) += 5;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "PurifiedGel", 8);
        recipe.AddIngredient(ItemID.HellstoneBar, 13);
		recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}