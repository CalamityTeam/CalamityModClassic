using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items.Armor;

namespace CalamityModClassic1Point1.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class DemonshadeBreastplate : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Demonshade Breastplate");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("Massively increased melee speed as health lowers");
        Item.value = 5100000;
        Item.expert = true;
        Item.defense = 35;
    }

    public override void UpdateEquip(Player player)
    {
        if(player.statLife <= (player.statLifeMax2 * 0.8f))
		{
			player.GetAttackSpeed(DamageClass.Melee) *= 1.25f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f))
		{
			player.GetAttackSpeed(DamageClass.Melee) *= 1.5f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f))
		{
			player.GetAttackSpeed(DamageClass.Melee) *= 1.75f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.GetAttackSpeed(DamageClass.Melee) *= 2f;
		}
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "ShadowspecBar", 7);
        recipe.AddIngredient(ItemID.SoulofFright, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}