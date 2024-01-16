using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Galeforce : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Galeforce");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Galeforce");
        Item.damage = 16;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 22;
        Item.height = 40;
        Item.useTime = 19;
        Item.useAnimation = 19;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4;
        Item.value = 75000;
        Item.rare = 3;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 20f;
        Item.useAmmo = 40;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 8);
        recipe.AddIngredient(ItemID.SunplateBlock, 3);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
    }
}}