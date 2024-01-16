using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Tradewinds : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Tradewinds");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Tradewinds");
        Item.damage = 17;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 7;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 13;
        Item.useAnimation = 13;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Casts fast moving sunlight feathers");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5;
        Item.value = 150000;
        Item.rare = 3;
        Item.UseSound = SoundID.Item7;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("TradewindsProjectile").Type;
        Item.shootSpeed = 20f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 6);
        recipe.AddIngredient(ItemID.SunplateBlock, 5);
        recipe.AddIngredient(ItemID.Feather, 3);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
    }
}}