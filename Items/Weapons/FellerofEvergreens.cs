using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class FellerofEvergreens : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/FellerofEvergreens");
        return true;
    }
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Feller of Evergreens");
        Item.damage = 15;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 36;
        Item.height = 36;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useTurn = true;
        Item.axe = 15;
        Item.useStyle = 1;
        Item.knockBack = 5;
        Item.value = 30000;
        Item.rare = 2;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Wood, 15);
        recipe.AddIngredient(ItemID.TungstenBar, 10);
        recipe.AddIngredient(ItemID.TungstenAxe);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Wood, 15);
        recipe.AddIngredient(ItemID.SilverBar, 10);
        recipe.AddIngredient(ItemID.SilverAxe);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}