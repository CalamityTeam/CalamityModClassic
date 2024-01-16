using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Cnidarian : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Cnidarian");
		return true;
	}
	
    public override void SetDefaults()
    {
    	Item.CloneDefaults(ItemID.CorruptYoyo);
        //Tooltip.SetDefault("Cnidarian");
        Item.damage = 13;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = 5;
        Item.channel = true;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.knockBack = 3;
        Item.value = 30000;
        Item.rare = 2;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("CnidarianProjectile").Type;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VictideBar", 2);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}