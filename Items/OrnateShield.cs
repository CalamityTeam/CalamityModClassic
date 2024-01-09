using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class OrnateShield : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/OrnateShield");
        return true;
    }
	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Ornate Shield");
		//Tooltip.SetDefault("Increased defense when below 25% life");
		Item.width = 36;
		Item.height = 32;
		Item.value = 150000;
		Item.rare = 5;
		Item.defense = 3;
		Item.accessory = true;
	}
	
	public override void UpdateEquip(Player player)
	{
		if(player.statLife <= (player.statLifeMax2 * 0.25f))
		{
			player.statDefense += 7;
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "VerstaltiteBar", 5);
		recipe.AddIngredient(ItemID.CrystalShard, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}