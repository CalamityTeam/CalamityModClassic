using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class GreatbayPickaxe : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Greatbay Pickaxe");
	}
		
    public override void SetDefaults()
    {
        Item.damage = 13;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 32;
        Item.height = 32;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.useTurn = true;
        Item.pick = 60;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 2;
        Item.value = 57000;
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VictideBar", 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}