using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class AlchemicalFlask : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/AlchemicalFlask");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Alchemical Flask");
		////Tooltip.SetDefault("All attacks inflict the plague\nMakes you immune to the plague\nProjectiles spawn plague seekers on enemy hits");
		Item.width = 26;
		Item.height = 26;
		Item.value = 150000;
		Item.rare = 8;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer.alchFlask = true;
		player.buffImmune[Mod.Find<ModBuff>("Plague").Type] = true;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.BottledWater);
		recipe.AddIngredient(ItemID.Bezoar);
		recipe.AddIngredient(null, "PlagueCellCluster", 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}