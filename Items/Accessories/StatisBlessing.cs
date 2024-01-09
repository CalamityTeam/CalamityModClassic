using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class StatisBlessing : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 32;
		Item.value = 5000000;
		Item.rare = ItemRarityID.Cyan;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.tearMinions = true;
		player.GetKnockback(DamageClass.Summon).Base += 2.5f;
		player.GetDamage(DamageClass.Summon) += 0.15f;
		player.maxMinions += 3;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.PapyrusScarab);
		recipe.AddIngredient(ItemID.PygmyNecklace);
		recipe.AddIngredient(ItemID.SummonerEmblem);
		recipe.AddIngredient(ItemID.BottledWater);
		recipe.AddIngredient(null, "CoreofCinder", 5);
		recipe.AddIngredient(ItemID.HolyWater, 30);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}