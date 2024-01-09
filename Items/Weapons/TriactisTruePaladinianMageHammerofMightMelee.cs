using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TriactisTruePaladinianMageHammerofMightMelee : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Triactis' True Paladinian Mage-Hammer of Might");
	}

	public override void SetDefaults()
	{
		Item.width = 160;  //The width of the .png file in pixels divided by 2.
		Item.damage = 5000;  //Keep this reasonable please.
		Item.noMelee = true;  //Dictates whether this is a melee-class weapon.
		Item.noUseGraphic = true;
		Item.autoReuse = true;
		Item.useAnimation = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 10;
		Item.knockBack = 50f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 160;  //The height of the .png file in pixels divided by 2.
		Item.value = 100000000;  //Value is calculated in copper coins.
		Item.shoot = Mod.Find<ModProjectile>("TriactisOPHammerMelee").Type;
		Item.shootSpeed = 25f;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(255, 0, 255);
            }
        }
    }
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "TruePaladinsHammerMelee");
		recipe.AddIngredient(ItemID.SoulofMight, 30);
		recipe.AddIngredient(null, "ShadowspecBar", 5);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
	}
}}
