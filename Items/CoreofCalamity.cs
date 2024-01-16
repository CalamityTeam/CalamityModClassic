using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class CoreofCalamity : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/CoreofCalamity");
        return true;
        }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 16));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }

        public override void SetDefaults()
	{
		//Tooltip.SetDefault("Core of Calamity");
		Item.width = 34;
		Item.height = 40;
		Item.maxStack = 69;
		Item.value = 500000;
		Item.rare = 10;
	}
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed *= 0.0001f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CoreofCinder", 5);
        recipe.AddIngredient(null, "CoreofEleum", 5);
        recipe.AddIngredient(null, "CoreofChaos", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}