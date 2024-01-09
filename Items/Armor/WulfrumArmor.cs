using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Body)]
public class WulfrumArmor : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Wulfrum Armor");
        //Tooltip.SetDefault("3% increased critical strike chance");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 18000;
        Item.rare = ItemRarityID.Blue;
        Item.defense = 2;
    }

    public override void UpdateEquip(Player player)
    {
    	player.GetCritChance(DamageClass.Melee) += 3;
		player.GetCritChance(DamageClass.Magic) += 3;
		player.GetCritChance(DamageClass.Ranged) += 3;
		player.GetCritChance(DamageClass.Throwing) += 3;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "WulfrumShard", 20);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}