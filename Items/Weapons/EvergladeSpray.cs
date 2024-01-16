using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class EvergladeSpray : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/EvergladeSpray");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Everglade Spray");
        Item.damage = 28;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 8;
        Item.width = 30;
        Item.height = 30;
        Item.useTime = 6;
        Item.useAnimation = 18;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Fires a stream of burning green ichor");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4.5f;
        Item.value = 550000;
        Item.rare = 6;
        Item.UseSound = SoundID.Item13;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("EvergladeSprayProjectile").Type;
        Item.shootSpeed = 10f;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.GoldenShower);
		recipe.AddIngredient(null, "DraedonBar", 3);
        recipe.AddTile(TileID.Bookcases);
        recipe.Register();
	}
}}