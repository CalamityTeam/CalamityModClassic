using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class ChaosAmulet : ModItem
{	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Chaos Amulet");
		//Tooltip.SetDefault("Attacking enemies will heal the player and set enemies on fire");
		Item.width = 20;
		Item.height = 24;
		Item.lifeRegen = 2;
		Item.value = 150000;
		Item.rare = 7;
		Item.accessory = true;
	}
	
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		player.statLife += 2;
		target.AddBuff(BuffID.OnFire, 300);
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CruptixBar", 2);
		recipe.AddIngredient(ItemID.MagmaStone);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
	}
}}