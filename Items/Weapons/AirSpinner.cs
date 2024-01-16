using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class AirSpinner : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/AirSpinner");
		return true;
	}
	
    public override void SetDefaults()
    {
    	Item.CloneDefaults(ItemID.Valor);
        //Tooltip.SetDefault("Air Spinner");
        Item.damage = 21;
        Item.useTime = 22;
        Item.useAnimation = 22;
        Item.useStyle = 5;
        Item.channel = true;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.knockBack = 4;
        Item.value = 50000;
        Item.rare = 3;
        Item.autoReuse = false;
        Item.shoot = Mod.Find<ModProjectile>("AirSpinnerProjectile").Type;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 6);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
	}
}}