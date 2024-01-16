using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class StreamGouge : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/StreamGouge");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Stream Gouge");
		Item.width = 86;  //The width of the .png file in pixels divided by 2.
		Item.damage = 350;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.useTurn = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 19;
		////Tooltip.SetDefault("Fires an essence flame beam\nThis spear ignores npc immunity frames");
		Item.useStyle = 5;
		Item.useTime = 19;
		Item.knockBack = 9.75f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item20;
		Item.autoReuse = false;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 86;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 1350000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("StreamGouge").Type;
		Item.shootSpeed = 11f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CosmiliteBar", 14);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
