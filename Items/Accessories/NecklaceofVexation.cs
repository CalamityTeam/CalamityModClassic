using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class NecklaceofVexation : ModItem
{
	
	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 28;
		Item.value = 150000;
		Item.rare = ItemRarityID.LightPurple;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if(player.statLife < (player.statLifeMax2 * 0.5f))
		{
			player.GetDamage(DamageClass.Melee) += 0.15f;
			player.GetDamage(DamageClass.Magic) += 0.15f;
			player.GetDamage(DamageClass.Ranged) += 0.15f;
			player.GetDamage(DamageClass.Throwing) += 0.15f;
			player.GetDamage(DamageClass.Summon) += 0.15f;
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "DraedonBar", 2);
		recipe.AddIngredient(ItemID.AvengerEmblem);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}