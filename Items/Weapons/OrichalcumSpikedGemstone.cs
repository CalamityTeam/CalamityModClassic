using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class OrichalcumSpikedGemstone : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/OrichalcumSpikedGemstone");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Orichalcum Spiked Gemstone");
		Item.width = 14;  //The width of the .png file in pixels divided by 2.
		Item.damage = 25;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.consumable = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 13;
		Item.scale = 0.75f;
		Item.useStyle = 1;
		Item.useTime = 13;
		Item.knockBack = 2f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 24;  //The height of the .png file in pixels divided by 2.
		Item.shoot = 330;
		Item.maxStack = 999;
		Item.value = 1200;  //Value is calculated in copper coins.
		Item.rare = 4;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("OrichalcumSpikedGemstoneProjectile").Type;
		Item.shootSpeed = 12f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(50);
        recipe.AddIngredient(ItemID.OrichalcumBar);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
