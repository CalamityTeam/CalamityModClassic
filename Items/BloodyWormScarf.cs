using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class BloodyWormScarf : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/BloodyWormScarf");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Bloody Worm Scarf");
		////Tooltip.SetDefault("15% increased damage reduction and increased melee stats");
		Item.width = 26;
		Item.height = 42;
		Item.value = 500000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Melee) *= 1.1f;
        player.GetAttackSpeed(DamageClass.Melee) *= 1.1f;
	    player.endurance += 0.15f;
	}
	
	public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "BloodyWormTooth");
        recipe.AddIngredient(ItemID.WormScarf);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}