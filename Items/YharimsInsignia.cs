using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class YharimsInsignia : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/YharimsInsignia");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Yharim's Insignia");
		////Tooltip.SetDefault("25% increased damage when under 50% life\nIncreased melee speed as health lowers\n7% increased melee speed and damage\nMelee attacks inflict holy fire\nIncreased invincibility after taking damage\nYou are immune to lava\nIncreased melee knockback");
		Item.width = 22;
		Item.height = 38;
		Item.value = 5000000;
		Item.rare = 9;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer1Point1 modPlayer = player.GetModPlayer<CalamityPlayer1Point1>();
		modPlayer.yInsignia = true;
		player.longInvince = true;
		player.kbGlove = true;
		player.GetDamage(DamageClass.Melee) *= 1.07f;
		player.GetAttackSpeed(DamageClass.Melee) *= 1.07f;
		player.lavaImmune = true;
		if(player.statLife <= (player.statLifeMax2 * 0.5f))
		{
			player.GetDamage(DamageClass.Melee) *= 1.25f;
			player.GetDamage(DamageClass.Magic) *= 1.25f;
			player.GetDamage(DamageClass.Ranged) *= 1.25f;
			player.GetDamage(DamageClass.Throwing) *= 1.25f;
			player.GetDamage(DamageClass.Summon) *= 1.25f;
		}
		if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.GetAttackSpeed(DamageClass.Melee) *= 1.2f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.GetAttackSpeed(DamageClass.Melee) *= 1.4f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.GetAttackSpeed(DamageClass.Melee) *= 1.6f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.GetAttackSpeed(DamageClass.Melee) *= 1.8f;
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.FireGauntlet);
		recipe.AddIngredient(null, "NecklaceofVexation");
		recipe.AddIngredient(null, "CoreofCinder", 5);
		recipe.AddIngredient(ItemID.CrossNecklace);
		recipe.AddIngredient(null, "BadgeofBravery");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}