using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Grax : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/Grax");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Grax");
		Item.width = 60;  //The width of the .png file in pixels divided by 2.
		Item.damage = 350;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 25;
		Item.useStyle = 1;
		Item.useTime = 5;
		Item.useTurn = true;
		Item.axe = 50;
		Item.hammer = 200;
		////Tooltip.SetDefault("Hitting an enemy will greatly boost your defense and melee stats for a short time");
		Item.knockBack = 8f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 60;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 5000000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "FellerofEvergreens");
		recipe.AddIngredient(null, "DraedonBar", 5);
		recipe.AddRecipeGroup("LunarAxe");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		player.AddBuff(Mod.Find<ModBuff>("GraxDefense").Type, 480);
	}
}}
