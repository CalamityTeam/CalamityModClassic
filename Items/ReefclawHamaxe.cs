using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class ReefclawHamaxe : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/ReefclawHamaxe");
        return true;
    }
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Reefclaw Hamaxe");
        Item.damage = 15;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 28;
        Item.height = 24;
        Item.useTime = 19;
        Item.useAnimation = 19;
        Item.useTurn = true;
        Item.axe = 13;
        Item.hammer = 50;
        Item.useStyle = 1;
        Item.knockBack = 4;
        Item.value = 15000;
        Item.rare = 2;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VictideBar", 2);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}