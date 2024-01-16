using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class GalacticaSingularity : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/GalacticaSingularity");
        return true;
        }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 19));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }


        public override void SetDefaults()
	{
		//Tooltip.SetDefault("Galactica Singularity");
		////Tooltip.SetDefault("A shard of the cosmos");
		Item.width = 28;
		Item.height = 28;
		Item.maxStack = 69;
		Item.value = 500000;
		Item.rare = 10;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.FragmentSolar, 3);
		recipe.AddIngredient(ItemID.FragmentVortex, 3);
		recipe.AddIngredient(ItemID.FragmentStardust, 3);
		recipe.AddIngredient(ItemID.FragmentNebula, 3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed *= 0.0001f;
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 1f * num, 0.3f * num, 0.3f * num);
    }
}}