using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class BloodyWormScarf : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Bloody Worm Scarf");
		//Tooltip.SetDefault("15% increased damage reduction and increased melee stats");
	}
	
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 42;
		Item.value = 500000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Melee) += 0.1f;
        player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
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