using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class SpearofPaleolith : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/SpearofPaleolith");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Spear of Paleolith");
		Item.width = 54;  //The width of the .png file in pixels divided by 2.
		Item.damage = 52;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 35;
		////Tooltip.SetDefault("Throws an ancient spear that shatters enemy armor at high velocity\nSpears rain fossil shards as they travel");
		Item.useStyle = 1;
		Item.useTime = 35;
		Item.knockBack = 6f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 54;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 330000;  //Value is calculated in copper coins.
		Item.rare = 5;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("SpearofPaleolith").Type;
		Item.shootSpeed = 35f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
		recipe.AddIngredient(ItemID.AdamantiteBar, 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
		recipe.AddIngredient(ItemID.TitaniumBar, 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}
