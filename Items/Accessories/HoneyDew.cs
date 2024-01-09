using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class HoneyDew : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.value = 500000;
		Item.rare = ItemRarityID.Lime;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.beeResist = true;
		if (player.ZoneJungle)
		{
			player.lifeRegen += 2;
			player.statDefense += 5;
			player.endurance += 0.1f;
		}
		player.buffImmune[70] = true;
		player.buffImmune[20] = true;
		if (!player.honey && player.lifeRegen < 0)
		{
			player.lifeRegen += 4;
			if (player.lifeRegen > 0)
			{
				player.lifeRegen = 0;
			}
		}
		player.lifeRegenTime += 2;
		player.lifeRegen += 2;
	}
	
	public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "LivingDew");
        recipe.AddIngredient(ItemID.BottledHoney, 10);
        recipe.AddIngredient(ItemID.BeeWax, 10);
        recipe.AddIngredient(ItemID.Bezoar);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}