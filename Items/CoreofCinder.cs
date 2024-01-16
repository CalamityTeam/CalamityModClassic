using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class CoreofCinder : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/CoreofCinder");
        return true;
        }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 14));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }

        public override void SetDefaults()
	{
		//Tooltip.SetDefault("Core of Cinder");
		Item.width = 16;
		Item.height = 16;
		Item.maxStack = 999;
		Item.value = 50000;
		Item.rare = 8;
	}
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed *= 0.0001f;
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.3f * num, 0.3f * num, 0.05f * num);
    }
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(3);
		recipe.AddIngredient(null, "EssenceofCinder");
        recipe.AddIngredient(ItemID.Ectoplasm);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}