using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class ShadowspecBar : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/ShadowspecBar");
        return true;
        }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(12, 3));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }


        public override void SetDefaults()
	{
		//Tooltip.SetDefault("Shadowspec Bar");
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 1000000;
		Item.expert = true;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(3);
        recipe.AddIngredient(null, "BarofLife", 3);
        recipe.AddIngredient(null, "ShadowEssence");
        recipe.AddIngredient(null, "HellcasterFragment");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}