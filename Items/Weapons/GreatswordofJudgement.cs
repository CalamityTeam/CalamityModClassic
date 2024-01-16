using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class GreatswordofJudgement : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/GreatswordofJudgement");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Greatsword of Judgement");
		Item.width = 70;  //The width of the .png file in pixels divided by 2.
		Item.damage = 100;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 18;
		Item.useStyle = 1;
		Item.useTime = 18;
		Item.useTurn = true;
		////Tooltip.SetDefault("A pale white sword from a forgotten land\nYou can hear faint yet comforting whispers emanating from the blade\n'No matter where you may be you are never alone.\nI shall always be at your side, my lord'");
		Item.knockBack = 7f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 72;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 5000000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("Judgement").Type;
		Item.shootSpeed = 10f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.LunarBar, 11);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    
}}
