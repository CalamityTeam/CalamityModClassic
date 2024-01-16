using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class GammaFusillade : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/GammaFusillade");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Gamma Fusillade");
        Item.damage = 35;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 2;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 3;
        Item.useAnimation = 3;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Unleashes a concentrated beam of gamma radiation");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 3;
        Item.value = 1250000;
        Item.rare = 10;
        Item.UseSound = SoundID.Item33;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("GammaLaser").Type;
        Item.shootSpeed = 20f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "UeliaceBar", 8);
        recipe.AddIngredient(ItemID.SpellTome);
        recipe.AddIngredient(ItemID.SoulofMight, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}