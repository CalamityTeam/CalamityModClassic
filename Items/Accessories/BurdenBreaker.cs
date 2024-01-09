using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class BurdenBreaker : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Determination Breaker");
		//Tooltip.SetDefault("The bad time\nRemoves immunity frames\nEnemies take damage quickly over time\nEnemies below 200 HP are not effected by the life drain\nIf you want a crazy challenge, equip this");
	}
	
	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 28;
		Item.value = 150000;
		Item.rare = ItemRarityID.Orange;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (player.immune)
		{
			player.immune = false;
		}
		for (int l = 0; l < 200; l++)
		{
			NPC nPC = Main.npc[l];
			if (nPC.lifeMax > 200 && nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage)
			{
				nPC.life--;
				nPC.HitEffect();
				if (nPC.life <= 0)
				{
					nPC.NPCLoot();
				}
			}
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.Bone, 50);
		recipe.AddIngredient(ItemID.IronBar, 7);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.Bone, 50);
		recipe.AddIngredient(ItemID.LeadBar, 7);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}